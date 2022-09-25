using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventoryObject : MonoBehaviour
{
    public SkillController skill;
    public List<SkillController> Container = new List<SkillController>();
    public Text skillName;
    public Text skillIntroduce;
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            skillName.gameObject.SetActive(true);
            skillIntroduce.text = Container[0].skillIntroduce;
            skillName.text = Container[0].SkillName;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(Input.GetKey(KeyCode.E))
        {
            Debug.Log("A");
            if(!skill)
            {
                skill = Instantiate<SkillController>(Container[0]);
                Debug.Log(skill);
            }
            other.GetComponent<SkillManager>().skill_01 = skill;

            skillName.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            skillName.gameObject.SetActive(false);
        }
    }
}
