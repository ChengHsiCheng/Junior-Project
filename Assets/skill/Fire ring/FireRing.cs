using System.IO.Pipes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRing : MonoBehaviour
{
    float damege = 0.2f;
    EnemyStatusInfo TargetEnemy;
    GameObject player;
    public FireRingSkill fireRingSkill;

    public delegate void FireRingUsingEventArgs();
    public FireRingUsingEventArgs OnFireRingUsing;

    public float clock = 0;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        transform.position = player.transform.position;

        clock += Time.deltaTime;

        if(clock >= 6)
        {
            OnFireRingUsing();//委派事件:效果結束
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.tag == "Enemy")
        {//計算傷害
            TargetEnemy = other.GetComponent<EnemyStatusInfo>();
            TargetEnemy.Damege(damege, false);
        }
    }
}
