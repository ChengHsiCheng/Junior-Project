using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rabbit : EnemyStatusInfo
{
    public GameObject player;
    public GameObject rabbit;
    NavMeshAgent agent;
    Animator animator;
    CharacterController controller;
    public float attackTime = 0;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = rabbit.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        player = GameObject.Find("Player");
        Hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;
        AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);

        if(state == 0)
        {
            Chasing();
            attackTime += Time.deltaTime;
            if(Vector3.Distance(playerPos,myPos) < 1f)
            {
                state = 1;
            }
        }if(state == 1)
        {
            Attack();
            if(Vector3.Distance(playerPos,myPos) >= 1f)
            {
                state = 0;
            }
        }if(state == 2)
        {
            Hit();
            if(stateInfo.normalizedTime >= 0.9)
            {
                state = 0;
            }
        }
        Face(player);
    }

    void Chasing()//追逐玩家
    {
        agent.speed = 1f;
        agent.SetDestination(player.transform.position);
        animator.SetBool("attack",false);
        animator.SetBool("hit",false);
    }

    void Attack()//攻擊
    {
        agent.speed = 0f;
        if(attackTime >= 1)
        {
            animator.SetBool("attack",true);
            attackTime = 0;
        }else if(attackTime < 1)
        {
            animator.SetBool("attack",false);
            attackTime += Time.deltaTime;
        }
        animator.SetBool("hit",false);
    }

    void Hit()
    {
        agent.speed = 0f;
        animator.SetBool("hit",true);
        controller.Move(-transform.forward * 2 * Time.deltaTime);
    }
}
