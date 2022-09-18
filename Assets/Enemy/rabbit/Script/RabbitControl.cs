using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitControl : MonoBehaviour
{
    public EnemyRabbit enemyRabbit;

    public void AttackMoveOn()
    {
        enemyRabbit.AttackMoveOn();
    }

    public void AttackMoveOff()
    {
        enemyRabbit.AttackMoveOff();
    }

    public void BeAttackMoveOn()
    {
        enemyRabbit.BeAttackMoveOn();
    }

    public void BeAttackMoveOff()
    {
        enemyRabbit.BeAttackMoveOff();
    }

    public void Destroy()
    {
        enemyRabbit.Destroy();
    }
}
