using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 宣告
        #region Dash
        float dashDuration = 0.2f;//衝刺時間
        float dashTime = 0.2f;//衝刺的時間
        float dashSpeed = 100f;//衝刺速度
        float dashCD = 0.5f;//衝刺的冷卻時間
        float dashElapsedTime = 0;//衝刺後經過的時間
        bool isDash = false;//是否在衝刺
        #endregion
        #region Attack
        public int comboStep = 0;//攻擊段數
        float interval = 0.5f;//攻擊間隔
        float attackTimer = 0;//攻擊間隔計算
        bool isAtteck = false;//是否在攻擊
        public ParticleSystem attackEffects01;
        public ParticleSystem attackEffects02;
        public ParticleSystem attackEffects03_1;
        public ParticleSystem attackEffects03_2;
        public AudioClip attackAudio01;
        public AudioClip attackAudio02;
        public AudioClip attackAudio03;
        #endregion
        #region 材質
        bool isChangePlayerMaterials = false; // 是否切換材質
        float changeColorTimer = 0; //切換材質時間計時
        float changeColorMaxTime = 0.1f; // 切換材質時間
        #endregion
        #region 實例
        Material[] materials;
        Animator animator;
        CharacterController controller;
        AudioSource audioSource;
        #endregion
    public float playerHp = 100;
    bool isPressLeftMouse = false;//是否按下滑鼠左鍵
    public float speed = 3;//移動速度
    #endregion
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        materials = GetComponent<MeshRenderer>().sharedMaterials;

        ResetPlayerMaterials();
    }
    void Update()
    {
        UpdateAttack();
        Move();
        Dash();

        if(Input.GetMouseButtonDown(0))
        {
            isPressLeftMouse = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isPressLeftMouse = false;
        }

        if(isChangePlayerMaterials)
        {
            ChangePlayerMaterials();
        }
    }
    void Move()//移動
    {
        if(isAtteck && comboStep == 3)
        {
            //攻擊時向前移動
            controller.Move(transform.forward * Time.deltaTime * 1);
        }if(!isAtteck && attackTimer == 0 && !isDash)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(h, 0, v);
            if(dir.magnitude > 0.1f)
            {
                //面向移動方向
                float faceAngle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
            }
            animator.SetFloat("dir",dir.magnitude);
            controller.Move( dir * speed * Time.deltaTime );
        }
    }
    void UpdateAttack()//攻擊輸入
    {
        if(Input.GetMouseButtonDown(0) && !isAtteck)
        {
            Attack();
        }

        if(attackTimer != 0)
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                //重製攻擊段數
                attackTimer = 0;
                comboStep = 0;
            }
        }

    }
    void Attack()//攻擊
    {
        isAtteck = true;
        comboStep++;
        if(comboStep > 3)
            comboStep = 1;
        attackTimer = interval;
        animator.SetTrigger("Attack");
        animator.SetInteger("ComboStep",comboStep);

        animator.SetFloat("dir",0);

        //面向鼠標
        var dir_r = Input.mousePosition-Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir_r.x,dir_r.y)*Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1f);
    }
    void AttaclOver()//結束攻擊狀態
    {
        if(!isPressLeftMouse)
            isAtteck = false;
        if(isPressLeftMouse)
        {
            Attack();

        }
    }
    void Dash()//衝刺
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        Vector3 dashDir = new Vector3(Mathf.Round(dir.x),Mathf.Round(dir.y),Mathf.Round(dir.z));
        
        if(!isAtteck)
        {
            if(!isDash)
            {
                if(dashElapsedTime >= dashCD)
                {
                    //CD結束
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        isDash = true;
                        dashElapsedTime = 0;
                        animator.SetTrigger("Dash");
                    }
                }else
                {
                    //計算CD
                    dashElapsedTime += Time.deltaTime;
                }
            }
            else
            {
                if(dashTime <= 0)
                {
                    isDash = false;
                    dashTime = dashDuration;
                }
                else
                {
                    dashTime -= Time.deltaTime;

                    controller.Move(this.transform.forward * dashTime * dashSpeed * Time.deltaTime);
                    // if(dir != Vector3.zero)//是否有方向輸入
                    // {
                    //     //朝輸入方向衝刺
                    //     controller.Move(dashDir * dashTime * dashSpeed * Time.deltaTime);
                    // }else
                    // {
                    //     //朝面相方向衝刺
                    //     controller.Move(this.transform.forward * dashTime * dashSpeed * Time.deltaTime);
                    // }

                }
            }
        }
    }
    void AttackEffectsPlay(string count)//攻擊特效
    {
        if(count == "1")
            attackEffects01.Play();
        if(count == "2")
            attackEffects02.Play();
        if(count == "3-1")
            attackEffects03_1.Play();
        if(count == "3-2")
            attackEffects03_2.Play();
    }
    void AttackAudioPlay(string count)//攻擊音效
    {
        if(count == "1")
        {
            audioSource.PlayOneShot(attackAudio01);
        }
        if(count == "2")
        {
            audioSource.PlayOneShot(attackAudio02);
        }
        if(count == "3")
        {
            audioSource.PlayOneShot(attackAudio03);
        }
    }
    public void PlayerHit(float damege)//被攻擊時計算傷害
    {
        playerHp -= damege;
        isChangePlayerMaterials = true;
        // animator.SetTrigger("Hit");

        for(int i = 0; i < materials.Length; i++) // 重置材質
        {
             materials[i].SetColor("_Color",Color.red);
        }

        if(playerHp <= 0)
        {
            PlayerDie();
        }
    }
    void ChangePlayerMaterials()
    {
        changeColorTimer += Time.deltaTime;
        if(changeColorTimer >= changeColorMaxTime)
        {
            ResetPlayerMaterials();
            isChangePlayerMaterials = false;
            changeColorTimer = 0;
        }
    }
    void ResetPlayerMaterials() // 重製材質
    {
        for(int i = 0; i < materials.Length; i++) // 重置材質
        {
             materials[i].SetColor("_Color",Color.white);
        }
    }
    void PlayerDie()//角色死亡
    {
        playerHp = 100;
        this.transform.position = new Vector3 (0,transform.position.y,-2f);

        //刪除所有敵人
        GameObject[] enemy;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemy.Length ; i++)
        {
            enemy[i].GetComponent<EnemyStatusInfo>().Damege(100,false);
        }
    }
    public void PlayerHeal(float heal)//角色回血
    { 
        if(playerHp < 100)  
        {
            playerHp += heal;
        }
    }
}
