using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatusInfo : MonoBehaviour
{
    // idle:待機, hound:追逐, attack:攻擊, Died:死亡
    protected enum EnemyState
    {
        idle, hound, attack, Died 
    }


    protected float _hp;
    public float hp
    {
        get => _hp;
    }
    public float maxHp;
    protected float _damege;
    public float damege
    {
        get => _damege;
    }

    protected bool isFace; // 是否要面對玩家

    void Start()
    {
        
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
