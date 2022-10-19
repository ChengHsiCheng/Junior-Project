using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkillTrigger : MonoBehaviour
{
    public SkillControl[] skillList;
    public Text skillName;
    public Text skillIntroduce;
    public SkillCDCount skillCDBar;
    bool inTrigger;
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player" || other.transform.parent.tag == "Player")
        {
            skillName.gameObject.SetActive(true);
            skillIntroduce.text = skillList[0].skillIntroduce;
            skillName.text = skillList[0].SkillName;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                SkillManager skillManager = other.GetComponent<SkillManager>();
                skillManager.ChangeSkill(skillList);
                skillCDBar.ChangeSkillImage(skillList[0].image, skillManager);

                skillName.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player" || other.transform.parent.tag == "Player")
        {
            skillName.gameObject.SetActive(false);
            skillIntroduce.gameObject.SetActive(false);
        }
    }
}
