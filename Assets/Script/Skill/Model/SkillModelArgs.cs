using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    //IAchievableSkill‚È‚Ç‚ğILeetMaster‚È‚Ç‚ÉŒp³‚³‚¹‚ÄAArgs‚ğ”r‚µ‚Ä‚à‚¢‚¢‚Æv‚Á‚½‚ªAKey‚Ìˆ—‚ª‚â‚â–Ê“|H
    public class SkillModelArgs
    {
        public FlagConst.ContainableMasterKey Key { get; private set; }
        public string Id { get; private set; }
        public SkillConst.SkillCategory Category { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public SkillModelArgs(FlagConst.ContainableMasterKey key, string id, SkillConst.SkillCategory category,  string name, string description)
        {
            Key = key;
            Id = id;
            Category = category;
            Name = name;
            Description = description;
        }
    }
}