using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    private float startHeight;
    private float spawnHeight;
    private int randChoice;
    private GameObject objectToSpawn;
    public GameObject obstacle1;
    public GameObject obstacle2;
    // Start is called before the first frame update
    void Start()
    {
        //startHeight = transform.position.y;
        spawnHeight = 0f;
        randChoice = Random.Range(1, 3);
        NewHeight(randChoice);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= spawnHeight)
        {
            //randomize what to spawn
            randChoice = Random.Range(1, 10);
            if (randChoice <= 5)
            {
                objectToSpawn = obstacle1;
            }
            else
            {
                objectToSpawn = obstacle2;
            }

            //add extra height in Instantiate so obstacle spawns off screen
            Instantiate(objectToSpawn, new Vector3(0f, spawnHeight + 10f, 0f), Quaternion.identity);

            //randomize next height
            randChoice = Random.Range(1, 3);
            print(randChoice);
            NewHeight(randChoice);
        }
    }

    private void NewHeight(int randInt)
    {
        if (randInt == 1)
        {
            spawnHeight += 10;
        }
        else if (randInt == 2)
        {
            spawnHeight += 5;
        }
    }
}