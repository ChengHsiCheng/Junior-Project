using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventoryObject : MonoBehaviour
{
    public SkillController skill;
    public List<SkillController> Container = new List<SkillController>();

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            if(!skill)
            {
                skill = Instantiate<SkillController>(Container[0]);
            }
            other.GetComponent<SkillManager>().skill_01 = skill;
        }
    }
}
