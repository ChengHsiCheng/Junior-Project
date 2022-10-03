using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkillTest : SkillController
{
    Vector3 pos;
    GameObject skillObj;
    FireBallTest fireBall;
    bool isCharge;
    float chargeTimer;
    public float maxChargeTime;
    public override void SkillStart()
    {
        pos = skillPos.transform.position + skillPos.transform.forward * 0.5f;
        if(skillTimer >= skillCD)
        {
            isCharge = true;
            // 生成火球
            skillObj = Instantiate(skill, pos, skillPos.transform.rotation);
            fireBall = skillObj.GetComponent<FireBallTest>();
            fireBall.damege = damege;
            skillTimer = 0;
        }
    }

    public override void SkillUpdate()
    {
        pos = skillPos.transform.position + skillPos.transform.forward * 0.5f;

        if(isCharge)
        {
            // 設定位置、旋轉、大小
            fireBall.transform.position = pos;
            fireBall.transform.rotation = skillPos.transform.rotation;
            fireBall.transform.localScale = new Vector3(chargeTimer, chargeTimer, chargeTimer);
            chargeTimer += Time.deltaTime;

            if(Input.GetMouseButtonUp(1) || chargeTimer > maxChargeTime)
            {
                // 發射
                isCharge = false;
                fireBall.Shoot();
                chargeTimer = 0;
            }
        }

        if(skillTimer < skillCD)
        {
            skillTimer += Time.deltaTime;
        }

        
    }
}
