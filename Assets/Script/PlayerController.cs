using Unity.Cinemachine;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CinemachineCamera freeLookCamera;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Rigidbody rb;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get RB
        rb = GetComponent<Rigidbody>();
        //add listener
        inputManager.onMove.AddListener(MovePlayerXZ);
        inputManager.onJump.AddListener(JumpPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = freeLookCamera.transform.forward;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    //apply x,z move of player
    private void MovePlayerXZ(Vector2 direction)
    {
        Vector3 moveDirection = new(direction.x, 0f, direction.y);
        rb.AddForce(moveDirection * speed, ForceMode.Force);
    }

    //apply y move of player
    private void JumpPlayer(Vector3 direction) 
    {
        if (canJump()&&direction.y>0)
        {
            //Debug.Log("jump");
            Vector3 moveDirection = new(0, 1, 0);
            rb.AddForce(moveDirection * jumpForce, ForceMode.Impulse);
        }
    }

    //method to check can or cannot jump
    private bool canJump() 
    {
        if(isGrounded) return true;
        return false;
    }

    //collision checker
    void OnCollisionEnter(Collision collision)
    {
        // Check if it collides with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        // Leave ground then update
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
