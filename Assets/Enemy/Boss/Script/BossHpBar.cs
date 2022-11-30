using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHpBar : MonoBehaviour
{
    public GameObject canvas;
    public Boss boss;
    public Image hpBar;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = boss.hp / boss.maxHp;

        if (boss.hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
