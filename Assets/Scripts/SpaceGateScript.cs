using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGateScript : MonoBehaviour
{
    private GameObject player;
    private BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovement2>().isFrozen)
        {
            coll.enabled = false;
        }
        else
        {
            coll.enabled = true;
        }
    }
}
