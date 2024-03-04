using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasState : BaseState
{
    float timer;
    [SerializeField] OnGroundState groundState;
    Vector3 startPosition;
    private void OnEnable()
    {
        startPosition = player.transform.position;
        timer = 0;
    }
    void Update()
    {
        timer += Time.deltaTime;
        playerRb.velocity = transform.forward * 40f;
        if ((timer > 0.2f))
        {
            GoToNextState(groundState);
        }
    }
}
