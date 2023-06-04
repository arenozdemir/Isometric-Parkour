using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected PlayerScript player;
    protected Rigidbody playerRb;
    protected void Awake()
    {
        player = GetComponentInParent<PlayerScript>();
        playerRb = GetComponentInParent<Rigidbody>();
    }
    public void GoToNextState(BaseState nextState)
    {
        gameObject.SetActive(false);
        nextState.gameObject.SetActive(true);
    }
}
