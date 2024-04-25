using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundGenerator : MonoBehaviour
{
    public AudioClip[] sounds;
    public float minDelay = 1f;
    public float maxDelay = 5f;
    
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("PlayRandomSoundWithDelay", Random.Range(minDelay, maxDelay));
    }

    void PlayRandomSoundWithDelay()
    {
        if (sounds.Length > 0)
        {
            AudioClip sound = sounds[Random.Range(0, sounds.Length)];
            audioSource.PlayOneShot(sound);
        }

        Invoke("PlayRandomSoundWithDelay", Random.Range(minDelay, maxDelay));
    }
}
