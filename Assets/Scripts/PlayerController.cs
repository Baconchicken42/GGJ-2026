using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Tooltip("References")]
    public InputActionReference moveAction;
    public InputActionReference shootAction;
    public GameObject bulletPrefab;
    public Transform bulletSpawnAnchor;
    public SpriteRenderer spriteRenderer;

    [Tooltip("Stats")]
    public float movementSpeed = 1.0f;
    public int livesRemaining = 3;
    public float shootCooldownSeconds = .2f;
    public float takeDamageCooldownSeconds = 1.5f;

    [Tooltip("Screen Size")]
    public float screenWidth = 1f;
    public float screenHeight = 2f;

    [Tooltip("Events")]
    public UnityEvent onDeath;
    public UnityEvent onTakeDamage;
    public UnityEvent onShoot;

    private GameManager gm;
    private float shootCooldownTimer = 0f;
    private bool canTakeDamage = true;
    private float takeDamageCooldownTimer = 100f;
    private bool isFaded = false;

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
    void FixedUpdate()
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


        takeDamageCooldownTimer += Time.deltaTime;
        if (takeDamageCooldownTimer >= takeDamageCooldownSeconds)
        {
            canTakeDamage = true;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        }
        else //while invincible
        {
            if (!isFaded)
                StartCoroutine(fade());
            else
                StartCoroutine(unFade());
        }
    }

    public void takeDamage(int dmg = 1)
    {
        if (!canTakeDamage)
        {
            return;
        }

        livesRemaining -= dmg;
        Debug.Log("Lives Remaining: " + livesRemaining);
        canTakeDamage = false;
        takeDamageCooldownTimer = 0f;
        onTakeDamage.Invoke();


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
        onDeath.Invoke();
        Destroy(gameObject);
    }

    private void shoot()
    {
        Debug.Log("Shooting!");
        shootCooldownTimer = 0f;
        if (bulletPrefab)
            Instantiate(bulletPrefab, bulletSpawnAnchor.position, bulletSpawnAnchor.rotation);
        onShoot.Invoke();
        return;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(-screenWidth, -screenHeight, 0), new Vector3(-screenWidth, screenHeight, 0)); //left wall
        Gizmos.DrawLine(new Vector3(screenWidth, -screenHeight, 0), new Vector3(screenWidth, screenHeight, 0)); //right wall
    }

    private IEnumerator fade()
    {
        Color c = spriteRenderer.color;
        for (float alpha = 1f; alpha >= 0; alpha -= .05f)
        {
            c.a = alpha;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(.2f);
        }
        isFaded = true;
    }

    private IEnumerator unFade()
    {
        Color c = spriteRenderer.color;
        for (float alpha = 0f; alpha<= 1; alpha += .05f)
        {
            c.a = alpha;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(.2f);
        }
        isFaded = false;
    }
}
