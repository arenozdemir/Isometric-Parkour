using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] Transform camera;
    private bool isGrounded;
    private int jumpCount;
    private bool isDashed;
    private float dashTimer;
    private float velocityY;
    [SerializeField] private float jumpForce;
    private float gravityMultiplier;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Debug.Log(isDashed);
        velocityY -= Time.deltaTime * gravityMultiplier;
        dashTimer += Time.deltaTime;
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVector = camera.right * inputVector.x + camera.forward * inputVector.z;

        moveVector.Normalize();
        if (playerRb.velocity.y < 0)
        {
            gravityMultiplier = 50f;
        }
        else
        {
            gravityMultiplier = 100f;
        }
        if (!isDashed)
        {
            playerRb.velocity = moveVector * 10f + Vector3.up * playerRb.velocity.y;
        }
        if (!isGrounded && !isDashed)
        {
            playerRb.velocity = moveVector * 10f + velocityY * Vector3.up;
        }
        transform.forward = moveVector;
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 1)
        {
            JumpController();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer > 2f)
        {
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash()
    {
        dashTimer = 0;
        isDashed = true;
        playerRb.velocity = transform.forward * 40f;
        yield return new WaitForSeconds(0.2f);
        isDashed = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
    void JumpController()
    {
        isGrounded = false;
        jumpCount++;
        //playerRb.velocity = Vector3.up * 5f;
        velocityY = jumpForce;
    }
}
