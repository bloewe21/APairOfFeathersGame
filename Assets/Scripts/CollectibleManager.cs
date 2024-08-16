using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] private bool isSinglePlayer;
    [SerializeField] private GameObject[] featherCollectibles;
    [SerializeField] private GameObject leftFeathers;
    [SerializeField] private GameObject rightFeathers;
    [SerializeField] private Material goldFeatherMat;
    [SerializeField] private GameObject allFeathersText;

    //private bool steamConnected = false;
    private int featherTotal = 0;
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

        if (isSinglePlayer && !PlayerPrefs.HasKey("singleFeatherCount"))
        {
            PlayerPrefs.SetInt("singleFeatherCount", 1);
        }

        else if (!isSinglePlayer && !PlayerPrefs.HasKey("multiFeatherCount"))
        {
            PlayerPrefs.SetInt("multiFeatherCount", 1);
        }

        if (isSinglePlayer)
        {
            for (int i = 1; i < 7; i++)
            {
                if (PlayerPrefs.HasKey("singleFeather" + i))
                {
                    Destroy(featherCollectibles[i-1]);
                    featherTotal += 1;
                }
            }
        }

        else
        {
            for (int i = 1; i < 7; i++)
            {
                if (PlayerPrefs.HasKey("multiFeather" + i))
                {
                    Destroy(featherCollectibles[i-1]);
                    featherTotal += 1;
                }
            }
        }

        if (featherTotal >= 6)
        {
            //change feathers to golden
            //print("golden mode");
            leftFeathers.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = goldFeatherMat;
            rightFeathers.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = goldFeatherMat;
            PlayerPrefs.SetInt("savedGolden", 1);
        }

        if (PlayerPrefs.HasKey("savedGolden"))
        {
            leftFeathers.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = goldFeatherMat;
            rightFeathers.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = goldFeatherMat;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupFunction(string objectName)
    {
        if (isSinglePlayer)
        {
            if (objectName == "Collectible1")
            {
                PlayerPrefs.SetInt("singleFeather1", 1);
            }
            if (objectName == "Collectible2")
            {
                PlayerPrefs.SetInt("singleFeather2", 1);
            }
            if (objectName == "Collectible3")
            {
                PlayerPrefs.SetInt("singleFeather3", 1);
            }
            if (objectName == "Collectible4")
            {
                PlayerPrefs.SetInt("singleFeather4", 1);
            }
            if (objectName == "Collectible5")
            {
                PlayerPrefs.SetInt("singleFeather5", 1);
            }
            if (objectName == "Collectible6")
            {
                PlayerPrefs.SetInt("singleFeather6", 1);
            }

            featherTotal += 1;
            if (featherTotal >= 6)
            {
                leftFeathers.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = goldFeatherMat;
                rightFeathers.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = goldFeatherMat;
                
                if (!PlayerPrefs.HasKey("savedGolden"))
                {
                    StartCoroutine(AllFeathersRoutine());
                }
                PlayerPrefs.SetInt("savedGolden", 1);
            }

            PlayerPrefs.SetInt("singleSavedFeathers", PlayerPrefs.GetInt("singleSavedFeathers") + 1);
        }

        else
        {
            if (objectName == "Collectible1")
            {
                PlayerPrefs.SetInt("multiFeather1", 1);
            }
            if (objectName == "Collectible2")
            {
                PlayerPrefs.SetInt("multiFeather2", 1);
            }
            if (objectName == "Collectible3")
            {
                PlayerPrefs.SetInt("multiFeather3", 1);
            }
            if (objectName == "Collectible4")
            {
                PlayerPrefs.SetInt("multiFeather4", 1);
            }
            if (objectName == "Collectible5")
            {
                PlayerPrefs.SetInt("multiFeather5", 1);
            }
            if (objectName == "Collectible6")
            {
                PlayerPrefs.SetInt("multiFeather6", 1);
            }

            featherTotal += 1;
            if (featherTotal >= 6)
            {
                leftFeathers.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = goldFeatherMat;
                rightFeathers.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = goldFeatherMat;
                
                if (!PlayerPrefs.HasKey("savedGolden"))
                {
                    StartCoroutine(AllFeathersRoutine());
                }
                PlayerPrefs.SetInt("savedGolden", 1);
            }

            PlayerPrefs.SetInt("multiSavedFeathers", PlayerPrefs.GetInt("multiSavedFeathers") + 1);
        }
        
    }

    private IEnumerator AllFeathersRoutine()
    {
        if (PlayerPrefs.GetInt("steamConnected") == 1)
        {
            var ach = new Steamworks.Data.Achievement("ACH_GOLDEN");
            if (!ach.State)
            {
                ach.Trigger();
            }
        }

        allFeathersText.SetActive(true);
        yield return new WaitForSeconds(5f);
        allFeathersText.SetActive(false);
    }
}
