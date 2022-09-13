using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpbar : MonoBehaviour
{
    public EnemyStatusInfo TargetEnemy;
    public Image Bar;
    public GameObject canvas;

    private RectTransform rectTrans;

    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        imagetocanvas();
    }

    void Update()
    {
        UpdatePosition();
        UpdateBarWidth();
        if(TargetEnemy.hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdatePosition() // 更新血條座標
    {
        Vector3 enemyScreenPos = Camera.main.WorldToScreenPoint(TargetEnemy.transform.position);//目標位置
        Vector3 offset = new Vector3(0, 50, 0);//血條相對目標位置
        rectTrans.position = enemyScreenPos + offset;
    }

    void UpdateBarWidth() // 更新血條長度
    {
        float hpScale = TargetEnemy.hp  * 0.01f;
        Bar.rectTransform.localScale = new Vector3
        (
            hpScale,
            Bar.rectTransform.localScale.y,
            Bar.rectTransform.localScale.z
        );
    }
    void imagetocanvas() //把image放到canvas上
    {
        canvas=GameObject.Find("Canvas_hpbar");
        transform.SetParent(canvas.transform);
    }
}
