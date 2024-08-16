using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform player;
    public float playerMaxHeight;

    // Start is called before the first frame update
    void Start()
    {
        playerMaxHeight = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > playerMaxHeight)
        {
            playerMaxHeight = player.transform.position.y;
        }
        transform.position = player.transform.position + new Vector3(0, 1, -5) - new Vector3((player.transform.position.x), 0, 0);
        //+1 so bird isn't directly in center
        transform.position = new Vector3(transform.position.x, playerMaxHeight + 1, transform.position.z);


        //IF WANT SIMPLE FOLLOW BIRD:
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }
}
