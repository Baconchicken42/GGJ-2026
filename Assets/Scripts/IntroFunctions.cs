using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;

public class IntroFunctions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        for (float alpha = 0f; alpha <= 1; alpha += .05f)
        {
            c.a = alpha;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(.2f);
        }
        isFaded = false;
    }
}
