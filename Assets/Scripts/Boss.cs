using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [Tooltip("References")]
    public Transform patternSpawnAnchor;
    public List<GameObject> BulletSpawners;
    public AudioSource dmgSFX;

    [Tooltip("Stats")]
    public int health = 10000;
    public int totalHealth = 10000;
    public int phase2TransitionHealth = 6500;
    public int phase3TransitionHealth = 4000;

    [Tooltip("Events")]
    public UnityEvent onTakeDamage;
    public UnityEvent onAttack;
    public UnityEvent onEnterPhase2;
    public UnityEvent onEnterPhase3;
    public UnityEvent onDeath;

    [Tooltip("Visual Elements")] 
    public SpriteRenderer bodySpriteRenderer;
    public SpriteRenderer head1SpriteRenderer;
    public SpriteRenderer head2SpriteRenderer;
    public SpriteRenderer head3SpriteRenderer;
    public List<Sprite> phase1HeadSprites;
    public List<Sprite> phase2HeadSprites;
    public Animator phase3HeadSpriteAnim;
    
    
    private int phase = 1;
    private GameManager gm;

    private bool dmgSoundHasPlayed = true;

    [SerializeField] 
    private bool isTestingPhase3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameManager.Instance;
        if (!gm)
            Debug.LogWarning("Boss: No Game Manager found in scene");

        head1SpriteRenderer.sprite = phase1HeadSprites[0];
        head2SpriteRenderer.gameObject.SetActive(false);
        head3SpriteRenderer.gameObject.SetActive(false);
        
        if (isTestingPhase3)
        {
            head1SpriteRenderer.gameObject.SetActive(false);
            head3SpriteRenderer.gameObject.SetActive(true);
            phase = 3; 
            enterPhase3();
        }
        
        foreach (GameObject spawner in BulletSpawners) 
            spawner.SetActive(spawner == BulletSpawners[0]); //phase 1
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTestingPhase3)
        {
            if (health <= totalHealth * 0.90f && health > totalHealth * 0.80f)
                head3SpriteRenderer.color = bodySpriteRenderer.color = Color.indianRed;
            if (health <= totalHealth * 0.80f && health > totalHealth * 0.70f)
                head3SpriteRenderer.color = bodySpriteRenderer.color = Color.softRed;
            if (health <= totalHealth * 0.70f && health > totalHealth * 0.60f)
                head3SpriteRenderer.color = bodySpriteRenderer.color = Color.crimson;

            return;
        }

        if (phase == 1)
        {
            //why won't it let me make this a switch case or something T_T
            if (health <= totalHealth * 0.90f && health > totalHealth * 0.85f)
            {
                if (head1SpriteRenderer.sprite != phase1HeadSprites[1])
                    dmgSoundHasPlayed = false;
                head1SpriteRenderer.sprite = phase1HeadSprites[1];
            }
            else if (health <= totalHealth * 0.85f && health > totalHealth * 0.80f)
            {
                if (head1SpriteRenderer.sprite != phase1HeadSprites[2])
                    dmgSoundHasPlayed = false;
                head1SpriteRenderer.sprite = phase1HeadSprites[2];
            }
            else if (health <= totalHealth * 0.80f && health > totalHealth * 0.75f)
            {
                if (head1SpriteRenderer.sprite != phase1HeadSprites[3])
                    dmgSoundHasPlayed = false;
                head1SpriteRenderer.sprite = phase1HeadSprites[3];
            }
            else if (health <= totalHealth * 0.75f && health > totalHealth * 0.70f)
            {
                if (head1SpriteRenderer.sprite != phase1HeadSprites[4])
                    dmgSoundHasPlayed = false;
                
                head1SpriteRenderer.sprite = phase1HeadSprites[4];
                head2SpriteRenderer.gameObject.SetActive(true);
            }
            else if (health <= totalHealth * 0.70f && health > totalHealth * 0.65f)
            {
                if (head1SpriteRenderer.sprite != phase1HeadSprites[5])
                    dmgSoundHasPlayed = false;
                head1SpriteRenderer.sprite = phase1HeadSprites[5];
            }
            
        }
        else if (phase == 2)
        {
            if (health <= totalHealth * 0.60f && health > totalHealth * 0.55f)
            {
                if (head2SpriteRenderer.sprite != phase2HeadSprites[1])
                    dmgSoundHasPlayed = false;
                head2SpriteRenderer.sprite = phase2HeadSprites[1];
            }
            else if (health <= totalHealth * 0.55f && health > totalHealth * 0.50f)
            {
                if (head2SpriteRenderer.sprite != phase2HeadSprites[2])
                    dmgSoundHasPlayed = false;
                head2SpriteRenderer.sprite = phase2HeadSprites[2];
            }
            else if (health <= totalHealth * 0.50f && health > totalHealth * 0.48f)
            {
                if (head2SpriteRenderer.sprite != phase2HeadSprites[3])
                    dmgSoundHasPlayed = false;
                head2SpriteRenderer.sprite = phase2HeadSprites[3];
            }
            else if (health <= totalHealth * 0.48f && health > totalHealth * 0.45f)
            {
                if (head2SpriteRenderer.sprite != phase2HeadSprites[4])
                    dmgSoundHasPlayed = false;
                
                head2SpriteRenderer.sprite = phase2HeadSprites[4];
                head3SpriteRenderer.gameObject.SetActive(true);
            }
            else if (health <= totalHealth * 0.45f && health > totalHealth * 0.40f)
            {
                if (head2SpriteRenderer.sprite != phase2HeadSprites[5])
                    dmgSoundHasPlayed = false;
                head2SpriteRenderer.sprite = phase2HeadSprites[5];
            }
        }
        else if (phase == 3)
        {
            if (health <= totalHealth * 0.35f && health > totalHealth * 0.25f)
                head3SpriteRenderer.color = bodySpriteRenderer.color = Color.indianRed;

            if (health <= totalHealth * 0.25f && health > totalHealth * 0.15f)
                head3SpriteRenderer.color = bodySpriteRenderer.color = Color.softRed;

            if (health <= totalHealth * 0.25f && health > totalHealth * 0.15f)
                head3SpriteRenderer.color = bodySpriteRenderer.color = Color.crimson;
        }
        
        PlayDmgSoundInAHackyWay();
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
        //Debug.Log("Boss Health Remaining: " + health);

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
        
        head1SpriteRenderer.gameObject.SetActive(false);
        head2SpriteRenderer.sprite = phase2HeadSprites[0];
        dmgSFX.Play();
        
        foreach (GameObject spawner in BulletSpawners)
            spawner.SetActive(spawner == BulletSpawners[1]);
    }

    private void enterPhase3()
    {
        phase = 3;
        onEnterPhase3.Invoke();
        //TODO: Trigger Animations and Stuff
        
        head2SpriteRenderer.gameObject.SetActive(false);
        dmgSFX.Play();
        head3SpriteRenderer.transform.localPosition= new Vector3(0, -1.35f, 0);
        head3SpriteRenderer.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        phase3HeadSpriteAnim.enabled = true;
        
        foreach (GameObject spawner in BulletSpawners)
            spawner.SetActive(spawner == BulletSpawners[2]);
    }

    private void die()
    {
        Debug.Log("Boss Killed");
        //TODO: play death animations if any
        onDeath.Invoke();
        Destroy(gameObject);
    }

    private void PlayDmgSoundInAHackyWay()
    {
        if (dmgSoundHasPlayed == false)
        {
            dmgSoundHasPlayed = true;
            
            if (!dmgSFX.isPlaying)
                dmgSFX.Play();
        }
    }
}