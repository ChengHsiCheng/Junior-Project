using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "New SkillInventoyry",menuName = "Invetory System/SkillInventory")]
public class SkillInventoryObject : ScriptableObject
{
    public List<SkillObject> Container = new List<SkillObject>();
}
