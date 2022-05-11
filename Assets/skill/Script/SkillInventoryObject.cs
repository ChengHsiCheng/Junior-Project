using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventoryObject : MonoBehaviour
{
    public List<SkillController> Container = new List<SkillController>();

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            other.GetComponent<SkillManager>().skill_01 = Container[0];
        }
    }
}
