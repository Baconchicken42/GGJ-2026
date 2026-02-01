using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IntroFunctions : MonoBehaviour
{
    public TMP_Text text;
    public InputActionReference skipAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.color = new Color(1, 1, 1, 0);
        skipAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (skipAction.action.WasPressedThisFrame())
        {
            loadNextLevel();
        }
    }

    public void startFade()
    {
        StartCoroutine(fade());
    }

    public void startUnfade()
    {
        StartCoroutine(unFade());
    }

    private IEnumerator fade()
    {
        Color c = text.color;
        for (float alpha = 1f; alpha >= 0; alpha -= .01f)
        {
            c.a = alpha;
            text.color = c;
            yield return null;
        }
    }

    private IEnumerator unFade()
    {
        Color c = text.color;
        for (float alpha = 0f; alpha <= 1; alpha += .01f)
        {
            c.a = alpha;
            text.color = c;
            yield return null;
        }
    }

    public void loadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
