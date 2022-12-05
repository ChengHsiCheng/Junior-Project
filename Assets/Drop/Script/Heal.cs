using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float value;

    AudioSource source;
    public AudioClip audioClip;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().PlayerHeal(value);
            source.PlayOneShot(audioClip);

            transform.position += new Vector3(0, 10, 0);

            Destroy(gameObject, 3f);
        }
    }
}
