using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    //IAchievableSkill�Ȃǂ�ILeetMaster�ȂǂɌp�������āAArgs��r���Ă������Ǝv�������AKey�̏��������ʓ|�H
    public class SkillArgs
    {
        public CancellationToken CancellationToken { get; private set; }

        public List<Data> DataList { get; private set; }

        public SkillArgs(CancellationToken cancellationToken, List<Data> dataList)
        {
            CancellationToken = cancellationToken;
            DataList = dataList;
        }



        public class Data
        {
            public FlagConst.ContainableMasterKey Key { get; private set; }
            public string Id { get; private set; }
            public SkillConst.SkillCategory Category { get; private set; }
            public string Name { get; private set; }
            public string Description { get; private set; }
            public Data(FlagConst.ContainableMasterKey key, string id, SkillConst.SkillCategory category, string name, string description)
            {
                Key = key;
                Id = id;
                Category = category;
                Name = name;
                Description = description;
            }
        }
    }

}