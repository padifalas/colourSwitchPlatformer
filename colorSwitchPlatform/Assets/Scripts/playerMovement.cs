using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private PlayerControls controls;
    private Vector2 moveInput;
    private bool isGrounded;

    public Transform playerCamera; 
    public float lookSpeedX = 2f; 
    public float lookSpeedY = 2f; 
    private float currentRotationX = 0f; 
    private float currentRotationY = 0f; 

    void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += _ => moveInput = Vector2.zero;
        controls.Player.Jump.performed += _ => Jump();
        controls.Player.LookAround.performed += ctx => LookAround(ctx.ReadValue<Vector2>());
    }

    void OnDisable()
    {
        controls.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled -= _ => moveInput = Vector2.zero;
        controls.Player.Jump.performed -= _ => Jump();
        controls.Player.LookAround.performed -= ctx => LookAround(ctx.ReadValue<Vector2>());
        controls.Player.Disable();
    }

    void FixedUpdate()
    {
       
        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right; 

        forward.y = 0; 
        right.y = 0; 

        forward.Normalize();
        right.Normalize(); 

     
        Vector3 move = forward * moveInput.y + right * moveInput.x;    // move the player based on the camera's direction and input
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Player jumping");
        }
    }

    void LookAround(Vector2 lookInput)
    {
  
        currentRotationX -= lookInput.y * lookSpeedY;
        currentRotationX = Mathf.Clamp(currentRotationX, -50f, 50f); 
        
        currentRotationY += lookInput.x * lookSpeedX;
        
  
        transform.localRotation = Quaternion.Euler(0, currentRotationY, 0); // rotate player (Y-axis rotation)
        /*playerCamera.localRotation = Quaternion.Euler(currentRotationX, 0, 0); */
    }
}
