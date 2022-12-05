using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevelUp : MonoBehaviour
{
    public GameObject UI;

    AudioSource source;
    public AudioClip audioClip;

    public Player player;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UI.SetActive(true);
            player.StopAudio();
            Time.timeScale = 0;
            Destroy(gameObject, 0.1f);

            source.PlayOneShot(audioClip);
        }
    }

}
