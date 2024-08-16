using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatManagerScript : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject heatBar;
    [SerializeField] private GameObject heatChild;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (heatBar.GetComponent<Slider>().value != 0)
        {
            heatChild.SetActive(true);
        }
        else
        {
            heatChild.SetActive(false);
        }
    }
}
