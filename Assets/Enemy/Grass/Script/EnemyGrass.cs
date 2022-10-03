using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrass : Enemy
{
    public AudioClip[] audios;


    public void PlayerAttackAudio(int i)
    {
        audioSource.PlayOneShot(audios[i]);
    }
}
