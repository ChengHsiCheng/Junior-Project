using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Collision : MonoBehaviour
{
    public Player player;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            Instantiate(player.hitEffecis, other.transform.position + transform.up * enemy.high, Quaternion.identity);

            player.CheckPassiveSkills("OnPlayerAttack");

            if (player.comboStep != 3)
            {
                enemy.BeAttacked(player.damege * player.damegeAdd, true);
            }
            if (player.comboStep == 3)
            {
                enemy.BeAttacked(player.damege * player.damegeAdd * player.critAdd, true);
            }


        }
        else if (other.tag == "Boss")
        {
            Boss boss = other.GetComponent<Boss>();

            Instantiate(player.hitEffecis, other.transform.position + transform.up * 0.5f, Quaternion.identity);

            player.CheckPassiveSkills("OnPlayerAttack");

            if (player.comboStep != 3)
            {
                boss.BeAttack(player.damege * player.damegeAdd);
            }
            if (player.comboStep == 3)
            {
                boss.BeAttack(player.damege * player.damegeAdd * player.critAdd);
            }
        }
    }
}
