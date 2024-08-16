using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public float myDefaultAudio;
    public float myDefaultPitch;
    // Start is called before the first frame update
    void Start()
    {
        myDefaultAudio = GetComponent<AudioSource>().volume;
        myDefaultPitch = GetComponent<AudioSource>().pitch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
