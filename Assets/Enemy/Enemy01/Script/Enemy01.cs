using System;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01 : EnemyStatusInfo
{
    public GameObject player;//玩家
    public GameObject attackCollision;//攻擊判定框
    public float hitTime = 0;//受擊經過時間
    public float attackCD = 1;//攻擊冷卻
    public float attackTime = 0;//攻擊後的時間
    NavMeshAgent agent;
    Animator animator;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
        mapController = GameObject.Find("Maps").GetComponent<MapController>();
        Hp = 100;
        damege = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;
        AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        
        if(state == 0)
        {
            //追逐
            Chasing();
            attackTime += Time.deltaTime;
            if(Vector3.Distance(playerPos,myPos) < 1.5f)//與玩家距離
            {
                state = 1;
            }
        }if(state == 1)
        {
            //攻擊
            Attack();
            if(Vector3.Distance(playerPos,myPos) >= 1.5f)//與玩家距離
            {
                state = 0;
            }
        }if(state == 2)
        {
            //被攻擊
            Hit();
            if(stateInfo.normalizedTime >= 0.9)
            {
                state = 0;
            }
        }if(state == 3)
        {
            //死亡
            Die();
        }
        if(state != 3)
        {
            //面對玩家
            Face(player);
        }

        AttackController();//攻擊時的移動、碰撞
    }

    void Chasing()//追逐玩家
    {
        agent.speed = 1f;
        agent.SetDestination(player.transform.position);

        animator.SetBool("attack",false);
        animator.SetBool("hit",false);
    }

    void Attack()//攻擊玩家
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

    void Hit()//被攻擊
    {
        agent.speed = 0f;
        animator.SetBool("hit",true);
        controller.Move(-transform.forward * 2 * Time.deltaTime);
    }

    void AttackController()
    {
        //攻擊時的移動、碰撞
        AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("attack") && stateInfo.normalizedTime >= 0.2 && stateInfo.normalizedTime <= 0.37)
        {
            controller.Move(transform.forward * Time.deltaTime);
            attackCollision.SetActive(true);
        }else if(stateInfo.IsName("attack") && stateInfo.normalizedTime >= 0.45 && stateInfo.normalizedTime <= 0.7)
        {
            controller.Move(transform.forward * Time.deltaTime);
            attackCollision.SetActive(true);
        }else 
        {
            attackCollision.SetActive(false);
        }

    }

    void Die()
    {
        dieTime += Time.deltaTime;
        agent.speed = 0f;
        animator.SetBool("die",true);
        controller.Move(-transform.forward * Time.deltaTime);
        if(dieTime >= 0.7f)
        {
            Instantiate (heal, this.transform.position, Quaternion.identity);
            mapController.EnemyCount();
            Destroy(gameObject);
        }
    }

}
