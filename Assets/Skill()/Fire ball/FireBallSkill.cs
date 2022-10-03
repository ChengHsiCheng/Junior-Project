// using System.Data;
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FireBallSkill : SkillController
// {
//     public Fireball fireBallSource;
//     public bool isFireBallCharging = false;

//     public FireBallSkill()
//     {
//         CD = 1;
//         skillElapsedTime = 0;
//         skillIntroduce = "按住右鍵可以集氣，時間越久傷害越高";
//         SkillName = "FireBall";
//     }

//     public override void SkillStart()
//     {
//         if(skillElapsedTime <= 0)
//         {
//             isFireBallCharging = true;
//             skillElapsedTime = CD;
//             // 複製產生出來的物件
//             Fireball fireBall = Instantiate<Fireball>(fireBallSource, transform.position, fireBallSource.transform.rotation);
//             //註冊事件:發射火球
//             fireBall.OnFireBallShooted += FireBallShooted;
//         }
//     }

//     public override void SkillUpdate()
//     {
//         //計算CD
//         if(!isFireBallCharging && skillElapsedTime >= 0)
//         {
//             skillElapsedTime -= Time.deltaTime;
//         }
//     }

//     //承接委派:火球發射出去
//     void FireBallShooted()
//     {
//         isFireBallCharging = false;
//     }
// }
