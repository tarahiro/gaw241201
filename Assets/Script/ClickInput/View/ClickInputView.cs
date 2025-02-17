using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using System.Threading;

namespace gaw241201.View
{
    public class ClickInputView : MonoBehaviour, IUiViewDeletable
    {
        [Inject] IGazable _gazable;
        [Inject] IObjectResolver _diContainer;

        [SerializeField] ClickInputItemView _itemPrefab;

        List<ClickInputItemView> _itemList;

        const float c_initialX = -280f;
        const float c_intervalX = 560f;

        Subject<int> _exited = new Subject<int>();
        public IObservable<int> Exited => _exited;

        bool isExit = false;

        public async UniTask Enter(ClickInputArgs args)
        {
            Log.DebugLog("ClickInputView開始");
            _itemList = new List<ClickInputItemView>();
            isExit = false;
            args.CancellationToken.Register(() => OnExit(0));

            for (int i = 0; i < args.LabelList.Count; i++)
            {
                var item =  _diContainer.Instantiate<ClickInputItemView> (_itemPrefab, transform);
                item.Initialize(i, args.LabelList[i]);
                item.transform.localPosition = Vector2.right * (c_initialX + c_intervalX * i);
                item.OnClicked.Subscribe(OnExit);
                _itemList.Add(item);
            }

            await UniTask.WaitUntil(() => isExit, cancellationToken: args.CancellationToken);
        }

        private void OnExit(int i)
        {
            Log.Comment("ClickInputView終了");
            _exited.OnNext(i);
            foreach(var item in _itemList)
            {
                item.Exit();
            }
            isExit = true;

        }

        public void Delete()
        {
            Log.Comment("FreeInputViewItemを削除");
            //色々disposeしないといけない気もする
            for(int i = 0;i < _itemList.Count; i++)
            {
                Destroy(_itemList[i].gameObject);
            }
        }
    }
}