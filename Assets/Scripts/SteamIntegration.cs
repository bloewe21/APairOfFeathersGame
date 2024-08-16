using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SteamIntegration : MonoBehaviour
{
    [SerializeField] private bool isMainMenu = false;
    [SerializeField] private string currentId;
    //[SerializeField] private TMP_Text myText;
    // Start is called before the first frame update
    void Start()
    {
        if (isMainMenu)
        {
            try
            {
                Steamworks.SteamClient.Init(3075170);
                PlayerPrefs.SetInt("steamConnected", 1);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                PlayerPrefs.SetInt("steamConnected", 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //deny inputs
        //return;

        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            IsThisAchievementUnlocked(currentId);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            UnlockAchievement2(currentId);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ClearAchievement(currentId);
        }
        */

        //myText.text = PlayerPrefs.GetString("savedHostName");
    }

    private void PrintName()
    {
        //Debug.Log(Steamworks.SteamClient.Name);
        //PlayerPrefs.SetString("savedHostName", Steamworks.SteamClient.Name);

        var ach = new Steamworks.Data.Achievement("ACH_CAVE");
        if (!ach.State)
        {
            print("nogo");
        }
    }

    //[Button]
    public void IsThisAchievementUnlocked(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);

        Debug.Log($"Achievement {id} status: " + ach.State);
    }

    //[Button]
    public void UnlockAchievement2(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        ach.Trigger();

        Debug.Log($"Achievement {id} unlocked");
    }

    //[Button]
    public void ClearAchievement(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        ach.Clear();

        Debug.Log($"Achievement {id} cleared");
    }


}
