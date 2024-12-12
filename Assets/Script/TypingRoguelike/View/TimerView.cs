using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class TimerView : MonoBehaviour, ITimerView
    {
        [SerializeField] Image _timerImage;
        float _time;
        bool _isCountTime = true;
        TimerArgs _args;

        Subject<Unit> _timeUped = new Subject<Unit>();
        Subject<float> _timeRemained = new Subject<float>();
        public IObservable<Unit> TimeUped => _timeUped;
        public IObservable<float> TimeRemained => _timeRemained;

        public async UniTask Enter(TimerArgs timerArgs)
        {
            Log.Comment("タイマー開始");
            OnEnter(timerArgs);

            while (_time < timerArgs.Time && _isCountTime)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                _time += Time.deltaTime;
                SetImage(_time, timerArgs.Time);
            }

            OnExit();
        }

        void OnEnter(TimerArgs timerArgs)
        {
            //初期化
            _time = 0;
            _args = timerArgs;
            _isCountTime = true;
            SetImage(_time, timerArgs.Time);
        }

        void OnExit()
        {
            if (_time > _args.Time)
            {
                _timeUped.OnNext(Unit.Default);
            }
        }

        void SetImage(float time, float maxTime)
        {
            _timerImage.fillAmount = time / maxTime;
        }

        public void HaltTimer()
        {
            _isCountTime = false;
            _timeRemained.OnNext(_args.Time - _time);
        }

    }
}