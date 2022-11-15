using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : PassiveSkill
{
    public override void OnPlayerAttack(Player player)
    {
        player.damegeAdd = 1 + (1 - player.playerHp / player.playetMaxHp);
    }

    public override void OnNextRoom(Player player)
    {
    }

    public override void OnStart(Player player)
    {
    }
}
