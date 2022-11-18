using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheckFront;
    [SerializeField] private Transform groundCheckBack;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float moveSpeed = 1000f;
    [SerializeField] private float startJumpForce = 400f;
    [SerializeField] private float gravityMod = 5f;

    private Rigidbody rb;

    private Vector3 movePosition;

    private Animator animator;

    private float horizontalInput;
    private float timeAtBeginningOfJump;
    private float timeAtEndingOfJump;
    private float jumpMod;
    private float timeDifference;
    public float isGroundedRange = 0.1f;

    private Vector3 savePosition;

    public bool isGrounded;
    

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
        ProcessMovement();
        ProcessRotation();
        CheckJump();
        CheckAndPullSwitch();
    }

    private void ProcessRotation()
    {
        if (horizontalInput > 0 && isGrounded)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        } else if (horizontalInput < 0 && isGrounded)
        {
            transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        }
    }

    private void ProcessMovement()
    {
        if (isGrounded && !Input.GetButton("Jump"))
        {
            rb.AddForce(movePosition * moveSpeed, ForceMode.Force);
            if (horizontalInput == 0)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        } 
        
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
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        
        movePosition = new Vector3(horizontalInput, 0f, 0f);

        isGrounded = CheckIsGrounded(groundCheckFront, groundLayer) || CheckIsGrounded(groundCheckBack, groundLayer);
    }

    private bool CheckIsGrounded(Transform groundCheck, LayerMask layer)
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, isGroundedRange, groundLayer);
    }

    private void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            timeAtBeginningOfJump = Time.time;
        }
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
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
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravityMod, ForceMode.Acceleration);
        }
        animator.SetBool("isLoadingJump", Input.GetButton("Jump") && isGrounded);
        animator.SetBool("isGrounded", isGrounded);
    }
}
