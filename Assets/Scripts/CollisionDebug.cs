using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDebug : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject freezes;
    [SerializeField] private GameObject cavefreezes;
    private bool debugOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //freezes.GetComponent<CompositeCollider2D>().enabled = !freezes.GetComponent<CompositeCollider2D>().enabled;
            if (!debugOn)
            {
                freezes.transform.gameObject.tag = "Untagged";
                cavefreezes.transform.gameObject.tag = "Untagged";
                debugOn = true;
            }
            else
            {
                freezes.transform.gameObject.tag = "Freeze";
                cavefreezes.transform.gameObject.tag = "Freeze";
                debugOn = false;
            }
            //freezes.transform.gameObject.tag = "Untagged";
        }
    }
}
