using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("References")]
    public PlayerController player;
    public Boss boss;
    public InputActionReference pauseAction;

    [Tooltip("Events")]
    public UnityEvent onPause;
    public UnityEvent onResume;
    
    [Header("HUD Elements")]
    [SerializeField] private List<GameObject> playerLives = new List<GameObject>();
    [SerializeField] private SpriteRenderer skyGradient;

    private bool isGamePaused;

    private static GameManager _instance;
    public static GameManager Instance {  get { return _instance; } }

    private void Awake()
    {
        Debug.Log("Timing debugging: GameManager Awake");
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Debug.Log("Timing debugging: GameManager Start");
        if (player == null)
        {
            Debug.LogWarning("Gamemanager: no Player object set");
        }
        if (boss == null)
            Debug.LogWarning("Gamemanager: no Boss object set");
        
        PlayerController.onTakeDamage.AddListener(UpdatePlayerLifeCountUI);

        pauseAction.action.Enable();

        skyGradient.color = new Color(1, 1, 1, 0f);
    }

    private void OnDestroy()
    {
        PlayerController.onTakeDamage.RemoveListener(UpdatePlayerLifeCountUI);
    }

    private void Update()
    {
        if (pauseAction.action.WasReleasedThisFrame())
        {
            if (isGamePaused)
                resumeGame();
            else
                pauseGame();
        }
    }

    private void FixedUpdate()
    {
        float newAlpha = Mathf.Abs(boss.health - boss.totalHealth) * 0.0001f;
        skyGradient.color = new Color(1,1,1, newAlpha);
    }

    public void loadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void loadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void loadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Application.Quit();
    }


    public void pauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        onPause.Invoke();
        Debug.Log("Game Paused!");
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        onResume.Invoke();
        Debug.Log("Game Resumed!");
    }

    private void UpdatePlayerLifeCountUI()
    {
        playerLives[player.livesRemaining].transform.GetChild(0).gameObject.SetActive(false);
    }

}
