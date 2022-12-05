using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fungus;

public class SkillManager : MonoBehaviour
{
    public SkillControl skill;
    public SkillControl[] skillList;

    public int count;
    float result01;
    float result02;

    public Flowchart startFlowchart;
    string fungusBoolName = "isStart";
    public bool fungusBool
    {
        get
        {
            return startFlowchart.GetBooleanVariable(fungusBoolName);
        }
        set
        {
            startFlowchart.SetBooleanVariable(fungusBoolName, value);
        }
    }

    void Start()
    {
        skill = null;
        skillList = null;
    }

    void Update()
    {
        if (skill && fungusBool)
        {
            if (Input.GetMouseButtonDown(1))
            {
                skill.SkillStart();
            }

            skill.SkillUpdate();
        }
    }

    public void ChangeSkill(SkillControl[] _skillList)
    {
        skillList = _skillList;
        skill = skillList[0];
    }

    public void SkillLvUp(int i)
    {
        if (i == 0)
        {
            result01 += 1;
        }
        else if (i == 1)
        {
            result02 += 1;
        }
        else if (i == 3)
        {
            count = 0;
        }

        if (result01 == 1 && result02 == 0)
        {
            count = 1;
        }
        else if (result01 == 2 && result02 == 0)
        {
            count = 2;
        }
        else if (result01 == 3 && result02 == 0)
        {
            count = 3;
        }
        else if (result01 == 0 && result02 == 1)
        {
            count = 4;
        }
        else if (result01 == 0 && result02 == 2)
        {
            count = 5;
        }
        else if (result01 == 0 && result02 == 3)
        {
            count = 6;
        }
        else if (result01 == 1 && result02 == 1)
        {
            count = 7;
        }
        else if (result01 == 2 && result02 == 1)
        {
            count = 8;
        }
        else if (result01 == 1 && result02 == 2)
        {
            count = 9;
        }

        skill = skillList[count];

    }
}
