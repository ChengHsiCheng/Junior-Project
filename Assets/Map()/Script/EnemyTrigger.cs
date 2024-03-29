using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public GameObject[] enemys;
    public MapController controller;
    public List<GameObject> GeneratePos = new List<GameObject> { };

    public EnemyParmType parmType;
    public float enemyHpAddition;
    public float enemyDamegeAddition;
    public float enemySpeedAddition;

    public GameObject endProtal;
    public GameObject bossDieFungus;

    private void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            //把地圖放進陣列中
            GameObject gameObject = this.gameObject.transform.GetChild(i).gameObject;
            GeneratePos.Add(gameObject);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < GeneratePos.Count; i++)
            {
                GameObject enemyObj = Instantiate(enemys[Random.Range(0, enemys.Length)], GeneratePos[i].transform.position, Quaternion.Euler(0, 180, 0));

                Enemy enemy = enemyObj.GetComponent<Enemy>();



                if (enemyObj.GetComponent<Boss>())
                {
                    Boss boss = enemyObj.GetComponent<Boss>();
                    boss.endProtal = endProtal;
                    boss.bossDieFungus = bossDieFungus;
                }


                if (enemy)
                {
                    EnemyStatusInfo info = enemyObj.GetComponent<EnemyStatusInfo>();
                    info.SetParm(enemyHpAddition, enemyDamegeAddition, enemySpeedAddition);
                    enemy.OnEnemyDie += OnEnemyDie;//註冊事件:效果結束
                }



            }

            this.gameObject.SetActive(false);
        }
    }


    void OnEnemyDie()
    {
        controller.EnemtCheck();
    }

    public void SetParmValue(float hp, float damege, float speed)
    {
        enemyHpAddition = hp / 100;
        enemyDamegeAddition = damege / 100;
        enemySpeedAddition = speed / 100;
    }
}
