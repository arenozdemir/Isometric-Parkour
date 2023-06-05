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
    int jumpCount;
    private void OnEnable()
    {
        jumpCount = 0;
        velocityY = jumpForce;
        player.isGrounded = false;
        playerRb.isKinematic = false;
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
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            velocityY = jumpForce;
            jumpCount++;
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
