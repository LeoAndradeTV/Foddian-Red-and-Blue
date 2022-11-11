using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;

    private Vector3 movePosition;

    private float moveSpeed = 15f;
    private float horizontalInput;
    private float jumpForce = 7f;

    private bool getJumpInput;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        if (getJumpInput)
        {
            Jump();
        }
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
    }

    private void Jump()
    {
        bool isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.25f, groundLayer);
        Debug.Log(isGrounded);
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (movePosition * moveSpeed * Time.fixedDeltaTime));
    }
}
