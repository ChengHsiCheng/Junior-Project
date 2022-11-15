using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveSkill
{
    public abstract void OnPlayerAttack(Player player);

    public abstract void OnNextRoom(Player player);

    public abstract void OnStart(Player player);

}
