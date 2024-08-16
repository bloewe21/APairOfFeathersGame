using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScreen()
    {
        if (Screen.fullScreenMode != FullScreenMode.ExclusiveFullScreen)
        {
            FullScreenMode fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreenMode = fullScreenMode;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, fullScreenMode, 60);
        }
        else
        {
            Screen.SetResolution(1280, 720, false, 60);
        }
        
    }
}
