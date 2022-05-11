using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public SkillController skill_01;
    float skill_01_ElapsedTime;
    public SkillController skill_02;
    public SkillController skill_03;
    public SkillController skill_04;

    void Start()
    {
    }

    void Update()
    {
        if(skill_01)
        {
            skill_01.OnStartCountCD += OnStartCountCD;
            if(Input.GetMouseButtonDown(1) && skill_01_ElapsedTime >= skill_01.CD)
            {
                skill_01.Use();
            }
            skill_01.SkillUpdate();
            skill_01_ElapsedTime += Time.deltaTime;
        }
    }

    void OnStartCountCD(object sender) 
    {
        if((object)sender == skill_01)
        {
            skill_01_ElapsedTime = 0;
        }
    }

    public void SetSKill_01(SkillController skill)
    {
        skill_01 = skill;
    }
}
