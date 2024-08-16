using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FadeInSprite : MonoBehaviour
{
    private float myAlpha = 0f;
    [SerializeField] private bool fadeIn;
    [SerializeField] private float fadeSpeed;

    [SerializeField] UnityEvent FadeInEvent;
    // Start is called before the first frame update
    void Start()
    {
        if (fadeIn)
        {
            myAlpha = 0f;
        }
        
        else
        {
            Time.timeScale = 0;
            myAlpha = 1f;
        }
    }

    // Update is called once per frame
    // void Update()
    // {

    // }

    private void Update()
    {
        if (fadeIn)
        {
            if (myAlpha < 1f)
            {
                myAlpha += fadeSpeed;
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, myAlpha);
            }
            else
            {
                FadeInEvent.Invoke();
            }
        }

        //fade out
        else
        {
            if (myAlpha > 0f)
            {
                myAlpha -= fadeSpeed;
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, myAlpha);
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
