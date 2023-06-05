using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenToWallState : BaseState
{
    [SerializeField] Transform wall1, wall2;
    [SerializeField] BaseState onGround;
    [SerializeField] LayerMask ground;
    public Transform nextWall;
    private void OnEnable()
    {
        playerRb.isKinematic = true;
        playerRb.velocity = Vector3.zero;
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f, ground))
        {
            GoToNextState(onGround);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.isKinematic = false;
            Vector3 dir = nextWall.forward;
            player.transform.forward = dir;
            playerRb.velocity = 10 * dir + Vector3.up * 6f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (player.isBetweenWall && other.TryGetComponent(out WallScrpit wall))
        {
            if(other.transform != nextWall)
            {
                nextWall = other.transform;
                playerRb.isKinematic = true;
                playerRb.velocity = Vector3.zero;
            }
        }
    }
}
