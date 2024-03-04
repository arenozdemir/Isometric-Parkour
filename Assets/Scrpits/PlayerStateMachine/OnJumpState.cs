using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OnJumpState : BaseState
{
    [SerializeField] float jumpForce;
    private float velocityY;
    private float gravityMultiplier;
    [SerializeField] Transform _camera;
    [SerializeField] BaseState onGround;
    [SerializeField] BaseState dashState;
    [SerializeField] BetweenToWallState betweenWall;
    private void OnEnable()
    {
        velocityY = jumpForce;
        player.isGrounded = false;
        playerRb.isKinematic = false;
        PlayerScript.jumpCount = 1;
    }
    void Update()
    {
        velocityY -= Time.deltaTime * gravityMultiplier;
        if (playerRb.velocity.y < 0)
        {
            gravityMultiplier = 50f;
        }
        else
        {
            gravityMultiplier = 100f;
        }
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVector = _camera.right * inputVector.x + _camera.forward * inputVector.z;

        moveVector.Normalize();
        playerRb.velocity = moveVector * 10f + velocityY * Vector3.up;
        if(player.isGrounded)
        {
            GoToNextState(onGround);
        }
        if (Input.GetKeyDown(KeyCode.Space) && PlayerScript.jumpCount < 2)
        {
            //Yavuz jump count de�erini bilerek static yapt�m ��nk� jump datas�n�n tek bir yerde depolanmas� laz�m. Mesela havada bir kez z�plad�m ve dash att�m bu dashten
            //sonra tekrardan havada 1 kez z�playabilmem laz�m ama. Ama biz burada nesne �zerinden i�lem yapt���m�z i�in OnJumpState tekrar enable oldu�unda eski veriyi tutmuyor
            
            PlayerScript.jumpCount++;
            velocityY = jumpForce;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GoToNextState(dashState);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (player.isBetweenWall && other.TryGetComponent(out WallScrpit wall))
        {
            betweenWall.nextWall = other.transform;
            GoToNextState(betweenWall);
        }
    }
}
