using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    [SerializeField] private GameObject collectibleManager;
    [SerializeField] private AudioSource itemSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collectibleManager.GetComponent<CollectibleManager>().PickupFunction(gameObject.name);
        itemSound.Play();
        Destroy(gameObject);
    }
}
