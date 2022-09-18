using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGrass : EnemyStatusInfo
{
    // idle:待機, hound:追逐, attack:攻擊, Died:死亡
    enum EnemyState
    {
        idle, hound, attack, Died 
    }
    EnemyState enemyState;
    NavMeshAgent agent;
    Animator animator;
    AudioSource audioSource;
    public Material[] materials; // 儲存材質
    public GameObject player; // 玩家 
    float attackTimer; // 攻擊計時器
    float attackCD = 2f; // 攻擊間隔
    float attackMoveSpeed = 2; // 攻擊移動速度
    bool attackMove; // 是否需要移動(攻擊)
    bool beAttackMove; // 是否需要移動(被攻擊)
    bool isChangeColor; // 是否需要切換材質顏色
    bool isFace; // 是否要面對玩家
    float changeColorMaxTime = 0.1f; // 切換材質顏色時間
    float changeColorTimer = 0; // 切換材質顏色計時器
    public AudioClip attackAudio_01;
    public AudioClip attackAudio_02;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");

        // 重製材質
        ResetMaterials(); 
        attackTimer = attackCD;

        //設定參數
        _damege = 10;
        _hp = 100;
    }

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

        if(isChangeColor)
        {
            BeAttackChangeColor();
        }

        if(isFace)
        {
            Face(player);
        }

        // 攻擊時移動
        if(attackMove)
        {
            transform.position += transform.forward *attackMoveSpeed * Time.deltaTime;
        }

        // 被攻擊時移動
        if(beAttackMove)
        {
            transform.position += -transform.forward * attackMoveSpeed * Time.deltaTime;
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
        isFace = false;
    }

    public override void BeAttacked(float damege)
    {   
        // 取得動畫狀態
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

        // 扣除血量
        _hp -= damege;

        isChangeColor = true;

        // 若不在攻擊動畫就撥放被攻擊動畫
        if(!stateinfo.IsName("Attack"))
        {
            animator.SetTrigger("Hit");
        }

        if(_hp <= 0)
        {
            enemyState = EnemyState.Died;
            animator.SetTrigger("Die");
        }
    }

    // 重製材質
    void ResetMaterials() 
    {
        for(int i = 0; i < materials.Length; i++)
        {
            materials[i].color = Color.white;
        }
    }

    // 被攻擊時切換材質顏色
    public void BeAttackChangeColor()
    {
        // 切換材質
        if(changeColorTimer == 0)
        { 
            for(int i = 0; i < materials.Length; i++) 
            {
                materials[i].color = Color.red;
            }
        }
        
        changeColorTimer += Time.deltaTime;

        // 重製材質顏色
        if(changeColorTimer >= changeColorMaxTime)
        {
            ResetMaterials();
            isChangeColor = false;
            changeColorTimer = 0;
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

    public void PlayAttackAudio_01()
    {
        audioSource.PlayOneShot(attackAudio_01);
    }

    public void PlayAttackAudio_02()
    {
        audioSource.PlayOneShot(attackAudio_02);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

