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
    //IAchievableSkillなどをILeetMasterなどに継承させて、Argsを排してもいいと思ったが、Keyの処理がやや面倒？
    public class SkillArgs
    {
        public CancellationToken CancellationToken { get; private set; }

        public int MenuItemIndex { get; private set; }
        public List<Data> DataList { get; private set; }

        public SkillArgs(CancellationToken cancellationToken,int menuItemIndex, List<Data> dataList)
        {
            CancellationToken = cancellationToken;
            MenuItemIndex = menuItemIndex;
            DataList = dataList;
        }



        public class Data
        {
            public FlagConst.ContainableMasterKey Key { get; private set; }
            public string Id { get; private set; }
            public string Category { get; private set; }
            public string Name { get; private set; }
            public string Description { get; private set; }
            public Data(FlagConst.ContainableMasterKey key, string id, string category, string name, string description)
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