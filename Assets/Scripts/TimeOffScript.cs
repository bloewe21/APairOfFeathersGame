using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeOffScript : MonoBehaviour
{
    [SerializeField] private GameObject timerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            timerText.GetComponent<TMP_Text>().enabled = !timerText.GetComponent<TMP_Text>().enabled;
        }
    }
}
