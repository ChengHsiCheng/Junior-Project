using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeSkill02 : SkillControl
{
    Vector3 pos;
    public float sizeX;
    public float sizeY;
    public float sizeZ;
    public override void SkillStart()
    {
        if (skillPos == null)
        {
            skillPos = GameObject.Find("SkillPos");
        }
        pos = skillPos.transform.position + skillPos.transform.forward * 0.5f - skillPos.transform.up * 0.5f;

        if (skillTimer <= 0)
        {
            IceSpike skillobj = Instantiate(skill, pos, skillPos.transform.rotation * skill.transform.rotation * Quaternion.Euler(0, 15, 0)).GetComponent<IceSpike>();
            skillobj.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
            skillobj.damege = damege;
            skillobj = Instantiate(skill, pos, skillPos.transform.rotation * skill.transform.rotation * Quaternion.Euler(0, -15, 0)).GetComponent<IceSpike>();
            skillobj.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
            skillobj.damege = damege;
            skillTimer = skillCD;
        }
    }

    public override void SkillUpdate()
    {
        if (skillTimer > 0)
        {
            skillTimer -= Time.deltaTime;
        }
    }
}
