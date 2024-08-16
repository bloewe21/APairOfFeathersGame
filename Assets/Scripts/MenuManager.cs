using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject masterSlider;

    private bool isOptions = false;
    private bool isSure = false;
    private bool isSure2 = false;
    private bool sureSingle = true;
    [SerializeField] private TextMeshProUGUI singleContinueText;
    [SerializeField] private TextMeshProUGUI multiContinueText;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject sureMenu;
    [SerializeField] private GameObject sureMenu2;
    [SerializeField] private GameObject multiplayerTip;
    [SerializeField] private GameObject blackFadeSingleIntro;
    [SerializeField] private GameObject blackFadeSingleGame;
    [SerializeField] private GameObject blackFadeMultiIntro;
    [SerializeField] private GameObject blackFadeMultiGame;
    [SerializeField] private GameObject musicObject;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI screenText;
    [SerializeField] private GameObject resetAllObject;

    [SerializeField] private GameObject cursor;
    [SerializeField] private float[] cursorPosY;
    private int currentCursor;
    private int cursorAmount;

    [SerializeField] private GameObject cursor2;
    [SerializeField] private float[] cursorPosY2;
    private int currentCursor2;
    private int cursorAmount2;

    [SerializeField] private GameObject cursor3;
    [SerializeField] private float[] cursorPosX3;
    private int currentCursor3;
    private int cursorAmount3;

    [SerializeField] private GameObject cursor4;
    [SerializeField] private float[] cursorPosX4;
    private int currentCursor4;
    private int cursorAmount4;

    [SerializeField] private AudioSource menuSound1;
    [SerializeField] private AudioSource menuSound2;
    // Start is called before the first frame update
    void Start()
    {
        currentCursor = 0;
        cursorAmount = cursorPosY.Length;

        currentCursor2 = 0;
        cursorAmount2 = cursorPosY2.Length;

        currentCursor3 = 0;
        cursorAmount3 = cursorPosX3.Length;

        currentCursor4 = 0;
        cursorAmount4 = cursorPosX4.Length;

        cursor.GetComponent<RectTransform>().localPosition = new Vector2(cursor.GetComponent<RectTransform>().localPosition.x, cursorPosY[currentCursor]);
        cursor2.GetComponent<RectTransform>().localPosition = new Vector2(cursor2.GetComponent<RectTransform>().localPosition.x, cursorPosY2[currentCursor2]);
        cursor3.GetComponent<RectTransform>().localPosition = new Vector2(cursorPosX3[currentCursor3], cursor3.GetComponent<RectTransform>().localPosition.y);
        cursor4.GetComponent<RectTransform>().localPosition = new Vector2(cursorPosX4[currentCursor4], cursor4.GetComponent<RectTransform>().localPosition.y);


        //grey continue
        //if dont have a singleplayer save file
        if (!PlayerPrefs.HasKey("singleSavedTime"))
        {
            singleContinueText.GetComponent<TMP_Text>().color = new Color32(135, 135, 135, 255);
        }
        //if do
        else
        {
            singleContinueText.GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
        }

        if (!PlayerPrefs.HasKey("multiSavedTime"))
        {
            multiContinueText.GetComponent<TMP_Text>().color = new Color32(135, 135, 135, 255);
        }
        //if do
        else
        {
            multiContinueText.GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //main menu
        if (!isOptions && !isSure)
        {
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && ((currentCursor+1) < cursorAmount))
            {
                menuSound1.Play();

                currentCursor += 1;
                cursor.GetComponent<RectTransform>().localPosition = new Vector2(cursor.GetComponent<RectTransform>().localPosition.x, cursorPosY[currentCursor]);
            }

            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && ((currentCursor-1) >= 0))
            {
                menuSound1.Play();

                currentCursor -= 1;
                cursor.GetComponent<RectTransform>().localPosition = new Vector2(cursor.GetComponent<RectTransform>().localPosition.x, cursorPosY[currentCursor]);
            }

            if (currentCursor == 2 || currentCursor == 3)
            {
                multiplayerTip.SetActive(true);
            }
            else
            {
                multiplayerTip.SetActive(false);
            }
        }

        //options menu
        else
        {
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && ((currentCursor2+1) < cursorAmount2))
            {
                menuSound1.Play();

                currentCursor2 += 1;
                cursor2.GetComponent<RectTransform>().localPosition = new Vector2(cursor2.GetComponent<RectTransform>().localPosition.x, cursorPosY2[currentCursor2]);
            }

            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && ((currentCursor2-1) >= 0))
            {
                menuSound1.Play();

                currentCursor2 -= 1;
                cursor2.GetComponent<RectTransform>().localPosition = new Vector2(cursor2.GetComponent<RectTransform>().localPosition.x, cursorPosY2[currentCursor2]);
            }

            //master slider
            if (currentCursor2 == 0)
            {
                MusicSliderFunction();
            }
        }

        //sure menu
        if (isSure)
        {
            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && ((currentCursor3+1) < cursorAmount3))
            {
                menuSound1.Play();

                currentCursor3 += 1;
                cursor3.GetComponent<RectTransform>().localPosition = new Vector2(cursorPosX3[currentCursor3], cursor3.GetComponent<RectTransform>().localPosition.y);
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && ((currentCursor3-1) >= 0))
            {
                menuSound1.Play();

                currentCursor3 -= 1;
                cursor3.GetComponent<RectTransform>().localPosition = new Vector2(cursorPosX3[currentCursor3], cursor3.GetComponent<RectTransform>().localPosition.y);
            }
        }

        //sure2 menu
        if (isSure2)
        {
            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && ((currentCursor4+1) < cursorAmount4))
            {
                menuSound1.Play();

                currentCursor4 += 1;
                cursor4.GetComponent<RectTransform>().localPosition = new Vector2(cursorPosX4[currentCursor4], cursor4.GetComponent<RectTransform>().localPosition.y);
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && ((currentCursor4-1) >= 0))
            {
                menuSound1.Play();

                currentCursor4 -= 1;
                cursor4.GetComponent<RectTransform>().localPosition = new Vector2(cursorPosX4[currentCursor4], cursor4.GetComponent<RectTransform>().localPosition.y);
            }
        }

        //pressing space in main menu
        if (Input.GetKeyDown(KeyCode.Space) && !isOptions && !isSure && !isSure2)
        {
            menuSound2.Play();

            //singeplayer continue
            if (currentCursor == 0)
            {
                SingleContinueFunction();
            }

            //singleplayer new game
            if (currentCursor == 1)
            {
                SingleNewFunction();
            }

            //multiplayer continue
            if (currentCursor == 2)
            {
                MultiContinueFunction();
            }

            //multiplayer new game
            if (currentCursor == 3)
            {
                MultiNewFunction();
            }

            //options
            if (currentCursor == 4)
            {
                isOptions = true;
                optionsMenu.SetActive(true);
            }

            //quit
            if (currentCursor == 5)
            {
                Application.Quit();
            }
        }

        //pressing space in options
        else if (Input.GetKeyDown(KeyCode.Space) && isOptions)
        {
            menuSound2.Play();

            //music
            if (currentCursor2 == 1)
            {
                MusicToggleFunction();
            }

            //screen
            if (currentCursor2 == 2)
            {
                ScreenResFunction();
            }

            //reset all
            if (currentCursor2 == 3)
            {
                isSure2 = true;
                isOptions = false;
                sureMenu2.SetActive(true);
                //resetAllObject.SetActive(true);
            }

            //back
            if (currentCursor2 == 4)
            {
                isOptions = false;
                currentCursor2 = 0;
                cursor2.GetComponent<RectTransform>().localPosition = new Vector2(cursor2.GetComponent<RectTransform>().localPosition.x, cursorPosY2[currentCursor2]);
                optionsMenu.SetActive(false);
            }
        }

        //pressing space in sure
        else if (Input.GetKeyDown(KeyCode.Space) && isSure)
        {
            menuSound2.Play();

            //yes
            if (currentCursor3 == 0)
            {
                if (sureSingle)
                {
                    DeleteSingleKeys();
                    blackFadeSingleIntro.SetActive(true);
                    this.gameObject.SetActive(false);
                }
                else
                {
                    DeleteMultiKeys();
                    blackFadeMultiIntro.SetActive(true);
                    this.gameObject.SetActive(false);
                }
            }

            //no
            if (currentCursor3 == 1)
            {
                isSure = false;
                currentCursor3 = 0;
                cursor3.GetComponent<RectTransform>().localPosition = new Vector2(cursorPosX3[currentCursor3], cursor3.GetComponent<RectTransform>().localPosition.y);
                sureMenu.SetActive(false);
            }
        }

        //pressing space in sure2
        else if (Input.GetKeyDown(KeyCode.Space) && isSure2)
        {
            menuSound2.Play();
            
            //yes
            if (currentCursor4 == 0)
            {
                resetAllObject.SetActive(true);
            }

            //no
            if (currentCursor4 == 1)
            {
                isSure2 = false;
                isOptions = true;
                currentCursor4 = 0;
                cursor4.GetComponent<RectTransform>().localPosition = new Vector2(cursorPosX4[currentCursor4], cursor4.GetComponent<RectTransform>().localPosition.y);
                sureMenu2.SetActive(false);
            }
        }
    }





    

    private void SingleContinueFunction()
    {
        if (PlayerPrefs.HasKey("singleSavedTime"))
        {
            //SceneManager.LoadScene("MainGame");
            blackFadeSingleGame.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            //play boo womp sound effect
        }
    }

    private void SingleNewFunction()
    {
        if (!PlayerPrefs.HasKey("singleSavedTime"))
        {
            //SceneManager.LoadScene("MainGame");
            blackFadeSingleIntro.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.HasKey("singleSavedTime"))
        {
            sureSingle = true;
            sureMenu.SetActive(true);
            isSure = true;
        }
    }

    private void MultiContinueFunction()
    {
        if (PlayerPrefs.HasKey("multiSavedTime"))
        {
            //SceneManager.LoadScene("MainGame");
            blackFadeMultiGame.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            //play boo womp sound effect
        }
    }

    private void MultiNewFunction()
    {
        if (!PlayerPrefs.HasKey("multiSavedTime"))
        {
            //SceneManager.LoadScene("MainGame");
            blackFadeMultiIntro.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.HasKey("multiSavedTime"))
        {
            sureSingle = false;
            sureMenu.SetActive(true);
            isSure = true;
        }
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
            //     child.gameObject.GetComponent<AudioSource>().volume = child.GetComponent<SoundManager>().myDefaultAudio;
            // }
            GameObject[] musics = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject music in musics)
            {
                music.GetComponent<AudioSource>().volume = music.GetComponent<SoundManager>().myDefaultAudio;
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
