using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveVelocity : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float x_position;
    [SerializeField] private float y_position;

    [SerializeField] private float x_velocity;
    [SerializeField] private float y_velocity;

    public bool isSinglePlayer;



    // Start is called before the first frame update
    void Start()
    {
        //GAME STARTS PAUSED
        // Time.timeScale = 0;

        rb = GetComponent<Rigidbody2D>();


        //first boot up
        if (isSinglePlayer && !PlayerPrefs.HasKey("singleSavedXPosition"))
        {
            transform.position = new Vector2(x_position, y_position);
            return;
        }

        if (!isSinglePlayer && !PlayerPrefs.HasKey("multiSavedXPosition"))
        {
            transform.position = new Vector2(x_position, y_position);
            return;
        }



        if (isSinglePlayer)
        {
            x_position = PlayerPrefs.GetFloat("singleSavedXPosition");
            y_position = PlayerPrefs.GetFloat("singleSavedYPosition");

            transform.position = new Vector2(x_position, y_position);

            x_velocity = PlayerPrefs.GetFloat("singleSavedXVelocity");
            y_velocity = PlayerPrefs.GetFloat("singleSavedYVelocity");

            rb.velocity = new Vector2(x_velocity, y_velocity);
        }

        else
        {
            x_position = PlayerPrefs.GetFloat("multiSavedXPosition");
            y_position = PlayerPrefs.GetFloat("multiSavedYPosition");

            transform.position = new Vector2(x_position, y_position);

            x_velocity = PlayerPrefs.GetFloat("multiSavedXVelocity");
            y_velocity = PlayerPrefs.GetFloat("multiSavedYVelocity");

            rb.velocity = new Vector2(x_velocity, y_velocity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinglePlayer)
        {
            PlayerPrefs.SetFloat("singleSavedXPosition", transform.position.x);
            PlayerPrefs.SetFloat("singleSavedYPosition", transform.position.y);

            PlayerPrefs.SetFloat("singleSavedXVelocity", rb.velocity.x);
            PlayerPrefs.SetFloat("singleSavedYVelocity", rb.velocity.y);
        }

        else
        {
            PlayerPrefs.SetFloat("multiSavedXPosition", transform.position.x);
            PlayerPrefs.SetFloat("multiSavedYPosition", transform.position.y);

            PlayerPrefs.SetFloat("multiSavedXVelocity", rb.velocity.x);
            PlayerPrefs.SetFloat("multiSavedYVelocity", rb.velocity.y);
        }
    }
}
