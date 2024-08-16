using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivate : MonoBehaviour
{
    [SerializeField] private float timeUntilActivate = 1f;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(activateTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator activateTimer()
    {
        yield return new WaitForSeconds(timeUntilActivate);

        player.SetActive(true);
    }
}
