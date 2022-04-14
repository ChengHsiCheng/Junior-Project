using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float dashDuration = 0.2f;//衝刺時間
    float dashTime;//衝刺經過的時間
    float dashSpeed = 1;
    bool isDash;//是否在衝刺
    bool isAttack = false;
    CharacterController controller;
    Animator animator;
    float speed = 5;
    public int attackCount = 0;
    string attackAnimaCount ="";
    void Start()
    {
        controller=GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        dashTime = dashDuration;
        StartCoroutine(AttackLogic());
    }
    void Update()
    {
        if(isAttack)
        {
            animator.SetBool("run",false);
        }else if(!isAttack)
        {
            Move();
            Dash();
        }
        Attack();
    }
    void Move()
    {
        //移動
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
        if(dir.magnitude > 0.1f)
        {
            float faceAngle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
        }

        controller.Move( dir * speed * Time.deltaTime );
    }
    void Dash()
    {
        //衝刺
        animator.SetBool("dash",isDash);
        if(!isDash)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isDash = true;
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
                Debug.Log("A");
                dashTime -= Time.deltaTime;
                controller.Move(transform.forward * dashTime * dashSpeed);
            }
        }
    }
    void Attack()
    {
        AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle") || stateInfo.IsName("run") || stateInfo.IsName("Dash"))
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
            controller.Move(transform.forward * Time.deltaTime * 10);
        }

        if (stateInfo.IsName("Attack1"))
        {
            attackAnimaCount = "Attack1";
        }

        if (Input.GetMouseButton(0))
        {
            // 監聽用戶輸入（假設左鍵爲攻擊鍵）
            AttackButton();
        }
    }
    void AttackButton()
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
            // 在攻擊1狀態下，按下攻擊鍵，記錄連擊數爲2（切換狀態在Update()中
            attackCount = 2;
        }
        else if (stateInfo.IsName("Attack2") && (stateInfo.normalizedTime > 0.4f))
        {
            // 在攻擊2狀態下，按下攻擊鍵，記錄連擊數爲3（切換狀態在Update()中）
            attackCount = 3;
        }
    }
    IEnumerator AttackLogic()
    {
        AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        while(true)
        {
            while(attackAnimaCount == "Attack1")
            {
                Debug.Log("A");
                yield return null;
            }
            yield return null;
        }
    }
}
