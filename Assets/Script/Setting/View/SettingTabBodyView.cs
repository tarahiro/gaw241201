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
    public class SettingTabBodyView : MonoBehaviour, IMenuView
    {
        [SerializeField] List<SettingItemView> _itemList;
        [SerializeField] Cursor _cursor;
        [SerializeField] GameObject _root;

        float cursorX = -480f;

        private void Awake()
        {
            Exit().Forget();
        }
        public async UniTask SetFocus(int itemIndex)
        {
            _cursor.transform.localPosition =
                new Vector2(cursorX, _itemList[itemIndex].transform.localPosition.y);
        }
        public async UniTask Decide(int itemIndex)
        {

            _cursor.transform.localPosition =
                new Vector2(cursorX +20f, _itemList[itemIndex].transform.localPosition.y);

        }

        public async UniTask Enter(int menuIndex)
        {
            _root.SetActive(true);
            await SetFocus(menuIndex);
        }

        public async UniTask Exit()
        {
            _root.SetActive(false);
        }

        public void OnInputBlockEnabled(bool b)
        {
            _cursor.gameObject.SetActive(!b);
        }
    }
}