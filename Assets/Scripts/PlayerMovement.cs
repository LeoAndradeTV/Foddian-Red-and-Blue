using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float moveSpeed = 1000f;
    [SerializeField] private float startJumpForce = 400f;

    private Rigidbody rb;

    private Vector3 movePosition;

    private Animator animator;

    private float horizontalInput;
    private float timeAtBeginningOfJump;
    private float timeAtEndingOfJump;
    private float jumpMod;
    private float timeDifference;
    private float rotationSpeed = 800f;
    private float isGroundedRange = 0.35f;

    private Vector3 savePosition;

    private bool getJumpInput;
    private bool isGrounded;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        savePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // THIS IS A CHEAT
        if (Input.GetKeyDown(KeyCode.V))
        {
            savePosition = transform.position;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.position = savePosition;
        }
        GetMovementInput();
        CheckJump();
        CheckAndPullSwitch();
    }

    private static void CheckAndPullSwitch()
    {
        if (GameManager.Instance.switchIsInteractable && Input.GetKeyDown(KeyCode.E))
        {
            Actions.OnSwitchPulled();
        }
    }

    private void GetMovementInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0 && !Input.GetButton("Jump") && isGrounded)
        {
            animator.SetFloat("Speed_f", Mathf.Abs(horizontalInput));
            animator.SetBool("Static_b", false);
        } else
        {
            animator.SetFloat("Speed_f", 0);
            animator.SetBool("Static_b", true);
        }
        if (horizontalInput != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(movePosition, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        getJumpInput = Input.GetButtonDown("Jump");
        movePosition = new Vector3(-horizontalInput, 0f, 0f);
        isGrounded = (Physics.Raycast(groundCheck.position, Vector3.down, isGroundedRange, groundLayer)) || (Physics.Raycast(groundCheck.position + new Vector3(0.7f, 0, 0), Vector3.down , isGroundedRange, groundLayer)) || (Physics.Raycast(groundCheck.position - new Vector3(0.7f, 0, 0), Vector3.down, isGroundedRange, groundLayer)) || (Physics.Raycast(groundCheck.position + new Vector3(0f, 0, 0.35f), Vector3.down, isGroundedRange, groundLayer)) || (Physics.Raycast(groundCheck.position - new Vector3(0.35f, 0, 0), Vector3.down, isGroundedRange, groundLayer));
    }

    private void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            timeAtBeginningOfJump = Time.time;
        }
        if (Input.GetButton("Jump") && isGrounded)
        {
            timeDifference += Time.deltaTime;
        }
        if ((Input.GetButtonUp("Jump") || timeDifference > 1f) && isGrounded) 
        {
            timeAtEndingOfJump = Time.time;
            jumpMod = 0.5f + (timeAtEndingOfJump - timeAtBeginningOfJump) * 1.05f;
            jumpMod = Mathf.Clamp(jumpMod, 0.5f, 1.5f);
            Vector3 forceToAdd = ((Vector3.up * jumpMod) + movePosition) * startJumpForce;
            rb.AddForce(forceToAdd, ForceMode.Impulse);
            if (isGrounded)
            {
                timeDifference = 0f;
            }
        }
        
        animator.SetBool("Grounded", isGrounded);

    }

    private void FixedUpdate()
    {
        if (isGrounded && !Input.GetButton("Jump"))
        {
            rb.AddForce(movePosition * moveSpeed, ForceMode.Force);
        }
        if (horizontalInput == 0 && isGrounded)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
