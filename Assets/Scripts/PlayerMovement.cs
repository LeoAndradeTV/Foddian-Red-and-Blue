using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;

    private Vector3 movePosition;

    private float moveSpeed = 8f;
    private float horizontalInput;
    private float startJumpForce = 400f;
    private float timeAtBeginningOfJump;
    private float timeAtEndingOfJump;
    private float jumpMod;
    private float timeDifference;

    private bool getJumpInput;
    private bool isGrounded;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
        getJumpInput = Input.GetButtonDown("Jump");
        movePosition = new Vector3(horizontalInput, 0f, 0f);
        isGrounded = (Physics.Raycast(groundCheck.position, Vector3.down, 0.25f, groundLayer) || Physics.Raycast(groundCheck.position - new Vector3(0.9f, 0, 0), Vector3.down , 0.25f, groundLayer));
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
            rb.AddForce(((Vector3.up * jumpMod) + movePosition) * startJumpForce, ForceMode.Impulse);
            timeDifference = 0f;
        }
        


    }

    private void FixedUpdate()
    {
        if (isGrounded && !Input.GetButton("Jump"))
        {
            rb.MovePosition(transform.position + (movePosition * moveSpeed * Time.fixedDeltaTime));
        }
    }
}
