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
        [Inject] ClickInputProccessorProvider _processorProvider;

        //Enterする
        Subject<ClickInputArgs> _entered = new Subject<ClickInputArgs>();
        public IObservable<ClickInputArgs> Entered => _entered;

        CancellationTokenSource _ct;

        bool _isEnded = false;
        IClickInputProcessor _currentProcessor;

        // 何らかのボタンをクリックしたら処理を戻す
        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment("ClickInputModel開始");
            _isEnded = false;
            _ct = new CancellationTokenSource();

            _currentProcessor = _processorProvider.Create(EnumUtil.KeyToType<ClickInputConst.Key>(bodyId));

            _entered.OnNext(_currentProcessor.CreateArgs(_ct.Token));

            await UniTask.WaitUntil(() => _isEnded);

            Log.Comment("ClickInputModel終了");
        }

        public void End(int _buttonIndex)
        {
            Log.Comment("Modelで終了検知");
            _currentProcessor.Process(_buttonIndex);
            _isEnded = true;
        }
        public void ForceEndFlow()
        {
            _ct.Cancel();
        }
    }
}