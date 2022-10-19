using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public SkillControl skill;
    public SkillControl[] skillList;

    public int level;

    void Start()
    {
        skill = null;
        skillList = null;
    }

    void Update()
    {
        if(skill)
        {
            if(Input.GetMouseButtonDown(1))
            {
                skill.SkillStart();
            }

            skill.SkillUpdate();
        }
    }

    public void ChangeSkill(SkillControl[] _skillList)
    {
        skillList = _skillList;
        level = 0;
        skill = skillList[level];
    }

    public void SkillLvUp()
    {
        if(skill.SkillName == "FireBall")
        {
            level += 1;
            skill = skillList[level];
        }
    }
}
