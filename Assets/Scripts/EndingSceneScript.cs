using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingSceneScript : MonoBehaviour
{
    [SerializeField] private bool isSinglePlayer;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject opaqueBlack;
    [SerializeField] private GameObject fadeInBlack;
    [SerializeField] private GameObject endingStats;

    [SerializeField] private GameObject timeText;
    [SerializeField] private GameObject rightsText;
    [SerializeField] private GameObject leftsText;
    [SerializeField] private GameObject stunsText;

    [SerializeField] private AudioSource menuSound1;
    [SerializeField] private AudioSource menuSound2;

    private GameObject myChild;

    private bool endingDone = false;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(3075170);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }



        PlayerPrefs.SetInt("savedWin", 1);


        if (PlayerPrefs.GetInt("steamConnected") == 1)
        {
            var ach = new Steamworks.Data.Achievement("ACH_WIN");
            if (!ach.State)
            {
                ach.Trigger();
            }
        }

        myChild = this.gameObject.transform.GetChild(0).gameObject;
        if (PlayerPrefs.GetInt("savedMusicToggle") == 0)
        {
            myChild.GetComponent<AudioSource>().volume = 0f;
        }
        //player.GetComponent<Animator>().SetTrigger("stunned");
        StartCoroutine(EndingRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (endingDone && Input.GetKeyDown(KeyCode.Space))
        {
            if (isSinglePlayer)
            {
            //if don't have a best time
                if (!PlayerPrefs.HasKey("singleBestTime"))
                {
                    PlayerPrefs.SetFloat("singleBestTime", PlayerPrefs.GetFloat("singleSavedTime"));
                }
                else
                {
                    //if new time better than best time
                    if (PlayerPrefs.GetFloat("singleSavedTime") < PlayerPrefs.GetFloat("singleBestTime"))
                    {
                        PlayerPrefs.SetFloat("singleBestTime", PlayerPrefs.GetFloat("singleSavedTime"));
                    }
                }
                DeleteSingleKeys();
            }

            //decided to save and display best time anyways
            else
            {
                if (!PlayerPrefs.HasKey("multiBestTime"))
                {
                    PlayerPrefs.SetFloat("multiBestTime", PlayerPrefs.GetFloat("multiSavedTime"));
                }
                else
                {
                    //if new time better than best time
                    if (PlayerPrefs.GetFloat("multiSavedTime") < PlayerPrefs.GetFloat("multiBestTime"))
                    {
                        PlayerPrefs.SetFloat("multiBestTime", PlayerPrefs.GetFloat("multiSavedTime"));
                    }
                }
                DeleteMultiKeys();
            }

            menuSound2.Play();

            fadeInBlack.SetActive(true);
            endingDone = false;
        }
    }

    private IEnumerator EndingRoutine()
    {
        yield return new WaitForSeconds(3f);
        opaqueBlack.SetActive(true);
        yield return new WaitForSeconds(1f);
        menuSound1.Play();
        endingStats.SetActive(true);

        if (isSinglePlayer)
        {
            DisplayTime(PlayerPrefs.GetFloat("singleSavedTime"));
            rightsText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("singleSavedRights").ToString();
            leftsText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("singleSavedLefts").ToString();
            stunsText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("singleSavedStuns").ToString();
        }

        else
        {
            DisplayTime(PlayerPrefs.GetFloat("multiSavedTime"));
            rightsText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("multiSavedRights").ToString();
            leftsText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("multiSavedLefts").ToString();
            stunsText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("multiSavedStuns").ToString();
        }

        endingDone = true;
    }

    private void DisplayTime(float timeToDisplay)
    {
        float hours = Mathf.FloorToInt(timeToDisplay / 3600); 
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 99;
        timeText.GetComponent<TMP_Text>().text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", hours, minutes, seconds, milliSeconds);
    }

    private void DeleteSingleKeys()
    {
        PlayerPrefs.DeleteKey("singleSavedTime");
        PlayerPrefs.DeleteKey("singleSavedXPosition");
        PlayerPrefs.DeleteKey("singleSavedYPosition");
        PlayerPrefs.DeleteKey("singleSavedXVelocity");
        PlayerPrefs.DeleteKey("singleSavedYVelocity");
        PlayerPrefs.DeleteKey("singleSavedFrozen");
        PlayerPrefs.DeleteKey("singleSavedRights");
        PlayerPrefs.DeleteKey("singleSavedLefts");
        PlayerPrefs.DeleteKey("singleSavedStuns");
        PlayerPrefs.DeleteKey("singleSavedBubbles");

        PlayerPrefs.DeleteKey("singleSavedFeathers");
        PlayerPrefs.DeleteKey("singleFeather1");
        PlayerPrefs.DeleteKey("singleFeather2");
        PlayerPrefs.DeleteKey("singleFeather3");
        PlayerPrefs.DeleteKey("singleFeather4");
        PlayerPrefs.DeleteKey("singleFeather5");
        PlayerPrefs.DeleteKey("singleFeather6");
    }

    private void DeleteMultiKeys()
    {
        PlayerPrefs.DeleteKey("multiSavedTime");
        PlayerPrefs.DeleteKey("multiSavedXPosition");
        PlayerPrefs.DeleteKey("multiSavedYPosition");
        PlayerPrefs.DeleteKey("multiSavedXVelocity");
        PlayerPrefs.DeleteKey("multiSavedYVelocity");
        PlayerPrefs.DeleteKey("multiSavedFrozen");
        PlayerPrefs.DeleteKey("multiSavedRights");
        PlayerPrefs.DeleteKey("multiSavedLefts");
        PlayerPrefs.DeleteKey("multiSavedStuns");
        PlayerPrefs.DeleteKey("multiSavedBubbles");

        PlayerPrefs.DeleteKey("multiSavedFeathers");
        PlayerPrefs.DeleteKey("multiFeather1");
        PlayerPrefs.DeleteKey("multiFeather2");
        PlayerPrefs.DeleteKey("multiFeather3");
        PlayerPrefs.DeleteKey("multiFeather4");
        PlayerPrefs.DeleteKey("multiFeather5");
        PlayerPrefs.DeleteKey("multiFeather6");
    }
}
