using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class UiMenuViewTemplate : MonoBehaviour, IMenuView
    {

        List<IMenuItemView> _itemViewList = new List<IMenuItemView>();
        int _index = 0;
        public async UniTask Decide(int index)
        {

        }
        public async UniTask SetFocus(int index)
        {
            Log.Comment("SkillMenuView: SetFocus");

            Current().UnFocus();
            _index = index;
            Current().Focus();
        }

        public async UniTask Enter(int index)
        {
            await SetFocus(index);
        }

        public async UniTask Exit()
        {
        }
        public IMenuItemView Current()
        {
            return _itemViewList[_index];
        }
    }
}