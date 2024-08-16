using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStart : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject menuManager;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        logo.SetActive(false);
        canvasMenu.SetActive(false);
        menuManager.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnAnimEnd()
    // {
    //     StartCoroutine(MenuRoutine());
    // }

    private IEnumerator MenuRoutine()
    {
        logo.SetActive(true);
        yield return new WaitForSeconds(1f);
        canvasMenu.SetActive(true);
        menuManager.SetActive(true);
    }
}
