using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : MonoBehaviour
{
    public float damege;

    List<Enemy> enteredEnemys;

    void Start()
    {
        enteredEnemys = new List<Enemy>();
        Invoke("DestroySkill", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (!enteredEnemys.Contains(enemy))
            {
                enteredEnemys.Add(enemy);
                enemy.AddDebuff(EnemyDebuffType.Frozen);
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().BeAttacked(damege, false);
        }
    }

    void DestroySkill()
    {
        foreach(Enemy enemy in enteredEnemys)
        {
            enemy.RemoveDebuff(EnemyDebuffType.Frozen);
        }

        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if(enteredEnemys.Contains(enemy))
            {
                enteredEnemys.Remove(enemy);
                enemy.RemoveDebuff(EnemyDebuffType.Frozen);
            }
        }
    }
}
