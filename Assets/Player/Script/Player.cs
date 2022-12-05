using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Fungus;
using UnityEngine.Assertions;



public class Player : MonoBehaviour
{
    #region 宣告

    Save save;

    public FungusControl fungusControl;

    public Flowchart startFlowchart;
    string fungusBoolName = "isStart";
    public bool fungusBool
    {
        get
        {
            return startFlowchart.GetBooleanVariable(fungusBoolName);
        }
        set
        {
            startFlowchart.SetBooleanVariable(fungusBoolName, value);
        }
    }



    public bool isDied;
    public bool isDying;
    public PlayerDieUI diedUI;

    #region Dash
    float dashDuration = 0.15f;//衝刺時間
    float dashTime = 0.15f;//衝刺的時間
    float dashSpeed = 200f;//衝刺速度
    float dashCD = 0.2f;//衝刺的冷卻時間
    float dashElapsedTime = 0;//衝刺後經過的時間
    bool isDash = false;//是否在衝刺
    public AudioClip dashAudio;
    public GameObject dashVFX;
    #endregion
    #region Attack
    public float baseDamege;
    public float damege;
    public float damegeAdd = 1;
    public float critAdd = 2;
    public int comboStep = 0;//攻擊段數
    float interval = 0.5f;//攻擊間隔
    float attackTimer = 0;//攻擊間隔計算
    bool isAtteck = false;//是否在攻擊
    bool attackMove;
    public GameObject attackEffects01;
    public GameObject attackEffects02;
    public ParticleSystem attackEffects03_1;
    public GameObject attackEffects03_2;
    public AudioClip attackAudio01;
    public AudioClip attackAudio02;
    public AudioClip attackAudio03;
    public AudioClip beAttackAudio;
    public AudioClip walkAudio;
    #endregion
    #region 材質
    bool isChangePlayerMaterials = false; // 是否切換材質
    float changeColorTimer = 0; //切換材質時間計時
    float changeColorMaxTime = 0.1f; // 切換材質時間
    #endregion
    #region 實例
    Material[] materials;
    Animator animator;
    Rigidbody rb;
    CharacterController character;
    AudioSource audioSource;
    public GameObject hitEffecis;
    public DashCollider dashCollider;
    #endregion
    public float baseHp;
    public float playerHp;
    public float playetMaxHp;
    bool isPressLeftMouse = false;//是否按下滑鼠左鍵
    public float baseSpeed = 3;
    public float speed;//移動速度

    public float crystalCount = 0;
    public float goldCount = 0;
    public LevelRewardType levelRewardType;
    public bool isTheEnd;

    SkillManager skillManager;

    public List<PassiveSkill> passiveSkills = new List<PassiveSkill>();
    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        materials = GetComponent<MeshRenderer>().sharedMaterials;
        rb = GetComponent<Rigidbody>();
        character = GetComponent<CharacterController>();
        skillManager = GetComponent<SkillManager>();
        save = GetComponent<Save>();

        damege = baseDamege;
        playetMaxHp = baseHp;
        playerHp = playetMaxHp;
        speed = baseSpeed;
        ResetPlayerMaterials();

        audioSource.Play();

        LoadGame();
    }

    void Update()
    {
        if (fungusBool && !isDying)
        {
            UpdateAttack();
            Dash();

            if (Input.GetMouseButtonDown(0))
            {
                isPressLeftMouse = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isPressLeftMouse = false;
            }



            if (transform.position.y > 0)
            {
                character.Move(new Vector3(0, -1f, 0) * Time.deltaTime);
            }
            if (transform.position.y < 0)
            {
                character.Move(new Vector3(0, 1f, 0) * Time.deltaTime);
            }
        }
        else
        {
            animator.SetFloat("dir", 0);
            StopAudio();
            isPressLeftMouse = false;
        }

        if (isChangePlayerMaterials)
        {
            ChangePlayerMaterials();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            crystalCount += 100;
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            damege = 10000;
            playerHp = 10000;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            Debug.Log(passiveSkills[0]);
        }
    }

    private void FixedUpdate()
    {
        if (fungusBool && !isDying)
        {
            Move();

            if (isDash)
            {
                if (dashTime <= 0)
                {
                    isDash = false;
                    rb.velocity = Vector3.zero;
                    dashTime = dashDuration;
                }
                else
                {
                    dashTime -= Time.deltaTime;

                    if (dashCollider.isCollision == 0)
                    {
                        character.Move(transform.forward * dashTime * dashSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }

    // 玩家移動
    void Move()
    {
        if (isAtteck && attackMove && dashCollider.isCollision == 0)
        {
            //攻擊時向前移動
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (!isAtteck && attackTimer == 0 && !isDash)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(h, 0, v);
            dir = Quaternion.Euler(0, -45, 0) * dir;
            if (dir.magnitude > 0.1f)
            {
                //面向移動方向
                float faceAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
                audioSource.clip = walkAudio;

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

            }
            else
            {
                audioSource.clip = null;
            }
            animator.SetFloat("dir", dir.magnitude);
            //transform.position += dir * speed * Time.deltaTime;
            character.Move(dir * speed * Time.deltaTime);


        }
    }

    // 攻擊輸入
    void UpdateAttack()
    {
        if (Input.GetMouseButtonDown(0) && !isAtteck)
        {
            Attack();
        }

        if (attackTimer != 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                //重製攻擊段數
                attackTimer = 0;
                comboStep = 0;
            }
        }

    }

    // 攻擊
    void Attack()
    {
        isAtteck = true;
        comboStep++;
        if (comboStep > 3)
            comboStep = 1;
        attackTimer = interval;
        animator.SetTrigger("Attack");
        animator.SetInteger("ComboStep", comboStep);

        animator.SetFloat("dir", 0);

        //面向鼠標
        var dir_r = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir_r.x, dir_r.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angle - 45, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1f);
    }

    // 結束攻擊狀態
    void AttaclOver()
    {
        if (!isPressLeftMouse)
            isAtteck = false;
        if (isPressLeftMouse)
        {
            Attack();

        }
    }

    // 衝刺
    void Dash()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        if (!isAtteck)
        {
            if (!isDash)
            {
                if (dashElapsedTime >= dashCD)
                {
                    // CD結束
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        isDash = true;
                        dashElapsedTime = 0;
                        animator.SetTrigger("Dash");
                        audioSource.PlayOneShot(dashAudio);

                        Instantiate(dashVFX, transform.position - transform.up * 0.3f, transform.rotation);
                    }
                }
                else
                {
                    // 計算CD
                    dashElapsedTime += Time.deltaTime;
                }
            }
        }
    }

    // 攻擊特效
    void AttackEffectsPlay(string count)
    {
        if (count == "1")
            Instantiate(attackEffects01, transform.position + transform.forward * 0.5f, transform.rotation);
        if (count == "2")
            Instantiate(attackEffects02, transform.position + transform.forward * 0.5f, transform.rotation);
        if (count == "3-1")
            attackEffects03_1.Play();
        if (count == "3-2")
            Instantiate(attackEffects03_2, transform.position + transform.forward * 0.5f, transform.rotation).transform.parent = transform;
    }

    // 攻擊音效
    void AttackAudioPlay(string count)
    {
        if (count == "1")
        {
            audioSource.PlayOneShot(attackAudio01);
        }
        if (count == "2")
        {
            audioSource.PlayOneShot(attackAudio02);
        }
        if (count == "3")
        {
            audioSource.PlayOneShot(attackAudio03);
        }
    }

    // 被攻擊時計算傷害
    public void PlayerBeAttack(float damege)
    {
        if (!isTheEnd)
        {
            playerHp -= damege;
            isChangePlayerMaterials = true;

            audioSource.PlayOneShot(beAttackAudio);


            if (playerHp <= 0)
            {
                diedUI.gameObject.SetActive(true);

                isPressLeftMouse = false;
                isDying = true;
            }
        }
    }

    // 被攻擊時切換材質
    void ChangePlayerMaterials()
    {
        // 切換材質
        if (changeColorTimer == 0)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetColor("_Color", Color.red);
            }
        }

        changeColorTimer += Time.deltaTime;

        if (changeColorTimer >= changeColorMaxTime)
        {
            ResetPlayerMaterials();
            isChangePlayerMaterials = false;
            changeColorTimer = 0;
        }
    }

    // 重製材質
    void ResetPlayerMaterials()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetColor("_Color", Color.white);
        }
    }

    // 角色死亡
    public void PlayerDie()
    {

        goldCount = 0;
        this.transform.position = new Vector3(0, transform.position.y, -2f);

        damege = baseDamege;
        playetMaxHp = baseHp;
        playerHp = playetMaxHp;
        speed = baseSpeed;

        Debug.Log(playerHp);

        levelRewardType = LevelRewardType.Null;

        MapController controller = GameObject.Find("MapController").GetComponent<MapController>();
        controller.mapsCount = 0;

        skillManager.SkillLvUp(3);



        //刪除所有敵人
        GameObject[] enemy;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].GetComponent<Enemy>().BeAttacked(1000, false);
        }

        GameObject boss;
        boss = GameObject.FindWithTag("Boss");
        if (boss)
        {
            Destroy(boss.gameObject);
        }


        if (!isDied)
        {
            fungusControl.OnPlayerFirstDie();
            isDied = true;
        }
    }

    // 角色回血
    public void PlayerHeal(float heal)
    {
        if (playerHp < playetMaxHp - heal)
        {
            playerHp += heal;
        }
        else
        {
            playerHp = playetMaxHp;
        }
    }

    public void AttrackMoveOn()
    {
        attackMove = true;
    }
    public void AttrackMoveOff()
    {
        attackMove = false;
        rb.velocity = Vector3.zero;
    }

    public void addPositiveSkill(PassiveSkill skill)
    {
        // 用class名稱判斷是否已經存在
        bool isExists = false;
        for (int i = 0; i < passiveSkills.Count; i++)
        {
            if (skill == passiveSkills[i])
            {
                isExists = true;
            }
        }

        // 加入
        if (!isExists)
        {
            passiveSkills.Add(skill);
        }
    }

    public void CheckPassiveSkills(string n)
    {
        for (int i = 0; i < passiveSkills.Count; i++)
        {
            PassiveSkill s = passiveSkills[i];

            s.GetType().GetMethod(n).Invoke(s, new object[] { this });
        }
    }

    public void SaveGame()
    {
        save.SaveGame(goldCount, crystalCount, isDied);

        for (int i = 0; i < passiveSkills.Count; i++)
        {
            save.SavePassiveSkil(passiveSkills[i].ToString());
        }


    }

    void LoadGame()
    {
        goldCount = save.goldCount;
        crystalCount = save.crystalCount;
        isDied = Convert.ToBoolean(PlayerPrefs.GetFloat("isDied"));

        if (PlayerPrefs.HasKey("LifeSteal"))
        {
            addPositiveSkill(new LifeSteal());
        }
        if (PlayerPrefs.HasKey("NextRoomHealth"))
        {
            addPositiveSkill(new NextRoomHealth());
        }
        if (PlayerPrefs.HasKey("Berserker"))
        {
            addPositiveSkill(new Berserker());
        }
        if (PlayerPrefs.HasKey("InitialGold"))
        {
            addPositiveSkill(new InitialGold());
        }
        if (PlayerPrefs.HasKey("IncreaseMaxHp"))
        {
            addPositiveSkill(new IncreaseMaxHp());
        }
        if (PlayerPrefs.HasKey("IncreaseDamage"))
        {
            addPositiveSkill(new IncreaseDamage());
        }

    }

    public void StopAudio()
    {
        audioSource.Stop();
    }


}
