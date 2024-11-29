using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
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

        bool _isEnded = false;
        string _bodyId;
        Subject<string> _entered  = new Subject<string>();
        public IObservable<string> Entered => _entered;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "のFreeInput開始");
            _bodyId = bodyId;
            _isEnded = false;

            _entered.OnNext(bodyId);

            await UniTask.WaitUntil(() =>  _isEnded);
        }

        public void EndFlow(string value)
        {
            Log.Comment("FreeInputの終了を検知。value = " + value);
            _valueRegisterer.Register(EnumUtil.KeyToType<FreeInputConst.RegisterProcessKey> (_bodyId), value);
            _isEnded = true;
        }

#if ENABLE_DEBUG
        Subject<Unit> _forceEnded = new Subject<Unit>();
        public IObservable<Unit> ForceEnded => _forceEnded;
        public string ForceGetCategory => "Conversation";
        public void ForceEndFlow()
        {
            _forceEnded.OnNext(Unit.Default);
        }

#endif
    }
}