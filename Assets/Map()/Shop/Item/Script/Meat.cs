using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : Item
{
    public override void BuyItem(Player player)
    {
        if (player.playerHp < player.playetMaxHp - 25)
        {
            player.playerHp += 25;
        }
        else
        {
            player.playerHp = player.playetMaxHp;
        }
    }
}
