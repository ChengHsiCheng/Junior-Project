using System;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01 : EnemyStatusInfo
{
    public GameObject player;
    public GameObject attackCollision;//攻擊判定框
    public int state = 0;//0 = 追逐 , 1 = 攻擊 , 2 = 受擊
    public float hitTime = 0;//受擊經過時間
    public float attackCD = 1;
    public float attackTime = 0;
    NavMeshAgent agent;
    Animator animator;
    CharacterController controller;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
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
            if(Vector3.Distance(playerPos,myPos) < 0.7f)
            {
                state = 1;
            }
        }
        if(state == 1)
        {
            Attack();
            if(Vector3.Distance(playerPos,myPos) >= 0.7f)
            {
                state = 0;
            }
        }
        if(state == 2)
        {
            Hit();
            if(stateInfo.normalizedTime >= 0.9)
            {
                state = 0;
            }
        }
        AttackTrigger();
        Face();
    }

    void Face()//面相玩家
    {
        float faceAngle = Mathf.Atan2(player.transform.position.x - transform.position.x , player.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
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

    void AttackTrigger()
    {
        AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("attack") && stateInfo.normalizedTime >= 0.2 && stateInfo.normalizedTime <= 0.37)
        {
            attackCollision.SetActive(true);
        }else if(stateInfo.IsName("attack") && stateInfo.normalizedTime >= 0.45 && stateInfo.normalizedTime <= 0.7)
        {
            attackCollision.SetActive(true);
        }else 
        {
            attackCollision.SetActive(false);
        }

    }

    public void Damege(float damege)//計算傷害
    {
        Hp -= damege;
        if(Hp <= 0)
        {
            Destroy(gameObject,0.1f);
        }
    }
}
