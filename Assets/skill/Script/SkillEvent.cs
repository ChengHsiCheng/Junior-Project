using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEvent : MonoBehaviour
{
    public GameObject Skill_01;
    public GameObject Skill_02;
    public SkillInventoryObject skillInventoryObject;
    public SkillController skillController;
    public float Skill_01_ElapsedTime = 0;
    public float Skill_02_ElapsedTime = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Skill_01 = skillInventoryObject.Container[0].Prefab;
        Skill_02 = skillInventoryObject.Container[1].Prefab;
        
        UseSkills();

        Skill_01_ElapsedTime += Time.deltaTime;
        Skill_02_ElapsedTime += Time.deltaTime;
    }

    void UseSkills()
    {
        if(Skill_01_ElapsedTime >= skillInventoryObject.Container[0].CD)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Instantiate(Skill_01,transform.position,Skill_01.transform.rotation);
                Skill_01_ElapsedTime = 0;
            }
        }
        if(Skill_02_ElapsedTime >= skillInventoryObject.Container[1].CD)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Instantiate(Skill_02,transform.position,Skill_02.transform.rotation);
                Skill_02_ElapsedTime = 0;
            }
        }
    }

}
