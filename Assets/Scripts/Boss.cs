using UnityEngine;

public class Boss : MonoBehaviour
{

    [Tooltip("Stats")]
    public int health = 10000;


    private int phase = 1;
    private GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }


    public void takeDamage(int dmg)
    {
        health -= dmg;
        Debug.Log("Boss Health Remaining: " + health);
    }

    public void die()
    {
        Debug.Log("Boss Killed");
        Destroy(gameObject);
    }
}