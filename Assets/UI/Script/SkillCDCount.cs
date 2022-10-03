using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCDCount : MonoBehaviour
{
    public Text skillText_01;
    public float skillElapsedTime;
    public SkillManager skillManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(skillManager.skill_01)
        // {
        //     skillElapsedTime = (Mathf.Floor((skillManager.skill_01.skillElapsedTime)*1000)/1000);
        //     if(skillElapsedTime > 0)
        //     {
        //         skillText_01.gameObject.SetActive(true);
        //         skillText_01.text = skillElapsedTime.ToString();
        //     }else
        //     {
        //         skillText_01.gameObject.SetActive(false);
        //     }
        // }
        
    }
}
