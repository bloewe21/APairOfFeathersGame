using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeletePrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //reset to full screen
        if (PlayerPrefs.HasKey("savedScreenRes") && PlayerPrefs.GetInt("savedScreenRes") != 0)
        {
            FullScreenMode fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreenMode = fullScreenMode;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, fullScreenMode, 60);
        }

        //if statement for gold feathers...
        
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
