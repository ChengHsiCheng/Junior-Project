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
        Imagetocanvas();
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

    void Imagetocanvas() //把image放到canvas上
    {
        canvas = GameObject.Find("Canvas_hpbar");
        transform.SetParent(canvas.transform);
    }
}
