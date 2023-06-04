using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenToWallState : BaseState
{
    [SerializeField] Transform wall1, wall2;
    public Transform nextWall;
    private void OnEnable()
    {
        playerRb.isKinematic = true;
        playerRb.velocity = Vector3.zero;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.isKinematic = false;
            Vector3 dir = nextWall.forward;
            player.transform.forward = dir;
            playerRb.velocity = 5 * dir + Vector3.up * 6f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") && player.isBetweenWall)
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
