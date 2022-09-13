using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffecis : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] hitAudio;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Destroy(gameObject, 1f);

        int r = Random.Range(0, hitAudio.Length);

        audioSource.PlayOneShot(hitAudio[r]);
    }

}
