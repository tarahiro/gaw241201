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

namespace gaw241201
{
    public class FreeInputModel : IFlowModel
    {
        [Inject] FreeInputValueRegisterer _valueRegisterer;
        [Inject] FreeInputArgsFactory _flowArgsFactory;

        bool _isEnded = false;
        string _bodyId;
        CancellationTokenSource _cts;
        Subject<FreeInputArgs> _entered  = new Subject<FreeInputArgs>();

        public IObservable<FreeInputArgs> Entered => _entered;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "のFreeInput開始");
            _cts = new CancellationTokenSource();
            _bodyId = bodyId;
            _isEnded = false;

            _entered.OnNext(_flowArgsFactory.Create(bodyId,_cts.Token));

            await UniTask.WaitUntil(() =>  _isEnded);
        }

        public void EndFlow(string value)
        {
            Log.Comment("FreeInputの終了を検知。value = " + value);
            _valueRegisterer.Register(EnumUtil.KeyToType<FreeInputConst.RegisterProcessKey> (_bodyId), value);
            _isEnded = true;
        }

#if ENABLE_DEBUG
        public void ForceEndFlow()
        {
            _cts.Cancel();
        }

#endif
    }
}