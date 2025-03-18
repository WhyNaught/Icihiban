using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")] // just lets us change this via the visual editor 
    public float moveSpeed;

    private float tempMoveSpeed; 

    public float groundDrag; 
    public float jumpForce; 
    public float jumpCooldown;
    public float airMultiplier; 
    bool canJump = true; 

    [Header("Key Bindings")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public bool isGrounded = true; 
    public float playerHeight; 
    public LayerMask whatIsGround; 

    public Transform orientation; 

    float horizontalInput; float verticalInput;  
    Vector3 moveDirection; 
    Rigidbody rb; 

    void Start() {
        rb = GetComponent<Rigidbody>(); 
        rb.freezeRotation = true; 
    }

    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (canJump && Input.GetKey(jumpKey) && isGrounded) {
            canJump = false; 
            Jump(); 
            Invoke(nameof(ResetJump), jumpCooldown); 
        }

        
        float sprintMultiplier = Input.GetKey(KeyCode.LeftShift) ? 1.4f : 1f; 
        tempMoveSpeed = moveSpeed * sprintMultiplier; 
    }

    private void MovePlayer() {
        // ensures we walk where we are looking 
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput; 
        if (isGrounded) {
            rb.AddForce(moveDirection.normalized * tempMoveSpeed * 10f, ForceMode.Force); 
        } else if (!isGrounded) {
            rb.AddForce(moveDirection.normalized * tempMoveSpeed * 10f * airMultiplier, ForceMode.Force); 
        }
    }

    private void LimitSpeed() {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVelocity.magnitude > tempMoveSpeed) { // limits our speed
            Vector3 lim = flatVelocity.normalized * tempMoveSpeed; 
            rb.linearVelocity = new Vector3(lim.x, rb.linearVelocity.y, lim.z);
        }
    }

    private void ResetJump() {
        canJump = true; 
    }

    private void Jump() {
        // reset y velo to 0 
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput(); 
        LimitSpeed(); 
        if (isGrounded) {
            rb.linearDamping = groundDrag; 
        } else {
            rb.linearDamping = 0; 
        }
    }

    void FixedUpdate()
    {
        MovePlayer(); 
    }
}
