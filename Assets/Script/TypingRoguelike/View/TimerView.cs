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

        Subject<Unit> _timeUped = new Subject<Unit>();
        public IObservable<Unit> TimeUped => _timeUped;

        public async UniTask Enter(TimerArgs timerArgs)
        {
            Log.Comment("タイマー開始");
            OnEnter(timerArgs);

            while (_time < timerArgs.Time)
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
            SetImage(_time, timerArgs.Time);
        }

        void OnExit()
        {
            _timeUped.OnNext(Unit.Default);
        }

        void SetImage(float time, float maxTime)
        {
            _timerImage.fillAmount = time / maxTime;
        }

    }
}