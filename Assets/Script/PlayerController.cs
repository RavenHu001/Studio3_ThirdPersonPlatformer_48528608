using System.Collections;
using Unity.Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CinemachineCamera freeLookCamera;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCoolDown;

    private int maxJumpCount = 2;
    private int jumpCount = 0;
    private int maxDashCount = 1;
    private int dashCount = 0;
    private float height;
    private bool isGrounded;

    private Coroutine resetCoroutine;

    private Rigidbody rb;
    private CapsuleCollider playerCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the height of player
        playerCollider = GetComponent<CapsuleCollider>();
        height = playerCollider.height/2;
        //get RB
        rb = GetComponent<Rigidbody>();
        //add listener
        inputManager.onMove.AddListener(MovePlayerXZ);
        inputManager.onJump.AddListener(JumpPlayer);
        inputManager.onDash.AddListener(DashPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = freeLookCamera.transform.forward;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        // if dash used start cooldown
        if (dashCount == maxDashCount) 
        {
            resetCoroutine = StartCoroutine(ResetAfterTime());
        }
    }

    //apply x,z move of player
    private void MovePlayerXZ(Vector2 direction)
    {
            Vector3 moveDirection = transform.forward * direction.y + transform.right * direction.x;
            rb.AddForce(moveDirection * speed, ForceMode.Force);
    }

    //apply y move of player
    private void JumpPlayer(Vector3 direction) 
    {
        //call check ground
        CheckGrounded();
        if (jumpCount<maxJumpCount)
        {
            Debug.Log("jump");
            //choose the force of jump based on jumpCount
            float force = (jumpCount != maxJumpCount) ? jumpForce : doubleJumpForce;
            // reset the y axis velocity
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            Vector3 moveDirection = new(0, direction.y, 0);
            rb.AddForce(moveDirection * jumpForce, ForceMode.Impulse);
            //update jump count
            jumpCount++;
        }
    }

    //apply dash to player
    private void DashPlayer(Vector2 direction) 
    {
        if (dashCount < maxDashCount) 
        {
            Debug.Log("Dash");
            // reset all velocity
            rb.linearVelocity = new Vector3(0, 0, 0);
            rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
            //update dash count
            dashCount++;
        }
    }

    //method to check can or cannot jump
    private void CheckGrounded() 
    {
        //change to lazer checker
        float rayLength = height+0.01f;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength);

        if (isGrounded)
        {
            //reset jumpCount
            jumpCount = 0;
        }
    }

    //method to reset dashCount
    private IEnumerator ResetAfterTime() 
    {
        //waite for cool Down
        yield return new WaitForSeconds(dashCoolDown);
        //reset dashCount
        dashCount = 0;
        //remove timer
        resetCoroutine = null;
    }

}
