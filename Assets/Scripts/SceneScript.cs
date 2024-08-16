using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI screenText;
    [SerializeField] private bool isCutscene = false;
    //[SerializeField] private int sceneAction;
    // Start is called before the first frame update
    void Start()
    {
        //single player load
        // if (sceneAction == 0)
        // {
        //     SceneManager.LoadScene("IntroSingle");
        // }

        // if (sceneAction == 1)
        // {
        //     SceneManager.LoadScene("MainGame");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sceneToLoad(int sceneNum)
    {
        //single player intro
        if (sceneNum == 0)
        {
            SceneManager.LoadScene("IntroSingle");
        }

        //single player game
        if (sceneNum == 1)
        {
            SceneManager.LoadScene("MainGame");
        }

        if (sceneNum == 2)
        {
            SceneManager.LoadScene("EndingSingle");
        }

        if (sceneNum == 3)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (sceneNum == 4)
        {
            if (PlayerPrefs.GetInt("savedScreenRes") != 0)
            {
                FullScreenMode fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                Screen.fullScreenMode = fullScreenMode;

                Resolution currentResolution = Screen.currentResolution;
                Screen.SetResolution(currentResolution.width, currentResolution.height, fullScreenMode, 60);
            }

            if (!isCutscene)
            {
                screenText.GetComponent<TMP_Text>().text = "Full";
            }
            PlayerPrefs.SetInt("savedScreenRes", 0);

            SceneManager.LoadScene("IntroMulti");
        }

        if (sceneNum == 5)
        {
            if (PlayerPrefs.GetInt("savedScreenRes") != 0)
            {
                FullScreenMode fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                Screen.fullScreenMode = fullScreenMode;

                Resolution currentResolution = Screen.currentResolution;
                Screen.SetResolution(currentResolution.width, currentResolution.height, fullScreenMode, 60);
            }

            if (!isCutscene)
            {
                screenText.GetComponent<TMP_Text>().text = "Full";
            }
            PlayerPrefs.SetInt("savedScreenRes", 0);

            SceneManager.LoadScene("MultiGame");
        }

        if (sceneNum == 6)
        {
            SceneManager.LoadScene("EndingMulti");
        }
    }
}
