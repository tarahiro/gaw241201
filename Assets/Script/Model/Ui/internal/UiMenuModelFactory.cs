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
    public class UiMenuModelFactory
    {
        public IUiMenuModel Create(IMenuItemProvider provider)
        {
            List<IUiMenuItemModel> list = new List<IUiMenuItemModel>();

            for (int i = 0; i < provider.Count; i++)
            {
                list.Add(provider.Provide(i));
            }

            return new UiMenuModel(list);

        }
    }
}