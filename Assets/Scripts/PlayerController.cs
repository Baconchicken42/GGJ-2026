using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("References")]
    public InputActionReference moveAction;
    public InputActionReference shootAction;
    public GameObject bulletPrefab;
    public Transform bulletSpawnAnchor;

    [Tooltip("Stats")]
    public float movementSpeed = 1.0f;
    public int livesRemaining = 3;
    public float shootCooldownSeconds = .2f;

    [Tooltip("Screen Size")]
    public float screenWidth = 1f;
    public float screenHeight = 2f;

    private float shootCooldownTimer = 0f;
    private GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.action.Enable();
        shootAction.action.Enable();

        gm = FindFirstObjectByType<GameManager>();
        if (!gm)
            Debug.LogWarning("PlayerController: No Game Manager found in scene");
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        Vector2 moveDir = moveAction.action.ReadValue<Vector2>();
        
        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * Time.deltaTime * movementSpeed;
        //clamp player pos within screen bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -screenWidth, screenWidth), Mathf.Clamp(transform.position.y, -screenHeight, screenHeight));

        shootCooldownTimer += Time.deltaTime;

        if (shootAction.action.IsPressed() && shootCooldownTimer >= shootCooldownSeconds)
        {
            shoot();
        }
        
    }

    public void takeDamage(int dmg = 1)
    {
        livesRemaining -= dmg;
        Debug.Log("Lives Remaining: " + livesRemaining);
        if (livesRemaining <= 0)
        {
            endGame();
        }
    }

    private void endGame()
    {
        Debug.Log("You died, game over");
        moveAction.action.Disable();
        shootAction.action.Disable();
        Destroy(gameObject);
    }

    private void shoot()
    {
        Debug.Log("Shooting!");
        shootCooldownTimer = 0f;
        if (bulletPrefab)
            Instantiate(bulletPrefab, bulletSpawnAnchor.position, bulletSpawnAnchor.rotation);

        return;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(-screenWidth, -screenHeight, 0), new Vector3(-screenWidth, screenHeight, 0)); //left wall
        Gizmos.DrawLine(new Vector3(screenWidth, -screenHeight, 0), new Vector3(screenWidth, screenHeight, 0)); //right wall
    }
}
