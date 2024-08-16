using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;

    private float timeRemaining = 3f;
    private float timeToDisplay;
    [SerializeField] private GameObject countdownObject;
    private TMP_Text countdownText;

    private bool inCannon = false;
    private float cannonRot;

    [SerializeField] private bool isGolden;

    private float launchSpeedX;
    [SerializeField] private float launchSpeedXMod = .4f;
    [SerializeField] private float launchSpeedY = 30f;
    [SerializeField] private float defaultLaunchSpeedY = 30f;
    [SerializeField] private float defaultRot = 0f;

    [SerializeField] private AudioSource lightSound;
    [SerializeField] private AudioSource shootSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        countdownText = countdownObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inCannon)
        {
            //3 2 1 timer
            timeRemaining -= Time.deltaTime;
            timeToDisplay = Mathf.Ceil(timeRemaining);
            
            //timeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
            if (timeToDisplay > 0)
            {
                countdownText.text = timeToDisplay.ToString();
            }


            if (isGolden)
            {
                return;
            }
            //player.transform.position = transform.position;

            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && cannonRot < 90)
            {
                cannonRot += 2f;
            }
            else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && cannonRot > -90)
            {
                cannonRot -= 2f;
            }
            transform.eulerAngles = new Vector3(0f, 0f, cannonRot);

            launchSpeedY = defaultLaunchSpeedY - Mathf.Abs(cannonRot) / 4.5f;
        }

        else
        {
            // countdownObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            lightSound.Play();

            countdownObject.SetActive(true);
            countdownObject.transform.position = new Vector2(transform.position.x + 2.7f, transform.position.y - 0.5f);
            timeRemaining = 3f;

            cannonRot = defaultRot;
            inCannon = true;
            launchSpeedY = defaultLaunchSpeedY;
            StartCoroutine(cannonRoutine());
            //player.transform.position = transform.position;
        }
    }

    private IEnumerator cannonRoutine()
    {
        player.transform.position = transform.position;
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -.1f);
        player.GetComponent<PlayerMovement2>().freezeInputs();
        this.GetComponent<CircleCollider2D>().enabled = false;

        yield return new WaitForSeconds(3f);

        inCannon = false;
        //default gravity scale (2f)
        player.GetComponent<Rigidbody2D>().gravityScale = 2f;

        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * launchSpeedY, ForceMode2D.Impulse);

        launchSpeedX = launchSpeedXMod * Mathf.Abs(cannonRot);
        if (cannonRot >= 0f)
        {
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * launchSpeedX, ForceMode2D.Impulse);
        }
        else
        {
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * launchSpeedX, ForceMode2D.Impulse);
        }

        shootSound.Play();

        countdownObject.SetActive(false);

        yield return new WaitForSeconds(.5f);

        //reset cannon angle, hitbox
        transform.eulerAngles = new Vector3(0f, 0f, defaultRot);
        this.GetComponent<CircleCollider2D>().enabled = true;
    }
}
