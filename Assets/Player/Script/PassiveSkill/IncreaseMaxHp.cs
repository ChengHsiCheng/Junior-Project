using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxHp : PassiveSkill
{
    public override void OnPlayerAttack(Player player)
    {
    }

    public override void OnNextRoom(Player player)
    {
    }

    public override void OnStart(Player player)
    {
        player.baseHp += 50;
        player.playetMaxHp = player.baseHp;
        player.playerHp = player.playetMaxHp;
    }
}
