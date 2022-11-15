using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : PassiveSkill
{
    public override void OnPlayerAttack(Player player)
    {
    }

    public override void OnNextRoom(Player player)
    {
    }

    public override void OnStart(Player player)
    {
        player.damege = 150;
    }
}
