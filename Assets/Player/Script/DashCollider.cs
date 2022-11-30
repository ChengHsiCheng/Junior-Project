using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCollider : MonoBehaviour
{
    public int isCollision;
    public string _other;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "EnemyAttackTrigger" && other.gameObject.tag != "Trigger" && other.gameObject.tag != "Boss" && other.gameObject.tag != "Player")
        {
            isCollision += 1;
            _other = other.name;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "EnemyAttackTrigger" && other.gameObject.tag != "Trigger" && other.gameObject.tag != "Boss" && other.gameObject.tag != "Player")
        {
            isCollision -= 1;
            _other = other.name;
        }
    }
}
