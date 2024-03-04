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
            //Yavuz jump count deðerini bilerek static yaptým çünkü jump datasýnýn tek bir yerde depolanmasý lazým. Mesela havada bir kez zýpladým ve dash attým bu dashten
            //sonra tekrardan havada 1 kez zýplayabilmem lazým ama. Ama biz burada nesne üzerinden iþlem yaptýðýmýz için OnJumpState tekrar enable olduðunda eski veriyi tutmuyor
            
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
