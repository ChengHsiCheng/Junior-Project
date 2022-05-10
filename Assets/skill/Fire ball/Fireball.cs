using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : SkillController
{
    public GameObject ball;
    Player player;
    SkillEvent skillEvent;
    EnemyStatusInfo TargetEnemy;
    ParticleSystem particle;
    ParticleSystem.ShapeModule shape;
    ParticleSystem.CollisionModule collision;
    float ChargeTime = 0;//蓄力時間
    public LayerMask desiredLayers; //取消碰撞層
    public LayerMask enemyLayers;//碰撞層:敵人
    float damege;//傷害
    bool IsShoot = false;//是否發射了

    void Start()
    {
        particle = ball.GetComponent<ParticleSystem>();
        shape = particle.shape;
        collision = GetComponent<ParticleSystem>().collision;
        player = GameObject.Find("Player").GetComponent<Player>();
        skillEvent = player.GetComponent<SkillEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsShoot)
        {
            if(shape.radius <= 0.7f)
            {
                //設定火球與玩家的相對位置
                transform.rotation = GameObject.Find("FireBallPos").transform.rotation;
                transform.position = GameObject.Find("FireBallPos").transform.position;
                collision.collidesWith = desiredLayers;
                shape.radius += 0.2f * Time.deltaTime;
                ChargeTime += Time.deltaTime;
            }if(shape.radius > 0.7)
            {
                IsShoot = true;
            }
        }if(IsShoot)
        {
            //向前移動
            transform.Translate(0,0,10 * Time.deltaTime);
            collision.collidesWith = enemyLayers;
            Destroy(gameObject,2);
        }

        if(Input.GetMouseButtonUp(1) && shape.radius <= 0.7f)
        {
            IsShoot = true;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {//計算傷害
            TargetEnemy = other.GetComponent<EnemyStatusInfo>();
            damege = ChargeTime * 10;
            TargetEnemy.Damege(damege);
            Destroy(gameObject,0.5f);
        }
    }
}

