using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill01 : SkillControl
{
    Vector3 pos;
    GameObject skillObj;
    FireBall fireBall;

    bool isCharge;
    float chargeTimer;
    public float maxChargeTime;
    public override void SkillStart()
    {
        if (skillPos == null)
        {
            skillPos = GameObject.Find("SkillPos");
        }
        pos = skillPos.transform.position + skillPos.transform.forward * 0.5f;
        if (skillTimer <= 0)
        {
            isCharge = true;
            // 生成火球
            skillObj = Instantiate(skill, pos, skillPos.transform.rotation);
            fireBall = skillObj.GetComponent<FireBall>();
            fireBall.damege = damege;
        }
    }

    public override void SkillUpdate()
    {
        if (isCharge)
        {
            pos = skillPos.transform.position + skillPos.transform.forward * 0.7f;

            // 設定位置、旋轉、大小
            fireBall.Charge(pos, skillPos.transform.rotation, new Vector3(chargeTimer, chargeTimer, chargeTimer));
            chargeTimer += Time.deltaTime;

            if (Input.GetMouseButtonUp(1) || chargeTimer > maxChargeTime)
            {
                // 發射
                isCharge = false;
                fireBall.Shoot(damege * chargeTimer);
                skillTimer = skillCD;
                chargeTimer = 0;
            }
        }

        if (skillTimer > 0)
        {
            skillTimer -= Time.deltaTime;
        }
    }

}
