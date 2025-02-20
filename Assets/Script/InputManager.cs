using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector2> onMove = new UnityEvent<Vector2>();
    public UnityEvent<Vector3> onJump = new UnityEvent<Vector3>();
    public UnityEvent<Vector2> onDash = new UnityEvent<Vector2>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move in pannle
        Vector2 inputXZ = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) 
        {
            inputXZ += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputXZ += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            inputXZ += Vector2.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputXZ += Vector2.left;
        }
        onMove?.Invoke(inputXZ);

        //jump
        Vector3 inputY = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.Space)) // we just want the jump is called once when space is press down.
        { 
            inputY = Vector3.up; // because jump is impulse force, so vector is constant
            onJump?.Invoke(inputY);//make sure the jump would not be multiple called
        }

        //Dash
        Vector2 inputDash = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.E)) 
        { 
            onDash?.Invoke(inputDash);
        }
    }
}
