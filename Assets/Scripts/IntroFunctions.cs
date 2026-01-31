using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class IntroFunctions : MonoBehaviour
{
    public TMP_Text text;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator fade()
    {
        Color c = text.color;
        for (float alpha = 1f; alpha >= 0; alpha -= .05f)
        {
            c.a = alpha;
            text.color = c;
            yield return new WaitForSeconds(.2f);
        }
    }

    private IEnumerator unFade()
    {
        Color c = text.color;
        for (float alpha = 0f; alpha <= 1; alpha += .05f)
        {
            c.a = alpha;
            text.color = c;
            yield return new WaitForSeconds(.2f);
        }
    }
}
