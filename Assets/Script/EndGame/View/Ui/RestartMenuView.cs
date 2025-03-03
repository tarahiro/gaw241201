using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class RestartMenuView : MonoBehaviour, IMenuView
    {
        [SerializeField] List<IMenuItemView> _itemViewList;
        int _index = 0;
        [SerializeField] GameObject _cursor;

        Vector3 c_offset = 50f * Vector3.left;

        void Start()
        {
            _itemViewList = GetComponentsInChildren<IMenuItemView>(true).ToList();
        }

        public async UniTask Decide(int index)
        {

        }
        public async UniTask SetFocus(int index)
        {
            Current().UnFocus();

            _index = index;
            SetCursor();
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
        void SetCursor()
        {
            _cursor.transform.localPosition = Current().transform.localPosition + c_offset;
        }

        public int Index => _index;
        // public int Count => _itemViewList.Count;
        public int Count => _itemViewList.Count;
    }
}