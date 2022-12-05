using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public AudioClip[] audios;
    protected EnemyStatusInfo info;
    EnemyState enemyState;
    NavMeshAgent agent;
    Animator animator;
    protected AudioSource audioSource;
    Player player; // 玩家 
    float attackTimer; // 攻擊計時器
    public float speed;
    public float idleToHoundDis;
    public float houndToAttackDis;
    public float attackCD; // 攻擊間隔
    public float attackMoveSpeed; // 攻擊移動速度
    public float beAttackMoveSpeed;
    public float high;

    bool attackMove; // 是否需要移動(攻擊)
    bool beAttackMove; // 是否需要移動(被攻擊)
    bool isFace; // 是否要面對玩家
    bool isCollision;
    Hashtable debuffTable;

    public delegate void EnemuDieEventArgs();
    public EnemuDieEventArgs OnEnemyDie;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        debuffTable = new Hashtable();
        debuffTable.Add(EnemyDebuffType.Burning, null);
        debuffTable.Add(EnemyDebuffType.Frozen, null);


        //設定參數
        info = GetComponent<EnemyStatusInfo>();
    }

    void Update()
    {
        if (!player.isDying)
        {// 玩家位置
            Vector3 playerPos = player.transform.position;
            // 自身位置
            Vector3 myPos = transform.position;
            // 取得動畫狀態
            AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

            if (enemyState == EnemyState.idle)
            {
                StateIdle();

                if (Vector3.Distance(playerPos, myPos) < idleToHoundDis)
                {
                    enemyState = EnemyState.hound;
                }
            }
            if (enemyState == EnemyState.hound)
            {
                StateHound();

                if (Vector3.Distance(playerPos, myPos) >= idleToHoundDis)
                {
                    enemyState = EnemyState.idle;
                }
                if (Vector3.Distance(playerPos, myPos) < houndToAttackDis)
                {
                    enemyState = EnemyState.attack;
                }
            }
            if (enemyState == EnemyState.attack)
            {
                StateAttack();

                if (Vector3.Distance(playerPos, myPos) >= houndToAttackDis && !stateinfo.IsName("Attack"))
                {
                    enemyState = EnemyState.hound;
                }
            }
            if (enemyState == EnemyState.died)
            {
                StateDied();
            }

            if (isFace)
            {
                Face(player);
            }

            // 攻擊間隔計時
            if (attackTimer <= attackCD)
            {
                attackTimer += Time.deltaTime;
            }

            // 攻擊時移動
            if (attackMove && Vector3.Distance(playerPos, myPos) > 0.8f)
            {
                transform.position += transform.forward * attackMoveSpeed * speed * Time.deltaTime;
            }

            // 被攻擊時移動
            if (beAttackMove)
            {
                transform.position += -transform.forward * beAttackMoveSpeed * speed * Time.deltaTime;
            }

            if (debuffTable[EnemyDebuffType.Frozen] != null)
            {
                speed = 0.5f;
            }
            else
            {
                speed = 1;
            }

            if (debuffTable[EnemyDebuffType.Burning] != null)
            {
                if (Time.time - (float)debuffTable[EnemyDebuffType.Burning] > 5)
                {
                    RemoveDebuff(EnemyDebuffType.Burning);
                }
                else
                {
                    if (info.hp > 0)
                        BeAttacked(0.1f, false);
                }
            }

            animator.SetFloat("Speed", speed);
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
        agent.speed = speed * info.speedAddition;
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
        if (attackTimer >= attackCD && !stateinfo.IsName("Hit"))
        {
            animator.SetTrigger("Attack");
            attackTimer = 0;
        }

        if (stateinfo.IsName("Attack"))
        {
            isFace = false;
        }
        else
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

    // 面對玩家
    public void Face(Player player)
    {
        float faceAngle = Mathf.Atan2(player.transform.position.x - transform.position.x, player.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
    }

    public void BeAttacked(float damege, bool isDriveOff)
    {
        // 取得動畫狀態
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

        // 扣除血量
        info.hp -= damege;

        if (isDriveOff)
        {
            animator.SetTrigger("Hit");
        }

        if (info.hp <= 0)
        {
            enemyState = EnemyState.died;
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

        gameObject.SetActive(false);
        OnEnemyDie();
        Destroy(gameObject);

    }

    public void AddDebuff(EnemyDebuffType type)
    {
        if (debuffTable[type] == null)
        {
            debuffTable[type] = Time.time;
        }
    }

    public void RemoveDebuff(EnemyDebuffType type)
    {
        debuffTable[type] = null;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            isCollision = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            isCollision = false;
        }
    }

    public void PlayerAttackAudio(int i)
    {
        audioSource.PlayOneShot(audios[i]);
    }
}
