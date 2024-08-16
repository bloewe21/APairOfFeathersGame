using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipCutscene : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject skippingText;
    [SerializeField] private GameObject fadeIn;
    private bool skipped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (skipped)
        {
            slider.value = slider.minValue;
            return;
        }

        
        if (Input.GetKey(KeyCode.Escape))
        {
            slider.value += Time.deltaTime;
            skippingText.SetActive(true);
        }
        else
        {
            slider.value -= Time.deltaTime;
            skippingText.SetActive(false);
        }

        if (slider.value >= slider.maxValue)
        {
            fadeIn.SetActive(true);
            //Destroy(slider);
            Destroy(skippingText);
            skipped = true;
        }
    }
}
