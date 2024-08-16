using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerLight;

    [SerializeField] private float oceanStartPos;
    [SerializeField] private float islandStartPos;
    [SerializeField] private float caveStartPos;
    [SerializeField] private float mountainStartPos;
    [SerializeField] private float volcanoStartPos;
    [SerializeField] private float nightStartPos;
    [SerializeField] private float spaceStartPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < islandStartPos)
        {
            //in ocean
            playerLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0.5f;
        }

        else if (player.transform.position.y > islandStartPos && player.transform.position.y < caveStartPos)
        {
            //in island
            playerLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0.1f;
        }

        else if (player.transform.position.y > caveStartPos && player.transform.position.y < mountainStartPos)
        {
            //in cave
            playerLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0.5f;
        }

        else if (player.transform.position.y > mountainStartPos && player.transform.position.y < volcanoStartPos)
        {
            //in mountain
            playerLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0.15f;
        }

        else if (player.transform.position.y > volcanoStartPos && player.transform.position.y < nightStartPos)
        {
            //in volcano
            playerLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0.3f;
        }

        else if (player.transform.position.y > nightStartPos && player.transform.position.y < spaceStartPos)
        {
            //in night
            playerLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0.3f;
        }

        else if (player.transform.position.y > spaceStartPos)
        {
            //in night
            playerLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0.25f;
        }
    }
}
