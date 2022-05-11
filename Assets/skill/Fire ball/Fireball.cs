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
    ParticleSystem particle;
    public ParticleSystem.ShapeModule shape;
    public ParticleSystem.CollisionModule collision;
    public float chargeTime = 0;//蓄力時間
    public LayerMask desiredLayers; //取消碰撞層
    public LayerMask enemyLayers;//碰撞層:敵人
    float damege;//傷害
    // bool IsShoot = false;//是否發射了

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
        if(!fireBallSkill.IsShoot)
        {
            //設定火球與玩家的相對位置
            transform.rotation = GameObject.Find("FireBallPos").transform.rotation;
            transform.position = GameObject.Find("FireBallPos").transform.position;
            collision.collidesWith = desiredLayers;
            shape.radius += 0.2f * Time.deltaTime;
            chargeTime += Time.deltaTime;
        }if(fireBallSkill.IsShoot)
        {
            //向前移動
            transform.Translate(0,0,10 * Time.deltaTime);
            collision.collidesWith = enemyLayers;
            Destroy(gameObject,2);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {//計算傷害
            TargetEnemy = other.GetComponent<EnemyStatusInfo>();
            damege = chargeTime * 10;
            TargetEnemy.Damege(damege);
            Destroy(gameObject,0.5f);
        }
    }
}

