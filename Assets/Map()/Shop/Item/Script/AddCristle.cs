using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCristle : Item
{
    public override void BuyItem(Player player)
    {
        player.crystalCount += 20;
    }
}
