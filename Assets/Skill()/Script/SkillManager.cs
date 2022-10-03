using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public SkillController skill;
    //public SkillController skill_01;
    // public SkillController skill_02;
    // public SkillController skill_03;
    // public SkillController skill_04;

    void Start()
    {
    }

    void Update()
    {
        // if(skill_01)
        // {
        //     // skill_01.OnStartCountCD += OnStartCountCD;
        //     if(Input.GetMouseButtonDown(1))
        //     {
        //         skill_01.SkillStart();
        //     }
        //     skill_01.SkillUpdate();
        // }
        if(skill)
        {
            if(Input.GetMouseButtonDown(1))
            {
                skill.SkillStart();
            }

            skill.SkillUpdate();
        }
    }

    // void OnStartCountCD(SkillController sender) 
    // {
    //     if(sender == skill_01)
    //     {
    //     }
    // }

    // public void SetSKill_01(SkillController skill)
    // {
    //     skill_01 = skill;
    // }
}
