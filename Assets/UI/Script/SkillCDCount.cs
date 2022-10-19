using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCDCount : MonoBehaviour
{
    public Image skillCDBar;
    public Image skillImage;
    public SkillManager manager;
    float skillCD;
    float skillTimer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(manager && manager.skill)
        {
            skillTimer = manager.skillList[manager.level].skillTimer;
            skillCD = manager.skillList[manager.level].skillCD;
            skillCDBar.fillAmount = skillTimer / skillCD;
        }else
        {
            skillCDBar.fillAmount = 0;
        }
        
    }

    public void ChangeSkillImage(Sprite _skillImage, SkillManager _manager)
    {
        manager = _manager;
        skillImage.sprite = _skillImage;
    }
}
