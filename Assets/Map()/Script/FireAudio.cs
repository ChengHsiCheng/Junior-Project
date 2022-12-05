using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAudio : MonoBehaviour
{
    AudioSource source;
    public AudioClip fire;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = fire;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
