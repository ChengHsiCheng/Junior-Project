using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillController : MonoBehaviour
{
    public float CD = 0;
    public float skillElapsedTime = 0;
    public string SkillName;
    public string skillIntroduce;
    public Image image;
    // public delegate void SkillCDEventArgs(SkillController sender);
    // public SkillCDEventArgs OnStartCountCD;

    public abstract void SkillStart();

    public virtual void SkillUpdate(){}

}
