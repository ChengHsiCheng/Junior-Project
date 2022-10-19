using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeSkillLv01 : SkillControl
{
    Vector3 pos;
    public override void SkillStart()
    {
        if(skillPos == null)
        {
            skillPos = GameObject.Find("SkillPos");
        }
        pos = skillPos.transform.position + skillPos.transform.forward * 0.5f - skillPos.transform.up * 0.5f;

        if(skillTimer <= 0)
        {
            Instantiate(skill, pos, skillPos.transform.rotation * skill.transform.rotation);
            skillTimer = skillCD;
        }
    }

    public override void SkillUpdate()
    {
        if(skillTimer > 0)
        {
            skillTimer -= Time.deltaTime;
        }
    }
}
