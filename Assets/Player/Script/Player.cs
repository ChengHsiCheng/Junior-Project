using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 變數
    float dashDuration = 0.2f;//衝刺時間
    float dashTime = 0;//衝刺的時間
    float dashSpeed = 0.5f;//衝刺速度
    float dashCD = 0.5f;//衝刺的冷卻時間
    float dashElapsedTime = 0;//衝刺後經過的時間
    bool isDash = false;//是否在衝刺
    bool isAttack = false;//是否在攻擊
    public float playerHp = 100;
    CharacterController controller;
    Animator animator;
    float speed = 5;//移動速度
    int attackCount = 0;//儲存攻擊段數
    public int attackHit = 0;//目前攻擊段數
    bool isPressLeftMouse = false;//是否觸發滑鼠左鍵
    public GameObject weaponCollision;//攻擊判定框
    public GameObject FireBall;//火球
    #endregion
    void Start()
    {
        controller=GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        dashTime = dashDuration;
    }
    void Update()
    {
        if(isAttack)//是否可以移動、衝刺
        {
            animator.SetBool("run",false);
        }else if(!isAttack)
        {
            Move();
            Dash();
        }
        Attack();
        AttackTrigger();
    }
    void Move()//移動
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        if(dir.magnitude > 0.1f)
        {
            animator.SetBool("run",true);
        }
        else
        {
            animator.SetBool("run",false);
        }

        //面相移動方向
        // if(dir.magnitude > 0.1f)
        // {
        //     float faceAngle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
        //     Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
        //     transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
        // }
        //面對鼠標
        var dir_r = Input.mousePosition-Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir_r.x,dir_r.y)*Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);

        controller.Move( dir * speed * Time.deltaTime );
    }
    void Dash()//衝刺
    {
        animator.SetBool("dash",isDash);
        if(!isDash)
        {
            if(dashElapsedTime >= dashCD)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    isDash = true;
                    dashElapsedTime = 0;
                }
            }else
            {
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
                controller.Move(transform.forward * dashTime * dashSpeed);
            }
        }
    }
    void Attack()//攻擊控制器
    {
        AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle") || stateInfo.IsName("run") || stateInfo.IsName("Dash"))//判斷是否在攻擊狀態
        {
            isAttack = false;
        }else
        {
            isAttack = true;
        }

        if (!stateInfo.IsName("Idle") || !stateInfo.IsName("run"))
        {
            // 每次設置完參數之後，都應該在下一幀開始時將參數設置清空，避免連續切換
            this.animator.SetInteger("attack", 0);
        }

        if (stateInfo.IsName("Attack1") && (stateInfo.normalizedTime > 0.4f) && (attackCount == 2))
        {
            // 當在攻擊1狀態下，並且當前狀態運行了0.4正交化時間（即動作時長的40%），並且用戶在攻擊1狀態下又按下了“攻擊鍵”
            this.animator.SetInteger("attack", 1);
        }

        if (stateInfo.IsName("Attack2") && (stateInfo.normalizedTime > 0.4f) && (attackCount == 3))
        {
            // 擋在攻擊2狀態下（同理攻擊1狀態）
            this.animator.SetInteger("attack", 1);
        }

        if (stateInfo.IsName("Attack3") && (stateInfo.normalizedTime >= 0.2f) && (stateInfo.normalizedTime <= 0.4f))
        {
            //攻擊第3段時往前移動
            controller.Move(transform.forward * Time.deltaTime * 10);
        }

        if (stateInfo.IsName("Attack1"))
        {
            attackHit = 1;
        }else if(stateInfo.IsName("Attack2"))
        {
            attackHit = 2;
        }else if(stateInfo.IsName("Attack3"))
        {
            attackHit = 3;
        }else
        {
            attackHit = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isPressLeftMouse = true;
        }else if(Input.GetMouseButtonUp(0))
        {
            isPressLeftMouse = false;
        }
        if(isPressLeftMouse)
        {
            AttackButton();
        }
    }
    void AttackButton()//監聽用戶輸入
    {
        AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle") || stateInfo.IsName("run"))
        {
            // 在待命狀態下，按下攻擊鍵，進入攻擊1狀態，並記錄連擊數爲
            this.animator.SetInteger("attack", 1);
            attackCount = 1;
        }
        else if (stateInfo.IsName("Attack1") && (stateInfo.normalizedTime > 0.4f))
        {
            // 在攻擊1狀態下，按下攻擊鍵，記錄連擊數爲2
            attackCount = 2;
        }
        else if (stateInfo.IsName("Attack2") && (stateInfo.normalizedTime > 0.4f))
        {
            // 在攻擊2狀態下，按下攻擊鍵，記錄連擊數爲3
            attackCount = 3;
        }
    }
    void AttackTrigger()//設定攻擊判定
    {
        AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Attack1")  || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3"))
        {
            if(stateInfo.normalizedTime >= 0.2 && stateInfo.normalizedTime <= 0.8)
            {
                weaponCollision.SetActive(true);
            }
            else
            {
                weaponCollision.SetActive(false);
            }
        }
        else
        {
            weaponCollision.SetActive(false);
        }

    }

    public void Damege(float damege)
    {
        playerHp -= damege;
        Debug.Log(playerHp);
    }
}
