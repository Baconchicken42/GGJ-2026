using UnityEngine;

public class Boss : MonoBehaviour
{

    [Tooltip("Stats")]
    public int health = 10000;
    public int phase2TransitionHealth = 6500;
    public int phase3TransitionHealth = 4000;


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


    public void takeDamage(int dmg)
    {
        health -= dmg;
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
        //TODO: Trigger Animations and Stuff
    }

    private void enterPhase3()
    {
        phase = 3;
        //TODO: Trigger Animations and Stuff
    }

    private void die()
    {
        Debug.Log("Boss Killed");
        Destroy(gameObject);
    }
}