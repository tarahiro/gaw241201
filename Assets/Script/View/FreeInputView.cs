using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class FreeInputView : MonoBehaviour, IUiViewDeletable
    {
        [Inject] IGazable _gazable;

        FreeInputItemView _currentItem;

        const string c_prefix = "Prefab/FreeInput/";

        Subject<string> _exited = new Subject<string>();
        public IObservable<string> Exited => _exited;
        

        public async UniTask Enter(string bodyId, CancellationToken ct)
        {
            Log.DebugLog(c_prefix + bodyId + "ÇÃprefabê∂ê¨");
            _currentItem = Instantiate(Resources.Load<FreeInputItemView>(c_prefix + bodyId), transform);
            _currentItem.Construct(_gazable);

            string value = "UnRegistered";
            _currentItem.Exited.Subscribe(x => value = x);
            await _currentItem.Enter(ct);

            _exited.OnNext(value);

        }

        public void Delete()
        {
            Log.Comment("FreeInputViewItemÇçÌèú");
            //êFÅXdisposeÇµÇ»Ç¢Ç∆Ç¢ÇØÇ»Ç¢ãCÇ‡Ç∑ÇÈ
            Destroy(_currentItem.gameObject);
        }
    }
}