using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrass : MonoBehaviour
{
    enum EnemyState
    {
        idle , hound, attack, hit, die
    }
    EnemyState enemyState;
    public GameObject player;//玩家
    void Start()
    {
        
    }

    void Update()
    {
        if(enemyState == EnemyState.idle)
        {
            StateIdle();
        }
        if(enemyState == EnemyState.hound)
        {
            StateHound();
        }
        if(enemyState == EnemyState.attack)
        {
            StateAttack();
        }
        if(enemyState == EnemyState.hit)
        {
            StateHit();
        }
        if(enemyState == EnemyState.die)
        {
            StateDie();
        }
    }

    void StateIdle()
    {

    }

    void StateHound()
    {
        
    }

    void StateAttack()
    {

    }

    void StateHit()
    {

    }

    void StateDie()
    {

    }
}
