using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeScript : MonoBehaviour
{
    public Image img;
    private float i;
    [SerializeField] private float fadeSpeed = 5.0f;

    public Camera myCam;
    private float j;
    [SerializeField] private Vector3 newCamPos;
    [SerializeField] private float camSpeed = 5.0f;
    [SerializeField] private float newCamSize = 5.0f;

    private bool invoked1 = false;
    [SerializeField] UnityEvent IntroEvent1;

    [SerializeField] private bool isEnding;

    // Start is called before the first frame update
    private void Start()
    {
        i = 1;
        j = myCam.orthographicSize;
        //print(myCam.transform.position);
        //newCamPos = new Vector3(2.0f, 2.0f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (invoked1)
        {
            return;
        }
        img.color = new Color(0, 0, 0, i);
        i -= Time.deltaTime / fadeSpeed;

        if (i < 0)
        {
            if (isEnding && !invoked1)
            {
                IntroEvent1.Invoke();
                invoked1 = true;
                return;
            }
            if (j > newCamSize) {
                myCam.orthographicSize = j;
                myCam.transform.position = Vector3.Lerp(myCam.transform.position, newCamPos, Time.deltaTime);

                j -= Time.deltaTime * camSpeed;
            }

            else
            {
                if (!invoked1) {
                    IntroEvent1.Invoke();
                    invoked1 = true;
                }
            }
        }
    }
}
