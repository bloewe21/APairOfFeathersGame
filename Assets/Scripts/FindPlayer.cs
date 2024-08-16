using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FindPlayer : MonoBehaviour
{
    [SerializeField] private bool isPlayerOne;
    [SerializeField] UnityEvent EndFindPlayerEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOne) {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                //EndFindPlayerEvent.Invoke();
                StartCoroutine(pauseRoutine());
            }
        }

        else if (!isPlayerOne) {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                //EndFindPlayerEvent.Invoke();
                StartCoroutine(pauseRoutine());
            }
        }
    }

    private IEnumerator pauseRoutine()
    {
        yield return new WaitForSeconds(.1f);
        EndFindPlayerEvent.Invoke();
    }
}
