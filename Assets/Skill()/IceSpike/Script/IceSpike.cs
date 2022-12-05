using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : MonoBehaviour
{
    public float damege;

    List<Enemy> enteredEnemys;

    AudioSource source;
    public AudioClip audioClip;

    void Start()
    {
        enteredEnemys = new List<Enemy>();
        source = GetComponent<AudioSource>();

        source.PlayOneShot(audioClip);
        Invoke("DestroySkill", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
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
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().BeAttacked(damege, false);
        }
        else if (other.tag == "Boss")
        {
            Boss boss = other.gameObject.GetComponent<Boss>();
            boss.BeAttack(damege);
        }
    }

    void DestroySkill()
    {
        foreach (Enemy enemy in enteredEnemys)
        {
            enemy.RemoveDebuff(EnemyDebuffType.Frozen);
        }

        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (enteredEnemys.Contains(enemy))
            {
                enteredEnemys.Remove(enemy);
                enemy.RemoveDebuff(EnemyDebuffType.Frozen);
            }
        }
    }
}
