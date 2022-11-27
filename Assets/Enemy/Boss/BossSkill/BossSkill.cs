using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public Player player;
    public GameObject fireBall;
    public Boss boss;
    float timer;
    int i;
    int[] skillTimer = { 1, 2, 3 };
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > skillTimer[i])
        {
            ShootFireBall();
            if (i < skillTimer.Length - 1)
            {
                i++;
            }
            else if (i < skillTimer.Length)
            {
                Destroy(gameObject);
            }

        }
    }

    void ShootFireBall()
    {
        BossFireBall bossFireBall = Instantiate(fireBall, transform.position, Quaternion.identity).GetComponent<BossFireBall>();

        player = GameObject.Find("Player").GetComponent<Player>();

        bossFireBall.GetPlayer(player, boss.damege);
    }
}
