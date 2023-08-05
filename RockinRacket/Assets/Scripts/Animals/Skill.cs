using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Skill 
{
    public Skill(Attribute attribute, int level)
    {
        this.SkillName = attribute;
        this.Level = level;
    }
    public Attribute SkillName;
    public int Level;
}
