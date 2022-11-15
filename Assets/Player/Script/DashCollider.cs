using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCollider : MonoBehaviour
{
    public bool isCollision;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "EnemyAttackTrigger" && other.gameObject.tag != "Trigger" && other.gameObject.tag != "Boss")
        {
            isCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            isCollision = false;
        }
    }
}
