using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] Transform camera;
    public bool isGrounded;
    public static int jumpCount;
    private bool isDashed;
    private float dashTimer;
    private float velocityY;
    [SerializeField] private float jumpForce;
    private float gravityMultiplier;

    public bool isBetweenWall;
    
    private void Awake()
    {
        
        playerRb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Debug.Log(jumpCount);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BetweenWall"))
            isBetweenWall = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BetweenWall"))
            isBetweenWall = false; 
    }

}
