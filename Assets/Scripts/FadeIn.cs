using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    private float myAlpha = 0f;
    [SerializeField] private bool fadeIn;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float musicFadeSpeed;

    [SerializeField] UnityEvent FadeInEvent;
    [SerializeField] UnityEvent FadeOutEvent;

    private bool gamepaused = false;
    // Start is called before the first frame update
    void Start()
    {
        if (fadeIn)
        {
            myAlpha = 0f;
        }
        
        else
        {
            //Time.timeScale = 0;
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
                this.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, myAlpha);

                GameObject[] musics = GameObject.FindGameObjectsWithTag("Music");
                foreach (GameObject music in musics)
                {
                    music.GetComponent<AudioSource>().volume = music.GetComponent<AudioSource>().volume -= musicFadeSpeed;
                }
            }
            else
            {
                FadeInEvent.Invoke();
            }
        }

        //black fade out
        else
        {
            if (!gamepaused)
            {
                GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
                foreach(GameObject camera in cameras)
                {
                    if (camera.activeSelf)
                    {
                        Time.timeScale = 0;
                        gamepaused = true;
                    }
                }
            }

            
            if (myAlpha > 0f)
            {
                myAlpha -= fadeSpeed;
                this.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, myAlpha);
            }
            else
            {
                FadeOutEvent.Invoke();
                Time.timeScale = 1;
                this.gameObject.SetActive(false);
            }
        }
    }
}
