using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCollider : MonoBehaviour
{
    public int isCollision;
    public string _other;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "EnemyAttackTrigger" && other.gameObject.tag != "Trigger" && other.gameObject.tag != "Player" && other.tag != "Enemy" && other.tag != "Boss")
        {
            isCollision += 1;
            _other = other.name;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "EnemyAttackTrigger" && other.gameObject.tag != "Trigger" && other.gameObject.tag != "Player" && other.tag != "Enemy" && other.tag != "Boss")
        {
            isCollision -= 1;
            _other = other.name;
        }
    }
}
