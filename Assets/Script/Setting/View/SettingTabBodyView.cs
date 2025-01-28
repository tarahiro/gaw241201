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
    public class SettingTabBodyView : MonoBehaviour
    {
        [SerializeField] List<SettingItemView> _itemList;
        [SerializeField] Cursor _cursor;
        [SerializeField] GameObject _root;

        float cursorX = -320f;

        private void Start()
        {
            Exit().Forget();
        }
        public async UniTask SetFocus(int itemIndex)
        {
            _cursor.transform.localPosition =
                new Vector2(cursorX, _itemList[itemIndex].transform.localPosition.y);
        }

        public async UniTask Enter()
        {
            _root.SetActive(true);
        }

        public async UniTask Exit()
        {
            _root.SetActive(false);
        }
    }
}