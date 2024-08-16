using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("savedMasterVolume"))
        {
            this.gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("savedMasterVolume");
        }
        else
        {
            print("no volume key");
        }
        //this.gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("savedMasterVolume");
        // this.gameObject.GetComponent<Slider>().value = Mathf.Log10(PlayerPrefs.GetFloat("savedMasterVolume")) * 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }
}
