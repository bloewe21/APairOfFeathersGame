using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerTest : MonoBehaviour
{
    private string myName;
    private string myHostName;
    private bool isHost = false;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(3075170);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

        myName = Steamworks.SteamClient.Name;
        myHostName = PlayerPrefs.GetString("savedHostName");

        if (myName == myHostName)
        {
            isHost = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && isHost)
        {
            print("left");
            transform.Translate(Vector2.left * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) && !isHost)
        {
            print("right");
            transform.Translate(Vector2.right * Time.deltaTime);
        }
    }
}
