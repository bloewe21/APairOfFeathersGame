using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAnims : MonoBehaviour
{
    [SerializeField] private int bgAnimNumber;

    private float startingXPos;
    private float startingYPos;
    // Start is called before the first frame update
    void Start()
    {
        startingXPos = transform.position.x;
        startingYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (bgAnimNumber == 1)
        {
            transform.Translate(Vector3.left * 2.0f * Time.deltaTime);
            if (transform.position.x < -50)
            {
                transform.position = new Vector2(startingXPos, transform.position.y);
            }
        }
    }
}
