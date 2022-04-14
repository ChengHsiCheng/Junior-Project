using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DESH : MonoBehaviour
{
    Animator animator;
    CharacterController controller;
    AnimatorStateInfo stateInfo;
    public Text text;
    public int attackCount = 0;
    float attackDelay = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        controller=GetComponent<CharacterController>();
        StartCoroutine(AttackLogic());
        stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        text.text = attackCount.ToString();
    }
    void Attack()
    {
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

        if (stateInfo.IsName("Attack3") && (stateInfo.normalizedTime <= 0.5f))
        {
            controller.Move(transform.forward * Time.deltaTime * 5);
        }

        if(Input.GetMouseButton(0))
        {
            AttackButton();
        }

    }
    void AttackButton()
    {
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
        while(true)
        {
            while (stateInfo.IsName("Attack1") || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3"))
            {
                Debug.Log("A");
                yield return 0;
            }
            yield return null;
        }
    }
}
