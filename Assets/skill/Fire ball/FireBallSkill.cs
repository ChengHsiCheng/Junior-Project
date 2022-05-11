using System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : SkillController
{
    public Fireball fireBallSource;
    public Fireball fireBall = null;
    public float chargeTime = 0;//蓄力時間
    public bool IsShoot = false;//是否發射了

    public FireBallSkill()
    {
        CD = 1;
    }


    public override void Use()
    {
        IsShoot = false;
        // 複製產生出來的物件
        fireBall = Instantiate<Fireball>(fireBallSource, transform.position, fireBallSource.transform.rotation);
    }


    public override void SkillUpdate()
    {
        if(fireBall)
        {
           if(Input.GetMouseButtonUp(1))
           {
               IsShoot = true;
               OnStartCountCD(this);
           }
        }
    }

}
