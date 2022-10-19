    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatusInfo : MonoBehaviour
{
    public float baseHp;
    public float hp;
    public float maxHp;
    public float damege;

    public float hpAddition;
    public float damegeAddition;
    public float speedAddition;
    

    void Start()
    {
        hp = maxHp;
    }

    public void SetParm(float hp, float damege, float speed)
    {
            hpAddition += hp;
            maxHp = baseHp * hpAddition;
            hp = maxHp;

            damegeAddition += damege;
            speedAddition += speed;
        
    }

}
