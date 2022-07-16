using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject ball;
    public FireBallSkill fireBallSkill;
    Player player;
    EnemyStatusInfo TargetEnemy;
    ParticleSystem particle;//粒子系統
    public ParticleSystem.ShapeModule shape;//粒子生成區域
    public ParticleSystem.CollisionModule collision;//粒子碰撞
    public float chargeTime = 0;//蓄力時間
    public LayerMask desiredLayers; //取消碰撞層
    public LayerMask enemyLayers;//碰撞層:敵人
    float damege;//傷害
    bool IsShoot = false;
    // bool IsShoot = false;//是否發射了

    public delegate void FireBallShootedEventArgs();
    public FireBallShootedEventArgs OnFireBallShooted;

    void Start()
    {
        particle = ball.GetComponent<ParticleSystem>();
        shape = particle.shape;
        collision = GetComponent<ParticleSystem>().collision;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonUp(1) || chargeTime >= 3)
        {
            IsShoot = true;
        }

        if(!IsShoot)
        {
            //設定火球與玩家的相對位置
            transform.rotation = GameObject.Find("FireBallPos").transform.rotation;
            transform.position = GameObject.Find("FireBallPos").transform.position;
            collision.collidesWith = desiredLayers;//切換碰撞層
            shape.radius += 0.2f * Time.deltaTime;//隨時間變大顆
            chargeTime += Time.deltaTime;
        }if(IsShoot)
        {
            //向前移動
            transform.Translate(0,0,10 * Time.deltaTime);
            collision.collidesWith = enemyLayers;//切換碰撞層
            OnFireBallShooted();//委派事件:發射火球
            Destroy(gameObject,2);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {//計算傷害
            TargetEnemy = other.GetComponent<EnemyStatusInfo>();
            damege = chargeTime * 100;
            TargetEnemy.Damege(damege,true);
            Destroy(ball.gameObject);
            Destroy(gameObject,0.5f);
        }
    }
}

