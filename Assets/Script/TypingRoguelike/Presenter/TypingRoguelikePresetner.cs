using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using gaw241201.View;
using gaw241201.Model;


namespace gaw241201.Presenter
{
    public class TypingRoguelikePresetner : IPostInitializable
    {
        [Inject] TypingRoguelikeModel _model;
        [Inject] ISingleTextSequenceEnterable<ITypingMaster> _enterable;
        [Inject] ITimerStartableModel _timerStartable;
        [Inject] ITypingView _view;
        [Inject] TypingViewArgsFactory _argsFactory;
        [Inject] ITimerView _timerView;
        [Inject] IHaltable _haltable;
        [Inject] EnterKeyHundler _enterKeyHundler;
        [Inject] IQuestionInitializer _questionInitializer;
        [Inject] TypingTextView _textView;

        //----------point Œã‚Å•ª‚¯‚é‚©‚à------------------
        [Inject] IPointable _pointModel;
        [Inject] PointView _pointView;



        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            Log.Comment("TypingRogueLikePresenter‚ÉEntry");
            _enterable.Entered.Subscribe(x => _view.Enter(_argsFactory.Create(x)).Forget()).AddTo(_disposable);
            _timerStartable.TimerStarted.Subscribe(args  => _timerView.Enter(args).Forget()).AddTo(_disposable);
            _view.KeyEntered.Subscribe(x => _enterKeyHundler.EnterKey(x)).AddTo(_disposable);
            _enterKeyHundler.Ended.Subscribe(_ => _view.EndLoop()).AddTo(_disposable);
            _questionInitializer.SampleInputted.Subscribe(_textView.SetSampleText);

            _view.Exited.Subscribe(_ => {
                _timerView.HaltTimer();
                _model.EndSingle(); 
            }).AddTo(_disposable);

            _timerView.TimeUped.Subscribe(_ => _haltable.Halt()).AddTo(_disposable);
            _timerView.TimeRemained.Subscribe(time => _pointModel.AddRemainTimePoint(time)).AddTo(_disposable);
            
            _pointModel.PointUpdated.Subscribe(_pointView.UpdatePoint).AddTo(_disposable);
        }
    }
}