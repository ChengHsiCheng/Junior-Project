using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRabbit : EnemyStatusInfo
{
    EnemyState enemyState;
    NavMeshAgent agent;
    Animator animator;
    BoxCollider boxCollider;
    public GameObject player; // 玩家 
    public GameObject enemy;
    float attackTimer; // 攻擊計時器
    float attackCD = 2f; // 攻擊間隔
    float attackMoveSpeed = 5; // 攻擊移動速度
    float beAttackMoveSpeed = 2; // 攻擊移動速度
    bool attackMove; // 是否需要移動(攻擊)
    bool beAttackMove; // 是否需要移動(攻擊)
    bool isFace; // 是否要面對玩家

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        animator = enemy.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        attackTimer = attackCD;

        hp = 100;
        damege = 10;
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

        if(isFace)
        {
            Face(player);
        }

        if(attackTimer <= attackCD)
        {
            attackTimer += Time.deltaTime;
        }

        // 攻擊時移動
        if(attackMove)
        {
            transform.position += transform.forward * attackMoveSpeed * Time.deltaTime;
        }

        // 被攻擊時移動
        if(beAttackMove)
        {
            transform.position += -transform.forward * beAttackMoveSpeed * Time.deltaTime;
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
        // 重製移動 && 停止追蹤
        agent.speed = 0f;
        AttackMoveOff();
        BeAttackMoveOff();
        boxCollider.enabled = false;
        isFace = false;
    }

    public void BeAttacked(float damege)
    {   
        // 取得動畫狀態
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

        // 扣除血量
        hp -= damege;

        animator.SetTrigger("Hit");

        if(hp <= 0)
        {
            enemyState = EnemyState.Died;
            animator.SetTrigger("Die");
        }
    }
    
    public void AttackMoveOn()
    {
        attackMove = true;
        beAttackMove = false;
    }

    public void AttackMoveOff()
    {
        attackMove = false;
    }

    public void BeAttackMoveOn()
    {
        beAttackMove = true;
        attackMove = false;
    }

    public void BeAttackMoveOff()
    {
        beAttackMove = false;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Face(GameObject player) 
    {
        float faceAngle = Mathf.Atan2(player.transform.position.x - transform.position.x , player.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
    }
}
