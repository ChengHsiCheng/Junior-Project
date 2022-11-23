using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    public GameObject attackVFX;

    void OnPlayAttackVFX()
    {
        Bubble bubble = Instantiate(attackVFX, transform.position + transform.forward * 0.15f - transform.up * 0.1f, transform.rotation).GetComponent<Bubble>();
        bubble.SteDamege(info.damege * info.damegeAddition);
    }
}
