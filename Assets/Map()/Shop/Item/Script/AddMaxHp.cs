using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMaxHp : Item
{
    public override void BuyItem(Player player)
    {
        player.playetMaxHp += 25;
        player.playerHp += 25;
    }
}
