using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill02 : SkillControl
{
    Vector3 pos;
    GameObject skillObj;
    FireBall fireBall;

    bool isShoot;
    float shootTimer;
    int fireBallCount;
    bool isCharge;
    float chargeTimer;
    public float maxChargeTime;
    public float fireBallMaxVal;
    public override void SkillStart()
    {
        if (skillPos == null)
        {
            skillPos = GameObject.Find("SkillPos");
        }
        pos = skillPos.transform.position + skillPos.transform.forward * 0.5f;
        if (skillTimer <= 0 && !isShoot)
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

        if (isCharge && !isShoot)
        {
            pos = skillPos.transform.position + skillPos.transform.forward * 0.5f;

            // 設定位置、旋轉、大小
            fireBall.Charge(pos, skillPos.transform.rotation, new Vector3(chargeTimer, chargeTimer, chargeTimer));
            chargeTimer += Time.deltaTime;

            if (Input.GetMouseButtonUp(1) || chargeTimer > maxChargeTime)
            {
                // 發射
                isCharge = false;
                isShoot = true;
                fireBall.Shoot(damege * chargeTimer);
            }
        }

        if (isShoot)
        {
            shootTimer += Time.deltaTime;


            if (shootTimer > 0.2f)
            {
                if (fireBallCount < fireBallMaxVal - 1)
                {
                    ShootFireBall(false);
                    fireBallCount += 1;
                }
                else if (fireBallCount == fireBallMaxVal - 1)
                {
                    ShootFireBall(true);
                    fireBallCount = 0;
                }
                shootTimer = 0;
            }
        }

        if (skillTimer > 0)
        {
            skillTimer -= Time.deltaTime;
        }

    }

    void ShootFireBall(bool end)
    {
        float val = chargeTimer / 2;
        pos = skillPos.transform.position + skillPos.transform.forward * 0.5f;
        skillObj = Instantiate(skill, pos, skillPos.transform.rotation);
        fireBall = skillObj.GetComponent<FireBall>();
        fireBall.damege = damege;
        fireBall.Charge(pos, skillPos.transform.rotation, new Vector3(val, val, val));
        fireBall.Shoot(damege * val);

        if (end)
        {
            End();
        }
    }

    void End()
    {
        chargeTimer = 0;
        isShoot = false;
        skillTimer = skillCD;
    }
}
