using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    hound, attack, skill, die
}

public class Boss : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;

    public State state;
    GameObject player; // 玩家 
    public GameObject attackVFX;
    public GameObject skillVFX;
    public GameObject walkVFX;
    public Transform attackVFXPos;
    public Transform skillVFXPos;
    public float speed;
    public float maxHp;
    public float hp;
    public float damege;
    public float houndToAttackDis;
    float walkVFXTimer;
    int walkVFXDir;
    float atttackTimer;
    float skillTimer;
    public float attackCD;
    public float attackMoveSpeed;
    public float skillCD;
    bool isAttackMoving;
    bool isDash;
    public float dashCD;
    float dashTimer;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        hp = maxHp;

        atttackTimer = attackCD;
        skillTimer = skillCD;
        dashTimer = dashCD;
    }

    // Update is called once per frame
    void Update()
    {
        // 玩家位置
        Vector3 playerPos = player.transform.position;
        // 自身位置
        Vector3 myPos = transform.position;
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

        if (state == State.hound)
        {
            StateHound();

            if (Vector3.Distance(playerPos, myPos) < houndToAttackDis)
            {
                state = State.attack;
                isDash = false;
                dashTimer = 0;
            }
        }
        if (state == State.attack)
        {
            StateAttack(Vector3.Distance(playerPos, myPos));

            if (Vector3.Distance(playerPos, myPos) >= houndToAttackDis && !stateinfo.IsName("Attack"))
            {
                state = State.hound;
            }
        }
        if (state == State.skill)
        {
            StateSkill();
        }


        if (atttackTimer < attackCD)
        {
            atttackTimer += Time.deltaTime;
        }

        if (skillTimer < skillCD)
        {
            skillTimer += Time.deltaTime;
        }

        if (dashTimer < dashCD)
        {
            dashTimer += Time.deltaTime;
        }

        if (walkVFXTimer < 0.5f)
        {
            walkVFXTimer += Time.deltaTime;
        }

        if (isAttackMoving)
        {
            transform.position += transform.forward * attackMoveSpeed * Time.deltaTime;
        }
    }

    void StateHound()
    {
        if (isDash)
        {
            speed = 50;
            dashTimer -= Time.deltaTime;
            if (dashTimer <= dashCD - 0.2f)
            {
                isDash = false;
                dashTimer = 0;
            }
        }
        else
        {
            speed = 2;
        }

        if (dashTimer >= dashCD)
        {
            isDash = true;
        }

        agent.speed = speed;
        agent.SetDestination(player.transform.position);
        animator.SetBool("Hount", true);

        if (walkVFXTimer >= 0.3f)
        {
            if (walkVFXDir == 0)
            {
                Instantiate(walkVFX, transform.position + transform.up * 0.05f + transform.right * 0.1f, transform.rotation * Quaternion.Euler(90, 0, 0));
                walkVFXDir = 1;
                walkVFXTimer = 0;
            }
            else if (walkVFXDir == 1)
            {
                Instantiate(walkVFX, transform.position + transform.up * 0.05f + -transform.right * 0.1f, transform.rotation * Quaternion.Euler(90, 0, 0));
                walkVFXDir = 0;
                walkVFXTimer = 0;
            }

        }
        Face(player);
    }

    void StateAttack(float dis)
    {
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

        agent.speed = 0;
        animator.SetBool("Hount", false);

        if (atttackTimer >= attackCD)
        {
            animator.SetTrigger("Attack");
            atttackTimer = 0;


        }

        if (!stateinfo.IsName("Attack"))
        {
            Face(player);
            if (dis > 2)
            {
                StateHound();
            }
        }
    }

    void StateSkill()
    {
        Face(player);
        animator.SetBool("Hount", false);
        agent.speed = 0;

        if (skillTimer >= skillCD)
        {
            animator.SetTrigger("Skill");
            skillTimer = 0;
        }

    }

    void StateDie()
    {
        agent.speed = 0f;

    }

    public void Face(GameObject player)
    {
        float faceAngle = Mathf.Atan2(player.transform.position.x - transform.position.x, player.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
    }

    public void BeAttack(float _damege)
    {
        hp -= _damege;

        if (hp <= 0)
        {
            state = State.die;
            animator.SetTrigger("Die");
        }
    }

    void AttactVFX()
    {
        Instantiate(attackVFX, attackVFXPos);
    }

    void SkillJudgment()
    {
        int ranval = Random.Range(0, 200);
        if (ranval < 50 && skillTimer >= skillCD)
        {
            state = State.skill;
        }
    }

    public void AttackMoveOn()
    {
        isAttackMoving = true;
    }

    public void AttackMoveOff()
    {
        isAttackMoving = false;
    }

    public void OnPlaySkill01()
    {
        Instantiate(skillVFX, skillVFXPos.position, Quaternion.identity);
    }

    public void SkillEnd()
    {
        state = State.hound;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
