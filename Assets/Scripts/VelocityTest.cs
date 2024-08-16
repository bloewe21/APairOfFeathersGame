using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTest : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float x_position;
    [SerializeField] private float y_position;

    [SerializeField] private float x_velocity;
    [SerializeField] private float y_velocity;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        x_position = PlayerPrefs.GetFloat("savedXPosition");
        y_position = PlayerPrefs.GetFloat("savedYPosition");

        transform.position = new Vector2(x_position, y_position);

        x_velocity = PlayerPrefs.GetFloat("savedXVelocity");
        y_velocity = PlayerPrefs.GetFloat("savedYVelocity");

        rb.velocity = new Vector2(x_velocity, y_velocity);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("savedXPosition", transform.position.x);
        PlayerPrefs.SetFloat("savedYPosition", transform.position.y);

        PlayerPrefs.SetFloat("savedXVelocity", rb.velocity.x);
        PlayerPrefs.SetFloat("savedYVelocity", rb.velocity.y);
    }
}
