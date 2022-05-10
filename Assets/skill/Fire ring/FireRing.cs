using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRing : MonoBehaviour
{
    float damege = 1;
    Enemy01 TargetEnemy;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        transform.position = player.transform.position;
    }
    private void OnTriggerStay(Collider other) {
        Debug.Log("A");
        if(other.tag == "Enemy")
        {//計算傷害
            TargetEnemy = other.GetComponent<Enemy01>();
            TargetEnemy.Damege(damege);
        }
    }
}
