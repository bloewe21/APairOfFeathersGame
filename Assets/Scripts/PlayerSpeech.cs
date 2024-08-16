using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeech : MonoBehaviour
{
    [SerializeField] private bool isPlayerOne = true;
    [SerializeField] private GameObject player;
    private bool canSpeakOne = true;
    private bool canSpeakTwo = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOne && canSpeakOne)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                canSpeakOne = false;
                gameObject.GetComponent<Animator>().SetTrigger("count");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                canSpeakOne = false;
                gameObject.GetComponent<Animator>().SetTrigger("up");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                canSpeakOne = false;
                gameObject.GetComponent<Animator>().SetTrigger("down");
            }
        }



        if (!isPlayerOne && canSpeakTwo)
        {
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                canSpeakTwo = false;
                gameObject.GetComponent<Animator>().SetTrigger("count");
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                canSpeakTwo = false;
                gameObject.GetComponent<Animator>().SetTrigger("up");
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                canSpeakTwo = false;
                gameObject.GetComponent<Animator>().SetTrigger("down");
            }
        }


        if (isPlayerOne)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.position = new Vector2(player.transform.position.x - 1.2f, player.transform.position.y + 1.2f);
        }
        if (!isPlayerOne)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.position = new Vector2(player.transform.position.x + 1.2f, player.transform.position.y + 1.2f);
        }
        
    }

    private void CanSpeakOneFunction()
    {
        //print("1");
        gameObject.GetComponent<Animator>().SetTrigger("idle");
        canSpeakOne = true;
    }

    private void CanSpeakTwoFunction()
    {
        gameObject.GetComponent<Animator>().SetTrigger("idle");
        canSpeakTwo = true;
    }
}
