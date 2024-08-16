using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyepatchManager : MonoBehaviour
{
    [SerializeField] private GameObject eyepatch;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("savedWin"))
        {
            eyepatch.SetActive(true);
        }
        else
        {
            eyepatch.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
