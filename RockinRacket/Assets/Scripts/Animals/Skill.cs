using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
    This script is for animals and items to help work with code involving skills and for what they affect
    
*/
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
