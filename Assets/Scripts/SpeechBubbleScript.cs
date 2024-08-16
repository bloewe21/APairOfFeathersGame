using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleScript : MonoBehaviour
{
    [SerializeField] private bool isSinglePlayer;
    //[SerializeField] private float[] speechHeights;
    [SerializeField] List <float> speechHeights;
    [SerializeField] List <GameObject> speechTexts;
    [SerializeField] List <float> speechTimes;

    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (isSinglePlayer)
        {
            if (!PlayerPrefs.HasKey("singleSavedBubbles"))
            {
                PlayerPrefs.SetInt("singleSavedBubbles", 0);
            }

            for (int i = 0; i < PlayerPrefs.GetInt("singleSavedBubbles"); i++)
            {
                speechHeights.RemoveAt(0);
                speechTexts.RemoveAt(0);
                speechTimes.RemoveAt(0);
            }
        }

        else
        {
            if (!PlayerPrefs.HasKey("multiSavedBubbles"))
            {
                PlayerPrefs.SetInt("multiSavedBubbles", 0);
            }

            for (int i = 0; i < PlayerPrefs.GetInt("multiSavedBubbles"); i++)
            {
                speechHeights.RemoveAt(0);
                speechTexts.RemoveAt(0);
                speechTimes.RemoveAt(0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (speechHeights.Count < 1)
        {
            return;
        }
        if (player.transform.position.y > speechHeights[0])
        {
            if (isSinglePlayer)
            {
                PlayerPrefs.SetInt("singleSavedBubbles", PlayerPrefs.GetInt("singleSavedBubbles") + 1);
            }
            else
            {
                PlayerPrefs.SetInt("multiSavedBubbles", PlayerPrefs.GetInt("multiSavedBubbles") + 1);
            }
            speechHeights.RemoveAt(0);
            StartCoroutine(SpeechRoutine());
        }
    }

    private IEnumerator SpeechRoutine()
    {
        yield return new WaitForSeconds(.1f);
        //speechBubble.SetActive(true);
        speechBubble.GetComponent<Animator>().SetTrigger("start");

        yield return new WaitForSeconds(1f);

        speechTexts[0].SetActive(true);

        yield return new WaitForSeconds(speechTimes[0]);

        //speechBubble.SetActive(true);
        speechTexts[0].SetActive(false);
        speechTexts.RemoveAt(0);
        speechTimes.RemoveAt(0);
        speechBubble.GetComponent<Animator>().SetTrigger("end");

        yield return new WaitForSeconds(2f);

        speechBubble.GetComponent<Animator>().SetTrigger("idle");
        //yield return new WaitForSeconds(.5f);
        //speechBubble.SetActive(false);
    }
}
