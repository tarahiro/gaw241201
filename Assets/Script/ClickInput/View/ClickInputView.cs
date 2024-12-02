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
    public class ClickInputView : MonoBehaviour
    {
        [Inject] IGazable _gazable;

        ClickInputItemView _currentItem;

        const string c_prefabPath = "Prefab/ClickInput/ClickInputItemView";

        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;

        public async UniTask Enter(CancellationToken ct)
        {
            Log.DebugLog("ClickInputViewäJén");
            _currentItem = Instantiate(ResourceUtil.GetResource<ClickInputItemView>(c_prefabPath), transform);
            _currentItem.Construct(_gazable);
            ct.Register(OnExit);

            await _currentItem.Enter(ct);

            OnExit();
        }

        private void OnExit()
        {

            Log.Comment("ClickInputViewèIóπ");
            _exited.OnNext(Unit.Default);   

        }
    }
}