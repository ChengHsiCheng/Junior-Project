using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillController : MonoBehaviour
{
    public float CD = 0;
    public delegate void SkillCDEventArgs(object sender);
    public SkillCDEventArgs OnStartCountCD;

    public abstract void Use();

    public virtual void SkillUpdate(){}

}
