using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Rigidbody rb;
    private bool isJumped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get RB
        rb = GetComponent<Rigidbody>();
        //add listener
        inputManager.onMove.AddListener(MovePlayerXZ);
        inputManager.onJump.AddListener(JumpPlayer);
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
        Vector3 moveDirection = new(0,direction.y,0);
        rb.AddForce(moveDirection * jumpForce, ForceMode.Impulse);
    }
}
