using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatusInfo : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float damege;

    void Start()
    {
        hp = maxHp;
    }
}
