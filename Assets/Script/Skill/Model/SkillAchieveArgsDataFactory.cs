using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class SkillAchieveArgsDataFactory
    {
        public SkillArgs.Data Create(ILeetMaster leetMaster)
        {
            return new SkillArgs.Data(FlagConst.ContainableMasterKey.Leet, leetMaster.Id, SkillConst.SkillCategory.Leet, leetMaster.Name, leetMaster.Description);
        }

        public SkillArgs.Data Create(IWordMaster wordMaster)
        {
            return new SkillArgs.Data(FlagConst.ContainableMasterKey.Word, wordMaster.Id, SkillConst.SkillCategory.Leet, wordMaster.WordName, wordMaster.Description);
        }

    }
}