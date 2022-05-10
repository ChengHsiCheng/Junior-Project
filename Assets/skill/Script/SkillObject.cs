using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "New Skill",menuName = "new Skill")]
public class SkillObject : ScriptableObject
{
    public GameObject Prefab;
    public Sprite SkillSprite;
    public float CD;
 
    [TextArea]
    public string description;

}
