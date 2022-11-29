using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpeed : Item
{
    public override void BuyItem(Player player)
    {
        player.speed += 0.5f;
    }
}
