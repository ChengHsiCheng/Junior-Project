using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float Hp = 100;
    public GameObject player;
    public int state = 0;//0 = 追逐 , 1 = 攻擊 , 2 = 受擊
    public float hitTime = 0;//受擊經過時間
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
        face();
    }

    void face()//面相玩家
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
        animator.SetBool("attack",true);
        animator.SetBool("hit",false);
    }

    void Hit()//被攻擊
    {
        agent.speed = 0f;
        animator.SetBool("hit",true);
        controller.Move(-transform.forward * Time.deltaTime);
    }

}
