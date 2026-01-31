using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [Tooltip("References")]
    public Transform patternSpawnAnchor;

    [Tooltip("Stats")]
    public int health = 10000;
    public int phase2TransitionHealth = 6500;
    public int phase3TransitionHealth = 4000;

    [Tooltip("Events")]
    public UnityEvent onTakeDamage;
    public UnityEvent onAttack;
    public UnityEvent onEnterPhase2;
    public UnityEvent onEnterPhase3;
    public UnityEvent onDeath;


    private int phase = 1;
    private GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        if (!gm)
            Debug.LogWarning("Boss: No Game Manager found in scene");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //TODO: shooting logic, animations?
        if (phase == 1)
        {

        }
        else if (phase == 2)
        {

        }
        else if (phase == 3)
        {

        }
        
    }

    private void spawnBulletPattern(GameObject pattern)
    {
        Instantiate(pattern, patternSpawnAnchor.position, patternSpawnAnchor.rotation);
    }


    public void takeDamage(int dmg)
    {
        health -= dmg;
        //TODO: damage effect
        onTakeDamage.Invoke();
        Debug.Log("Boss Health Remaining: " + health);

        if (phase == 1 && health <= phase2TransitionHealth)
            enterPhase2();
        else if (phase == 2 && health <= phase3TransitionHealth)
            enterPhase3();
        else if (health <= 0)
            die();
    }

    private void enterPhase2()
    {
        phase = 2;
        onEnterPhase2.Invoke();
        //TODO: Trigger Animations and Stuff
    }

    private void enterPhase3()
    {
        phase = 3;
        onEnterPhase3.Invoke();
        //TODO: Trigger Animations and Stuff
    }

    private void die()
    {
        Debug.Log("Boss Killed");
        onDeath.Invoke();
        Destroy(gameObject);
    }
}