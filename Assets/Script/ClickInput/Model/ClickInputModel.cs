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

namespace gaw241201
{
    public class ClickInputModel : IFlowModel
    {
        //Enterする
        Subject<CancellationToken> _entered = new Subject<CancellationToken>();
        public IObservable<CancellationToken> Entered => _entered;

        CancellationTokenSource _ct;

        bool _isEnded = false;

        // 何らかのボタンをクリックしたら処理を戻す
        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment("ClickInputModel開始");
            _isEnded = false;
            _ct = new CancellationTokenSource();

            _entered.OnNext(_ct.Token);

            await UniTask.WaitUntil(() => _isEnded);

            Log.Comment("ClickInputModel終了");
        }

        public void End()
        {
            Log.Comment("Modelで終了検知");
            _isEnded = true;
        }
#if ENABLE_DEBUG
        public void ForceEndFlow()
        {
            _ct.Cancel();
        }
        public string ForceGetCategory => "ClickInput";
#endif
    }
}