using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : PassiveSkill
{
    public override void OnPlayerAttack(Player player)
    {
        player.damege = 15;
    }

    public override void OnNextRoom(Player player)
    {
    }

    public override void OnStart(Player player)
    {

    }
}
