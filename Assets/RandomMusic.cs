using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> hudba = new List<AudioClip>();
    [SerializeField]
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if(!source.isPlaying)
        {
            PlayMusic();
        }
    }

    void PlayMusic()
    {
        source.clip = hudba[Random.Range(0,hudba.Count)];
        source.Play();
    }
}
