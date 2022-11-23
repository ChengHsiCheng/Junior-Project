using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillLevelUpUI : MonoBehaviour
{
    GameObject canvas;
    SkillManager skillManager;

    public Text result01;
    public Text result02;

    private void Start()
    {
        skillManager = GameObject.Find("Player").GetComponent<SkillManager>();
        Imagetocanvas();

        Debug.Log(skillManager.skill.SkillName);
        if (skillManager.skill.SkillName == "FireBall")
        {
            result01.text = "增加傷害";
            result02.text = "追加火球(數量+2)";
        }
        else if (skillManager.skill.SkillName == "IceSpike")
        {
            result01.text = "增加傷害";
            result02.text = "增加範圍";
        }
    }

    public void OnSkillLevelUp(int i)
    {
        skillManager.SkillLvUp(i);
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    void Imagetocanvas() //把image放到canvas上
    {
        canvas = GameObject.Find("PlayerUI");
        transform.SetParent(canvas.transform);
    }
}
