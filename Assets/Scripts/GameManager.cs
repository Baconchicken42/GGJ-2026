using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("References")]
    public PlayerController player;
    public InputActionReference pauseAction;

    [Tooltip("Events")]
    public UnityEvent onPause;
    public UnityEvent onResume;


    private bool isGamePaused;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<PlayerController>();
            if (player == null)
            {
                Debug.Log("Gamemanager: No Player object found");
            }
        }

        pauseAction.action.Enable();
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
    
}
