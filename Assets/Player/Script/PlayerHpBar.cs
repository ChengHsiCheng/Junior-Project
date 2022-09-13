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
        UpdateBarWidth();
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
