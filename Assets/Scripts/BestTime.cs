using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BestTime : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("singleBestTime") && !PlayerPrefs.HasKey("multiBestTime"))
        {
            DisplayTime(PlayerPrefs.GetFloat("singleBestTime"));
        }

        if (!PlayerPrefs.HasKey("singleBestTime") && PlayerPrefs.HasKey("multiBestTime"))
        {
            DisplayTime(PlayerPrefs.GetFloat("multiBestTime"));
        }

        if (PlayerPrefs.HasKey("singleBestTime") && PlayerPrefs.HasKey("multiBestTime"))
        {
            if (PlayerPrefs.GetFloat("singleBestTime") < PlayerPrefs.GetFloat("multiBestTime"))
            {
                DisplayTime(PlayerPrefs.GetFloat("singleBestTime"));
            }
            else
            {
                DisplayTime(PlayerPrefs.GetFloat("multiBestTime"));
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
