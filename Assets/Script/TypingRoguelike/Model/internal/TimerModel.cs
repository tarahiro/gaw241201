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
    public class TimerModel : ITimerModel
    {
        float _time;
        float _maxTime;
        bool _isCountTime = false;
        TimerArgs _args;
        CancellationTokenSource _cts;

        Subject<TimerArgs> _entered = new Subject<TimerArgs>();
        Subject<float> _updated = new Subject<float>();
        Subject<Unit> _timeUped = new Subject<Unit>();
        Subject<float> _timeRemained = new Subject<float>();

        public IObservable<TimerArgs> Entered => _entered;
        public IObservable<float> Updated => _updated;
        public IObservable<Unit> TimeUped => _timeUped;
        public IObservable<float> TimeRemained => _timeRemained;

        public async UniTask Enter(float maxTime)
        {
            Log.Comment("タイマー開始");

            Log.DebugAssert(!_isCountTime);

            //パラメータ初期化
            _isCountTime = true;
            _time = 0;
            _maxTime = maxTime;
            _cts = new CancellationTokenSource();

            _entered.OnNext(new TimerArgs(_time, _cts.Token));


            while (_time < _maxTime && _isCountTime)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                _time += Time.deltaTime;
                _updated.OnNext(_time / _maxTime);

            }

            OnExit();
        }

        void OnExit()
        {
            if (_time > _maxTime)
            {
                _timeUped.OnNext(Unit.Default);
            }
        }

        public void EndTimer()
        {
            _timeRemained.OnNext(_maxTime - _time);
            
            _isCountTime = false;
            _time = 0;
            _maxTime = 0;
        }
    }
}