using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource my_sound;

    [SerializeField] UnityEvent EndSoundEvent;

    // Start is called before the first frame update
    void Awake()
    {
        my_sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!my_sound.isPlaying)
        {
            EndSoundEvent.Invoke();
        }
    }
}
