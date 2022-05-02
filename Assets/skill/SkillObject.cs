using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : ScriptableObject
{
    public GameObject Prefab;
    public Sprite SkillSprite;
    
    [TextArea]
    public string description;
}
