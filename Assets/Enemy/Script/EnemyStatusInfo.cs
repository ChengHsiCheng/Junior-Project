using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusInfo : MonoBehaviour
{
    protected float _hp;
    public float hp
    {
        get => _hp;
    }
    protected float _damege;
    public float damege
    {
        get => _damege;
    }

    // 被攻擊
    public virtual void BeAttacked(float damege)
    { 
    }

    // 面對玩家
    public void Face(GameObject player) 
    {
        float faceAngle = Mathf.Atan2(player.transform.position.x - transform.position.x , player.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
    }
}
