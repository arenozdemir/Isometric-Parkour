using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasState : BaseState
{
    float timer;
    [SerializeField] OnGroundState groundState;
    private void OnEnable()
    {
        timer = 0;
    }
    void Update()
    {
        timer += Time.deltaTime;
        playerRb.velocity = transform.forward * 40f;
        if (timer > 0.2f)
        {
            GoToNextState(groundState);
        }
    }
}
