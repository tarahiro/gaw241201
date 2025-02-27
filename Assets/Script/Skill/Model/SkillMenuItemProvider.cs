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
    public class SkillMenuItemProvider : ISkillMenuItemProvider
    {
        List<SkillMenuItemModel> _itemModelList;

        public int Count => 3;

        [Inject] public SkillMenuItemProvider()
        {
            _itemModelList = new List<SkillMenuItemModel>();

            for(int i = 0; i < Count; i++)
            {
                _itemModelList.Add(new SkillMenuItemModel());
            }
        }
        public IUiMenuItemModel Provide(int index)
        {
            return _itemModelList[index];
        }

        public SkillMenuItemModel ProvideRaw(int index)
        {
            return _itemModelList[index];
        }

    }
}