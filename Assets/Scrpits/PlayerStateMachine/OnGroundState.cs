using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundState : BaseState
{
    [SerializeField] Transform _camera;
    private float velocityY;
   
    private float gravityMultiplier = 100f;
    [SerializeField] BaseState jumpState;
    [SerializeField] BaseState dashState;

    private void OnEnable()
    {
        player.isGrounded= true;
    }
    void Update()
    {
        //velocityY -= Time.deltaTime * gravityMultiplier;
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVector = _camera.right * inputVector.x + _camera.forward * inputVector.z;

        playerRb.velocity = moveVector * 10f + playerRb.velocity.y * Vector3.up;
        if(Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            GoToNextState(jumpState);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
            GoToNextState(dashState);
        player.transform.forward = moveVector;
    }
}
