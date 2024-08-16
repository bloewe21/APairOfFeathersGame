using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private bool isSinglePlayer;

    [SerializeField] private GameObject masterSlider;
    [SerializeField] private GameObject musicObject;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI screenText;
    [SerializeField] private GameObject timerText;

    private bool isPaused = false;
    private bool isOptions = false;
    private bool moveUp = false;

    [SerializeField] private GameObject cursor;
    [SerializeField] private float[] cursorPosX;
    private int currentCursor;
    private int cursorAmount;

    [SerializeField] private GameObject cursor2;
    [SerializeField] private float[] cursor2PosY;
    private int currentCursor2;
    private int cursorAmount2;

    [SerializeField] UnityEvent OnPauseEvent;
    [SerializeField] UnityEvent OnUnpauseEvent;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject player;

    [SerializeField] private AudioSource menuSound1;
    [SerializeField] private AudioSource menuSound2;
    // Start is called before the first frame update
    void Start()
    {
        currentCursor = 0;
        cursorAmount = cursorPosX.Length;

        currentCursor2 = 0;
        cursorAmount2 = cursor2PosY.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //pause game
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseFunction();
        }
        
        //unpause game
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeFunction();
        }

        //move up pause menu
        if (moveUp && pauseMenu.GetComponent<RectTransform>().localPosition.y < 100)
        {
            pauseMenu.transform.Translate(Vector3.up * .05f);
        }
        //stop moving up
        else
        {
            moveUp = false;
        }


        //move cursor
        if (isPaused && !isOptions)
        {
            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && ((currentCursor+1) < cursorAmount))
            {
                menuSound1.Play();

                currentCursor += 1;
                //cursor.GetComponent<RectTransform>().localPosition.x = cursorPosX[currentCursor];
                cursor.GetComponent<RectTransform>().localPosition = new Vector2(cursorPosX[currentCursor], cursor.GetComponent<RectTransform>().localPosition.y);
            }
            
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && ((currentCursor-1) >= 0))
            {
                menuSound1.Play();

                currentCursor -= 1;
                //cursor.GetComponent<RectTransform>().localPosition.x = cursorPosX[currentCursor];
                cursor.GetComponent<RectTransform>().localPosition = new Vector2(cursorPosX[currentCursor], cursor.GetComponent<RectTransform>().localPosition.y);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                menuSound2.Play();

                if (currentCursor == 0)
                {
                    ResumeFunction();
                }
                else if (currentCursor == 1)
                {
                    OptionsFunction();
                }
                else if (currentCursor == 2)
                {
                    QuitFunction();
                }
            }
        }

        else if (isOptions)
        {
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && ((currentCursor2+1) < cursorAmount2))
            {
                menuSound1.Play();

                currentCursor2 += 1;
                //cursor.GetComponent<RectTransform>().localPosition.x = cursorPosX[currentCursor];
                cursor2.GetComponent<RectTransform>().localPosition = new Vector2(cursor2.GetComponent<RectTransform>().localPosition.x, cursor2PosY[currentCursor2]);
            }

            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && ((currentCursor2-1) >= 0))
            {
                menuSound1.Play();

                currentCursor2 -= 1;
                //cursor.GetComponent<RectTransform>().localPosition.x = cursorPosX[currentCursor];
                cursor2.GetComponent<RectTransform>().localPosition = new Vector2(cursor2.GetComponent<RectTransform>().localPosition.x, cursor2PosY[currentCursor2]);
            }

            if (currentCursor2 == 0)
            {
                MusicSliderFunction();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                menuSound2.Play();

                //music
                if (currentCursor2 == 1)
                {
                    print("music");
                    MusicToggleFunction();
                }

                //screen
                if (currentCursor2 == 2)
                {
                    ScreenResFunction();
                }

                //show timer
                if (currentCursor2 == 3)
                {
                    print("timer");
                    ShowTimeFunction();
                }

                //back
                if (currentCursor2 == 4)
                {
                    isOptions = false;
                    optionsMenu.SetActive(false);
                    currentCursor2 = 0;
                }
            }
        }
    }

    //pause game
    private void PauseFunction()
    {
        isPaused = true;
        OnPauseEvent.Invoke();

        pauseMenu.SetActive(true);
        currentCursor = 0;
        cursor.GetComponent<RectTransform>().localPosition = new Vector2(cursorPosX[0], cursor.GetComponent<RectTransform>().localPosition.y);
        moveUp = true;

        if (!isSinglePlayer)
        {
            player.GetComponent<PlayerMovement2>().isPaused = true;
            AudioSource[] audios2 = FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audios2) {
                if (a.gameObject.tag == "Music") {
                    a.volume /= 2;
                }
            }
            return;
        }

        Time.timeScale = 0;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios) {
            if (a.gameObject.tag == "Music") {
                a.volume /= 2;
            }
            else {
                a.Pause();
            }
        }
    }

    //unpause game
    private void ResumeFunction()
    {
        pauseMenu.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        isPaused = false;
        isOptions = false;
        OnUnpauseEvent.Invoke();
        Time.timeScale = 1;

        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        currentCursor = 0;
        currentCursor2 = 0;

        if (!isSinglePlayer)
        {
            player.GetComponent<PlayerMovement2>().isPaused = false;
            AudioSource[] audios2 = FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audios2) {
                if (a.gameObject.tag == "Music") {
                    a.volume *= 2;
                }
            }
            return;
        }

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios) {
            if (a.gameObject.tag == "Music") {
                a.volume *= 2;
            }
            else {
                a.UnPause();
            }
        }
    }

    private void OptionsFunction()
    {
        //print("options");
        isOptions = true;
        optionsMenu.SetActive(true);
        cursor2.GetComponent<RectTransform>().localPosition = new Vector2(cursor2.GetComponent<RectTransform>().localPosition.x, cursor2PosY[0]);
    }

    private void QuitFunction()
    {
        SceneManager.LoadScene("MainMenu");
    }







    private void MusicSliderFunction()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            menuSound1.Play();

            masterSlider.GetComponent<Slider>().value += 0.4f;
            PlayerPrefs.SetFloat("savedMasterVolume", masterSlider.GetComponent<Slider>().value);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            menuSound1.Play();

            masterSlider.GetComponent<Slider>().value -= 0.4f;
            PlayerPrefs.SetFloat("savedMasterVolume", masterSlider.GetComponent<Slider>().value);
        }
    }


    private void MusicToggleFunction()
    {
        //turn on music
        if (PlayerPrefs.GetInt("savedMusicToggle") == 0)
        {
            //musicObject.SetActive(true);
            // foreach (Transform child in musicObject.transform)
            // {
            //     // / 2.0f since it's currently paused
            //     child.gameObject.GetComponent<AudioSource>().volume = child.GetComponent<SoundManager>().myDefaultAudio / 2.0f;
            // }
            GameObject[] musics = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject music in musics)
            {
                music.GetComponent<AudioSource>().volume = music.GetComponent<SoundManager>().myDefaultAudio / 2.0f;
            }
            PlayerPrefs.SetInt("savedMusicToggle", 1);
            musicText.GetComponent<TMP_Text>().text = "On";
        }
        //turn off music
        else if (PlayerPrefs.GetInt("savedMusicToggle") == 1)
        {
            //musicObject.SetActive(false);
            // foreach (Transform child in musicObject.transform)
            // {
            //     child.gameObject.GetComponent<AudioSource>().volume = 0f;
            // }
            GameObject[] musics = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject music in musics)
            {
                music.GetComponent<AudioSource>().volume = 0f;
            }
            PlayerPrefs.SetInt("savedMusicToggle", 0);
            musicText.GetComponent<TMP_Text>().text = "Off";
        }
    }


    private void ScreenResFunction()
    {
        if (!isSinglePlayer)
        {
            return;
        }
        //transition to full screen
        if (PlayerPrefs.GetInt("savedScreenRes") == 2)
        {
            FullScreenMode fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreenMode = fullScreenMode;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, fullScreenMode, 60);

            PlayerPrefs.SetInt("savedScreenRes", 0);
            screenText.GetComponent<TMP_Text>().text = "Full";
        }
        //transition to big windowed
        else if (!PlayerPrefs.HasKey("savedScreenRes") || PlayerPrefs.GetInt("savedScreenRes") == 0)
        {
            Resolution currentResolution = Screen.currentResolution;
            //Screen.SetResolution(1280, 720, false, 60);
            Screen.SetResolution(Mathf.FloorToInt(currentResolution.width / 1.75f), Mathf.FloorToInt(currentResolution.height / 1.75f), false, 60);

            PlayerPrefs.SetInt("savedScreenRes", 1);
            screenText.GetComponent<TMP_Text>().text = "Large";
        }
        //transition to small windowed
        else if (PlayerPrefs.GetInt("savedScreenRes") == 1)
        {
            Resolution currentResolution = Screen.currentResolution;
            //Screen.SetResolution(960, 540, false, 60);
            Screen.SetResolution(Mathf.FloorToInt(currentResolution.width / 2.5f), Mathf.FloorToInt(currentResolution.height / 2.5f), false, 60);

            PlayerPrefs.SetInt("savedScreenRes", 2);
            screenText.GetComponent<TMP_Text>().text = "Small";
        }
    }

    private void ShowTimeFunction()
    {
        timerText.GetComponent<TMP_Text>().enabled = !timerText.GetComponent<TMP_Text>().enabled;

        if (PlayerPrefs.GetInt("savedShowTime") == 0)
        {
            PlayerPrefs.SetInt("savedShowTime", 1);
        }
        else if (PlayerPrefs.GetInt("savedShowTime") == 0)
        {
            PlayerPrefs.SetInt("savedShowTime", 0);
        }
    }
}
