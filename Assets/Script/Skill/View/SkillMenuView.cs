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
        [Inject] IObjectResolver _diContainer;
        [SerializeField] SkillItemView _itemViewPrefab;
        [SerializeField] Transform _root;

        List<SkillItemView> _itemViewList;
        const float c_intervalX = 540f;

        int _index = 0;

        float[] _fakeInitialRotation = new float[]
        {
            -4f,6f,3f
        };

        public async UniTask Enter(SkillArgs args)
        {
            _itemViewList = new List<SkillItemView>();
            _index = args.MenuItemIndex;

            for (int i = 0; i < args.DataList.Count; i++)
            {
                SkillItemView item = _diContainer.Instantiate<SkillItemView>(_itemViewPrefab, _root);
                item.SetData(args.DataList[i]);
                item.transform.localPosition = Vector2.right * (-1f + i) * c_intervalX;
                item.transform.localRotation = Quaternion.Euler(0, 0, _fakeInitialRotation[i]);

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