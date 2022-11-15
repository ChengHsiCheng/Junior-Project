using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSteal : PassiveSkill
{
    public override void OnPlayerAttack(Player player)
    {
        if (player.playerHp < player.playetMaxHp - 2)
        {
            player.playerHp += 2;
        }
        else
        {
            player.playerHp = player.playetMaxHp;
        }
    }

    public override void OnNextRoom(Player player)
    {
    }

    public override void OnStart(Player player)
    {
    }
}
