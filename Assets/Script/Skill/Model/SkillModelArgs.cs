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
    //IAchievableSkill�Ȃǂ�ILeetMaster�ȂǂɌp�������āAArgs��r���Ă������Ǝv�������AKey�̏��������ʓ|�H
    public class SkillModelArgs
    {
        public FlagConst.ContainableMasterKey Key { get; private set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public SkillModelArgs(FlagConst.ContainableMasterKey key, string id, string name, string description)
        {
            Key = key;
            Id = id;
            Name = name;
            Description = description;
        }
    }
}