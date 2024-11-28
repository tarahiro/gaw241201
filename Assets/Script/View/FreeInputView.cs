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
    public class FreeInputView : MonoBehaviour
    {
        FreeInputItemView _item;

        const string c_prefix = "Prefab/FreeInput/";

        Subject<string> _exited = new Subject<string>();
        public IObservable<string> Exited => _exited;
        

        public async UniTask Enter(string bodyId, CancellationToken ct)
        {
            Log.DebugLog(c_prefix + bodyId + "ÇÃprefabê∂ê¨");
            _item = Instantiate(Resources.Load<FreeInputItemView>(c_prefix + bodyId), transform);
            string value = "UnRegistered";
            _item.Exited.Subscribe(x => value = x);
            await _item.Enter(ct);

            _exited.OnNext(value);

        }
    }
}