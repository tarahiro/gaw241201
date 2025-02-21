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
    public class SelectInputModelFake : ICategoryEnterableModel
    {

        //Enterする
        Subject<CancellationToken> _entered = new Subject<CancellationToken> ();
        public IObservable<CancellationToken> Entered => _entered;

        Subject<Unit> _exited = new Subject<Unit> ();
        public IObservable<Unit> Exited => _exited;

        Subject<int> _focused = new Subject<int> ();
        public IObservable<int> Focused => _focused;

        [Inject] ICancellationTokenPure _ct;

        bool _isEnded = false;
        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment("ClickInputModel開始");
            _isEnded = false;
            _ct.SetNew();

            _entered.OnNext(_ct.Token);

            await UniTask.WaitUntil(() => _isEnded);

            Log.Comment("ClickInputModel終了");
        }
        public void ForceEndFlow()
        {
            _ct.Cancel();
        }

        public void Exit()
        {
            _isEnded = true;
            _exited.OnNext(Unit.Default);
        }

        int _index = 0;
        const int _fakeMaxIndex = 2;

        //本来は分けるべき
        public void Focus(int inputIndex)
        {
            _index += inputIndex;
            if (_index < 0) _index = _fakeMaxIndex - 1;
            if (_index > _fakeMaxIndex - 1) _index = 0;
            _focused.OnNext(_index);
        }



    }
}