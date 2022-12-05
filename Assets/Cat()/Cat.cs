using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    Animator animator;
    int ran;
    float timer;
    int ranAudio;

    public AudioClip[] audios;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio()
    {
        ranAudio = Random.Range(0, audios.Length);
        audioSource.PlayOneShot(audios[ranAudio]);
    }
}
