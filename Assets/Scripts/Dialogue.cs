using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textComponent;
    public string[] lines;
    public GameObject[] portraits;
    [SerializeField] private float textSpeed;
    private int index;

    [SerializeField] private AudioSource typeSound;

    [SerializeField] UnityEvent EndDialogueEvent;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        portraits[0].SetActive(true);
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == lines[index])
            {   
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    private void StartDialogue()
    {
        index = 0;

        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            if (c != ' ')
            {
                typeSound.Play();
            }
            else
            {
                //yield return new WaitForSeconds(textSpeed);
            }

            if (c == '?')
            {
                typeSound.pitch *= 2.0f;
                yield return new WaitForSeconds(textSpeed * 4);
            }
            if (c == '!')
            {
                typeSound.volume *= 2.0f;
                yield return new WaitForSeconds(textSpeed * 4);
            }
            if (c == '.')
            {
                yield return new WaitForSeconds(textSpeed * 4);
            }
            typeSound.pitch = typeSound.GetComponent<SoundManager>().myDefaultPitch;
            typeSound.volume = typeSound.GetComponent<SoundManager>().myDefaultAudio;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;

            for(int i = 0; i <= portraits.Length - 1; i++)
            {
                portraits[i].SetActive(false);
            }
            portraits[index].SetActive(true);

            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogueEvent.Invoke();
            //gameObject.SetActive(false);
        }
    }
}
