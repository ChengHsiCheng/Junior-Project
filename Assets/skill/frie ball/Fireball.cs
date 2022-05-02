using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject ball;
    Enemy TargetEnemy;
    ParticleSystem particle;
    ParticleSystem.ShapeModule shape;
    float ChargeTime = 0;
    public float SkillCD = 1;
    bool OnClickMouse = false;
    bool IsShoot = false;//是否發射了
    void Start()
    {
        particle = ball.GetComponent<ParticleSystem>();
        shape = particle.shape;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsShoot)
        {
            if(Input.GetMouseButton(1) && shape.radius <= 0.7f)
            {
                //設定火球與玩家的相對位置
                transform.rotation = GameObject.Find("skill").transform.rotation;
                transform.position = GameObject.Find("skill").transform.position;
                shape.radius += 0.2f * Time.deltaTime;
                ChargeTime += Time.deltaTime;
            }if(shape.radius > 0.7)
            {
                Debug.Log(ChargeTime);
                IsShoot = true;
            }
        }if(IsShoot)
        {
            //向前移動
            transform.Translate(0,0,10 * Time.deltaTime);
        }

        if(Input.GetMouseButtonUp(1))
        {
            IsShoot = true;
        }
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {
            TargetEnemy = other.GetComponent<Enemy>();
            TargetEnemy.Hp -= ChargeTime * 10;
            Destroy(gameObject,0.5f);
        }
    }
}

