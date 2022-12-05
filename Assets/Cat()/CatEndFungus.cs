using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEndFungus : MonoBehaviour
{
    public AudioClip roar01;
    public AudioClip roar02;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        int i = Random.Range(0, 2);
        if (i == 0)
            audioSource.PlayOneShot(roar01);
        else if (i == 1)
            audioSource.PlayOneShot(roar02);
    }
}
