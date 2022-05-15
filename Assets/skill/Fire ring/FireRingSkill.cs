using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRingSkill : SkillController
{
    public FireRing fireRingSource;
    public bool isUsing = false;

    public FireRingSkill()
    {
        CD = 1;
        skillElapsedTime = 0;
        isUsing = false;
    }
    public override void SkillStart()
    {
        if(skillElapsedTime <= 0)
        {
            isUsing = true;
            skillElapsedTime = CD;
            FireRing fireRing = Instantiate<FireRing>(fireRingSource, transform.position, fireRingSource.transform.rotation);
            fireRing.OnFireRingUsing += FireRingFinishUsing;//註冊事件:效果結束
        }
    }
    public override void SkillUpdate()
    {
        //計算CD
        if(!isUsing && skillElapsedTime >= 0)
        {
            skillElapsedTime -= Time.deltaTime;
        }
    }

    //承接委派:效果結束
    void FireRingFinishUsing()
    {
        isUsing = false;
    }
}
