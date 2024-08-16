using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{

    private TMP_Text timeText;
    private float timePassed = 0f;

    public bool isSinglePlayer;
    // Start is called before the first frame update
    void Start()
    {
        //print(PlayerPrefs.GetFloat("singleSavedTime"));
        
        timeText = GetComponent<TMP_Text>();

        if (isSinglePlayer)
        {
            timePassed = PlayerPrefs.GetFloat("singleSavedTime");
        }
        else
        {
            timePassed = PlayerPrefs.GetFloat("multiSavedTime");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //roundedTime = Mathf.Round(timePassed * 10.0f) * 0.1f;
        timePassed += Time.deltaTime;
        DisplayTime(timePassed);

        if (isSinglePlayer)
        {
            PlayerPrefs.SetFloat("singleSavedTime", timePassed);
        }
        else
        {
            PlayerPrefs.SetFloat("multiSavedTime", timePassed);
        }
        
    }



    private void DisplayTime(float timeToDisplay)
    {
        float hours = Mathf.FloorToInt(timeToDisplay / 3600); 
        float minutes = Mathf.FloorToInt((timeToDisplay / 60) % 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 99;
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", hours, minutes, seconds, milliSeconds);


        //timeText.text = string.Format("{0:00}:{1:00}", seconds, milliSeconds);
    }
}