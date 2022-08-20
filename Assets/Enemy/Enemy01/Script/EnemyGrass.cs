using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGrass : MonoBehaviour
{
    enum EnemyState
    {
        idle, hound, attack // idle:待機, hound:追逐, attack:攻擊
    }
    EnemyState enemyState;
    NavMeshAgent agent;
    Animator animator;
    public GameObject player; // 玩家
    public float damege;
    float attackTimer;
    float attackCD = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;

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
            
            if(Vector3.Distance(playerPos,myPos) >= 1.5f)
            {
                enemyState = EnemyState.hound;
            }
        }

        if(attackTimer <= attackCD)
        {
            attackTimer += Time.deltaTime;
        }
    }

    void StateIdle()
    {
        agent.speed = 0f;
        animator.SetBool("Hount", false);
    }

    void StateHound()
    {
        agent.speed = 1f;
        agent.SetDestination(player.transform.position);
        animator.SetBool("Hount", true);
        Face(player);
    }

    void StateAttack()
    {
        agent.speed = 0f;
        animator.SetBool("Hount", false);
        if(attackTimer >= attackCD)
        {
            animator.SetTrigger("Attack");
            attackTimer = 0;
        }
        Face(player);
    }

    void StateHit()
    {
        animator.SetTrigger("Hit");
    }

    public void OnHit()
    {

    }
    void StateDie()
    {

    }

    public void Face(GameObject player) // 面對玩家
    {
        float faceAngle = Mathf.Atan2(player.transform.position.x - transform.position.x , player.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
    }
}
