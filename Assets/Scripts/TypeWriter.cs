using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private string fullText;
    private string currentText = "";

    private char[] punctuations = {'.', '?', '!'};

    [SerializeField] private AudioSource typeSound;

    [SerializeField] private bool playSound = true;
    // Start is called before the first frame update
    void Start()
    {
        // punctuations = new char[] {'.', '!', '?'};
        // if (punctuations.Contains('?'))
        // {
        //     //print("YAHOOOOO");
        // }
        //print(punctuations[1]);
        //this.GetComponent<TextMeshProUGUI>().text = "";
        //currentText = "";
        //StartCoroutine(ShowText());
    }

    private void OnEnable()
    {
        this.GetComponent<TextMeshProUGUI>().text = "";
        currentText = "";
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private IEnumerator ShowText()
    {
        for (int i = 1; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);

            if (currentText[currentText.Length - 1] == '?')
            {
                typeSound.pitch *= 2.0f;
            }
            if (currentText[currentText.Length - 1] == '!')
            {
                typeSound.volume *= 2.0f;
            }
            this.GetComponent<TextMeshProUGUI>().text = currentText;
            if (currentText.Length > 1 && currentText[currentText.Length - 1] != ' ')
            {
                if (playSound)
                {
                    typeSound.Play();
                }
                
            }
            else
            {
                yield return new WaitForSeconds(delay / 4.0f);
            }
            if (punctuations.Contains(currentText[currentText.Length - 1]))
            {
                yield return new WaitForSeconds(delay * 4.0f);
            }
            yield return new WaitForSeconds(delay);
            typeSound.pitch = typeSound.GetComponent<SoundManager>().myDefaultPitch;
            typeSound.volume = typeSound.GetComponent<SoundManager>().myDefaultAudio;
        }
    }
}
