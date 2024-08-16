using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PrefManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;
    [SerializeField] private float defaultVolume;

    [SerializeField] private GameObject musicObject;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField] private TextMeshProUGUI screenText;

    [SerializeField] private GameObject timerText;

    [SerializeField] private bool isCutscene = false;
    // Start is called before the first frame update
    void Start()
    {
        //MUSIC TOGGLE
        if (!PlayerPrefs.HasKey("savedMusicToggle") || PlayerPrefs.GetInt("savedMusicToggle") == 1)
        {
            // musicObject.SetActive(true);
            GameObject[] musics = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject music in musics)
            {
                music.GetComponent<AudioSource>().volume = music.GetComponent<SoundManager>().myDefaultAudio;
            }
            PlayerPrefs.SetInt("savedMusicToggle", 1);
            musicText.GetComponent<TMP_Text>().text = "On";
        }
        else if (PlayerPrefs.GetInt("savedMusicToggle") == 0)
        {
            // musicObject.SetActive(false);
            GameObject[] musics = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject music in musics)
            {
                music.GetComponent<AudioSource>().volume = 0f;
            }
            musicText.GetComponent<TMP_Text>().text = "Off";
        }


        if (isCutscene)
        {
            return;
        }



        //MASTER VOLUME
        if (!PlayerPrefs.HasKey("savedMasterVolume"))
        {
            PlayerPrefs.SetFloat("savedMasterVolume", defaultVolume);
            volumeSlider.value = defaultVolume;
            mixer.SetFloat("MasterVol", Mathf.Log10(defaultVolume) * 20);
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetFloat("savedMasterVolume");
            mixer.SetFloat("MasterVol", Mathf.Log10(PlayerPrefs.GetFloat("savedMasterVolume")) * 20);
        }










        //SCREEN RES
        if (!PlayerPrefs.HasKey("savedScreenRes") || PlayerPrefs.GetInt("savedScreenRes") == 0)
        {
            screenText.GetComponent<TMP_Text>().text = "Full";
        }
        else if (PlayerPrefs.GetInt("savedScreenRes") == 1)
        {
            screenText.GetComponent<TMP_Text>().text = "Large";
        }
        else if (PlayerPrefs.GetInt("savedScreenRes") == 2)
        {
            screenText.GetComponent<TMP_Text>().text = "Small";
        }






        //SHOW TIMER
        if (!PlayerPrefs.HasKey("savedShowTime"))
        {
            PlayerPrefs.SetInt("savedShowTime", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("savedShowTime") == 0)
            {
                timerText.GetComponent<TMP_Text>().enabled = false;
            }
            else if (PlayerPrefs.GetInt("savedShowTime") == 1)
            {
                timerText.GetComponent<TMP_Text>().enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
