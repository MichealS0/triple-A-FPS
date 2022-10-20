using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("KeyBinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Checks")]
    [SerializeField] private float playerH;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float GroundDrag;
    bool isGrounded;


    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private float JumpForce;
    [SerializeField] private float JumpCoolDown;
    [SerializeField] private float airMultiplier;
    bool readyforJump;

    [SerializeField] private Transform orientation;

    float horizontalInput;
    float verticalInput;

    [SerializeField] private Vector3 moveDirection;

    [SerializeField] private Rigidbody rb;

    void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyforJump = true;
    }

    void Update()
    {
        InputHandler();
        SpeedControl();
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerH * 0.5f + 0.2f, ground);

        if(isGrounded)
            rb.drag = GroundDrag;
        else
            rb.drag =0;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void InputHandler()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyforJump && isGrounded)
        {
            readyforJump = false;

            Jump();

            Invoke(nameof(resetJump), JumpCoolDown);
        }
    }

    void MovePlayer()
    {
        // Calc direction facing
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Move the nigga

        if(isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        
        else if(!isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, rb.velocity.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.z, 0f, rb.velocity.z);

        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    void resetJump()
    {
        readyforJump = true;
    }
    // -- Back At It Again
}
