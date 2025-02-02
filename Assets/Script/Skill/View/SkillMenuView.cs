using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class SkillMenuView : MonoBehaviour
    {
        [SerializeField] SkillItemView _itemViewPrefab;
        [SerializeField] Transform _root;

        List<SkillItemView> _itemViewList;
        const float c_intervalX = 480f;

        int _index = 0;

        public async UniTask Enter(SkillArgs args)
        {
            _itemViewList = new List<SkillItemView>();
            _index = args.MenuItemIndex;

            for (int i = 0; i < args.DataList.Count; i++)
            {
                SkillItemView item = Instantiate(_itemViewPrefab, _root);
                item.SetData(args.DataList[i]);
                item.transform.localPosition = Vector2.right * (-1f + i) * c_intervalX;

                _itemViewList.Add(item);
            }
        }

        public async UniTask Exit()
        {
            for (int i = 0; i < _itemViewList.Count; i++)
            {
                Destroy(_itemViewList[i].gameObject);
            }
        }

        public IMenuItemView Current()
        {
            return _itemViewList[_index];
        }

        public void ChangeFocus(int index)
        {
            Current().UnFocus();
            _index = index;
            Current().Focus();
        }
    }
}