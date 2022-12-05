using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
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
            other.gameObject.GetComponent<Player>().goldCount += Random.Range(5, 11);

            source.PlayOneShot(audioClip);

            transform.position += new Vector3(0, 10, 0);

            Destroy(gameObject, 2f);
        }
    }
}
