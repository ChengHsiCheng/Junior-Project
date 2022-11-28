using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAttack : Item
{
    public override void BuyItem(Player player)
    {
        player.damege += 10;
        Debug.Log("A");
    }

}
