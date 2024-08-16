using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    private bool playerTouch = false;
    [SerializeField] private GameObject player;
    private float rotationFloat = 0f;

    [SerializeField] private GameObject timerObject;
    [SerializeField] private GameObject blackFadeIn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTouch)
        {
            //player.transform.Translate(transform.position * Time.deltaTime);
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, 5f * Time.deltaTime);
            player.GetComponent<Rigidbody2D>().gravityScale = 0f;
            player.transform.eulerAngles = new Vector3(0f, 0f, rotationFloat);
            rotationFloat -= 3f;

            if (player.transform.position == transform.position)
            {
                //player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, .01f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            //player.GetComponent<PlayerMovement2>().freezeInputs();
            player.GetComponent<PlayerMovement2>().enabled = false;
            playerTouch = true;

            timerObject.GetComponent<TimerScript>().enabled = false;
            blackFadeIn.SetActive(true);

            PlayerPrefs.SetInt("savedSingleWin", 1);
        }
    }
}
