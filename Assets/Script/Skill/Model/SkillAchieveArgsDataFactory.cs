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
            SkillConst.SkillCategory category;
            switch (wordMaster.TagName)
            {
                case "animal":
                    category = SkillConst.SkillCategory.Animal;
                    break;

                case "human":
                    category = SkillConst.SkillCategory.Human;
                    break;

                case "vi":
                    category = SkillConst.SkillCategory.Vi;
                    break;

                case "vt":
                    category = SkillConst.SkillCategory.Vt;
                    break;

                default:
                    Log.DebugLog("tagNameÇ™ïsê≥Ç≈Ç∑:" + wordMaster.TagName);
                    category = SkillConst.SkillCategory.Vt;
                    break;
            }


            return new SkillArgs.Data(FlagConst.ContainableMasterKey.Word, wordMaster.Id, category, wordMaster.WordName, wordMaster.Description);
        }

    }
}