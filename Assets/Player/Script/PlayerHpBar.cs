using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Player player;
    public Image Bar;

    private RectTransform rectTrans;

    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
    }

    void Update()
    {
        UpdatePosition();
        UpdateBarWidth();
        // if(player.playerHp <= 0)
        // {
        //     Destroy(gameObject);
        // }
    }

    // 更新血條座標
    void UpdatePosition()
    {
        Vector3 enemyScreenPos = Camera.main.WorldToScreenPoint(player.transform.position);//目標位置
        Vector3 offset = new Vector3(0, 50, 0);//血條相對目標位置
        rectTrans.position = enemyScreenPos + offset;
    }

    // 更新血條長度
    void UpdateBarWidth() {
        float hpScale = player.playerHp  * 0.01f;
        Bar.rectTransform.localScale = new Vector3
        (
            hpScale,
            Bar.rectTransform.localScale.y,
            Bar.rectTransform.localScale.z
        );
    }
}
