using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : MonoBehaviour
{
    float dashDuration = 0.2f;//衝刺時間
    float dashTime = 0;//衝刺的時間
    float dashSpeed = 100f;//衝刺速度
    float dashCD = 0.5f;//衝刺的冷卻時間
    float dashElapsedTime = 0;//衝刺後經過的時間
    bool isDash = false;//是否在衝刺
    int comboStep = 0;//攻擊段數
    float interval = 0.5f;//攻擊間隔
    float timer = 0;//攻擊間隔計算
    bool isAtteck = false;//是否在攻擊
    bool isPressLeftMouse = false;//是否按下滑鼠左鍵
    public float speed = 3;//移動速度
    Animator animator;
    CharacterController controller;
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Attack();
        Move();
    }
    void Move()//移動
    {
        if(isAtteck)
        {
            //攻擊時向前移動
            controller.Move(transform.forward * Time.deltaTime * 1);
        }if(!isAtteck && timer == 0)
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

            Dash();

        }
    }
    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && !isAtteck)
        {
            isAtteck = true;
            comboStep++;
            if(comboStep > 3)
                comboStep = 1;
            timer = interval;
            animator.SetTrigger("Attack");
            animator.SetInteger("ComboStep",comboStep);

            //面向鼠標
            var dir_r = Input.mousePosition-Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir_r.x,dir_r.y)*Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1f);
        }

        if(timer != 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                //重製攻擊段數
                timer = 0;
                comboStep = 0;
            }
        }

    }
    void AttaclOver()
    {
        isAtteck = false;
    }

    void Dash()//衝刺
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        Vector3 dashDir = new Vector3(Mathf.Round(dir.x),Mathf.Round(dir.y),Mathf.Round(dir.z));
        
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
                if(dir != Vector3.zero)//是否有方向輸入
                {
                    //朝輸入方向衝刺
                    controller.Move(dashDir * dashTime * dashSpeed * Time.deltaTime);
                }else
                {
                    //朝面相方向衝刺
                    controller.Move(this.transform.forward * dashTime * dashSpeed * Time.deltaTime);
                }

            }
        }
    }
}
