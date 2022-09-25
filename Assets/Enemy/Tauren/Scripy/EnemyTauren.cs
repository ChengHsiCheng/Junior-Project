using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTauren : EnemyStatusInfo
{
    EnemyState enemyState;
    NavMeshAgent agent;
    Animator animator;
    public GameObject player; // 玩家 
    float attackTimer; // 攻擊計時器
    float attackCD = 2f; // 攻擊間隔
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        // 重製材質
        // ResetMaterials(); 
        // attackTimer = attackCD;

        //設定參數
        _damege = 10;
        _hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // 玩家位置
        Vector3 playerPos = player.transform.position; 
        // 自身位置
        Vector3 myPos = transform.position; 
        // 取得動畫狀態
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

        if(enemyState == EnemyState.idle)
        {
            StateIdle();

            if(Vector3.Distance(playerPos,myPos) < 4f)
            {
                enemyState = EnemyState.hound;
            }
        }
        if(enemyState == EnemyState.hound)
        {
            StateHound();

            if(Vector3.Distance(playerPos,myPos) >= 4f)
            {
                enemyState = EnemyState.idle;
            }
            if(Vector3.Distance(playerPos,myPos) < 1.5f)
            {
                enemyState = EnemyState.attack;
            }
        }
        if(enemyState == EnemyState.attack)
        {
            StateAttack();
            
            if(Vector3.Distance(playerPos,myPos) >= 1.5f && !stateinfo.IsName("Attack"))
            {
                enemyState = EnemyState.hound;
            }
        }
        if(enemyState == EnemyState.Died)
        {
            StateDied();
        }

        // 攻擊間隔計時
        if(attackTimer <= attackCD)
        {
            attackTimer += Time.deltaTime;
        }
    }

    void StateIdle()
    {
        // 停止追蹤
        agent.speed = 0f;
        animator.SetBool("Hount", false);
        isFace = false;
    }

    void StateHound()
    {
        // 開始追蹤
        agent.speed = 1f;
        agent.SetDestination(player.transform.position);
        animator.SetBool("Hount", true);
        isFace = true;
    }

    void StateAttack()
    {
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

        // 準備進行攻擊
        agent.speed = 0f;
        animator.SetBool("Hount", false);
        if(attackTimer >= attackCD && !stateinfo.IsName("Hit"))
        {
            animator.SetTrigger("Attack");
            attackTimer = 0;
        }
        
        if(stateinfo.IsName("Attack"))
        {
            isFace = false;
        }else
        {
            isFace = true;
        }
    }

    void StateDied()
    {
    }
}
