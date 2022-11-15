using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomHealth : PassiveSkill
{
    public override void OnPlayerAttack(Player player)
    {
    }

    public override void OnNextRoom(Player player)
    {
        if (player.playerHp < player.playetMaxHp - 5)
        {
            player.playerHp += 5;
        }
        else
        {
            player.playerHp = player.playetMaxHp;
        }
    }

    public override void OnStart(Player player)
    {
    }
}
