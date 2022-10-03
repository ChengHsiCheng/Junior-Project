using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    public GameObject skillPos;
    public float skillCD = 0;
    public float skillTimer;
    public float damege;
    public GameObject skill;
    public string SkillName;
    public string skillIntroduce;
    public Image image;
    // public delegate void SkillCDEventArgs(SkillController sender);
    // public SkillCDEventArgs OnStartCountCD;

    void Start()
    {
        skillPos = GameObject.Find("SkillPos");

        skillTimer = skillCD;
    }

    public virtual void SkillStart(){}

    public virtual void SkillUpdate(){}

}
