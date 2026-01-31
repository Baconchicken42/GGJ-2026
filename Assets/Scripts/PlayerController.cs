using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference shootAction;

    public float movementSpeed = 1.0f;

    public float screenWidth = 1f;
    public float screenHeight = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.action.Enable();
        shootAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 moveDir = moveAction.action.ReadValue<Vector2>();

        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * Time.deltaTime * movementSpeed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -screenWidth, screenWidth), Mathf.Clamp(transform.position.y, -screenHeight, screenHeight));


        
    }

    private void shoot()
    {
        Debug.Log("Shooting!");
        return;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(-screenWidth, -screenHeight, 0), new Vector3(-screenWidth, screenHeight, 0)); //left wall
        Gizmos.DrawLine(new Vector3(screenWidth, -screenHeight, 0), new Vector3(screenWidth, screenHeight, 0)); //right wall
    }
}
