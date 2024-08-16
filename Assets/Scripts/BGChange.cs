using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BGChange : MonoBehaviour
{
    //private GameObject player;
    private float player_y_position;
    //private bool steamConnected = false;

    // public GameObject background1;
    // public GameObject background2;
    // public GameObject room_background2;

    [SerializeField] private GameObject player;

    [SerializeField] private float cave_position = 110f;
    [SerializeField] private float mountain_position = 250f;
    [SerializeField] private float volcano_position = 400f;
    [SerializeField] private float night_position = 600f;
    [SerializeField] private float space_position = 800f;

    [SerializeField] UnityEvent ToIslandEvent;
    [SerializeField] UnityEvent ToCaveEvent;
    [SerializeField] UnityEvent ToMountainEvent;
    [SerializeField] UnityEvent ToVolcanoEvent;
    [SerializeField] UnityEvent ToNightEvent;
    [SerializeField] UnityEvent ToSpaceEvent;

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player_y_position = player.transform.position.y;

        if (player_y_position > cave_position  && player_y_position < mountain_position)
        {
            if (PlayerPrefs.GetInt("steamConnected") == 1)
            {
                var ach = new Steamworks.Data.Achievement("ACH_CAVE");
                if (!ach.State)
                {
                    ach.Trigger();
                }
            }
            
            ToCaveEvent.Invoke();
        }

        else if (player_y_position > mountain_position && player_y_position < volcano_position)
        {
            if (PlayerPrefs.GetInt("steamConnected") == 1)
            {
                var ach = new Steamworks.Data.Achievement("ACH_MOUNTAIN");
                if (!ach.State)
                {
                    ach.Trigger();
                }
            }

            ToMountainEvent.Invoke();
        }

        else if (player_y_position > volcano_position && player_y_position < night_position)
        {
            if (PlayerPrefs.GetInt("steamConnected") == 1)
            {
                var ach = new Steamworks.Data.Achievement("ACH_VOLCANO");
                if (!ach.State)
                {
                    ach.Trigger();
                }
            }

            ToVolcanoEvent.Invoke();
        }

        else if (player_y_position > night_position && player_y_position < space_position)
        {
            if (PlayerPrefs.GetInt("steamConnected") == 1)
            {
                var ach = new Steamworks.Data.Achievement("ACH_PEAK");
                if (!ach.State)
                {
                    ach.Trigger();
                }
            }

            ToNightEvent.Invoke();
        }

        else if (player_y_position > space_position)
        {
            if (PlayerPrefs.GetInt("steamConnected") == 1)
            {
                var ach = new Steamworks.Data.Achievement("ACH_SPACE");
                if (!ach.State)
                {
                    ach.Trigger();
                }
            }

            ToSpaceEvent.Invoke();
        }

        //many ifs

        else
        {
            ToIslandEvent.Invoke();
        }
    }
}
