using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRabbit : EnemyStatusInfo
{
    // idle:待機, hound:追逐, attack:攻擊, Died:死亡
    enum EnemyState
    {
        idle, hound, attack, Died 
    }

    void Start()
    {
        _hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
