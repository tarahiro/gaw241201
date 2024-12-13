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
        [Inject] ILeetMasterGettable _leetMasterGettable;
        [Inject] ITimerStartableModel _timerStartable;
        [Inject] ITypingView _view;
        [Inject] TypingViewArgsFactory _argsFactory;
        [Inject] ITimerView _timerView;
        [Inject] IHaltable _haltable;
        [Inject] IPointableView _pointableView;
        [Inject] ILeetDataUserView _leetDataUserView;
        [Inject] TypingSentenceController _sentenceController;

        //----------point Œã‚Å•ª‚¯‚é‚©‚à------------------
        [Inject] PointModel _pointModel;
        [Inject] PointView _pointView;

        //----------Factory----------------------
        [Inject] LeetDataListFactory _factory;


        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            Log.Comment("TypingRogueLikePresenter‚ÉEntry");
            _enterable.Entered.Subscribe(x => _view.Enter(_argsFactory.Create(x)).Forget()).AddTo(_disposable);
            _timerStartable.TimerStarted.Subscribe(args  => _timerView.Enter(args).Forget()).AddTo(_disposable);
            _leetMasterGettable.LeetMasterGetted.Subscribe(x => _leetDataUserView.Initialize(_factory.Create(x))).AddTo(_disposable);
            _sentenceController.Ended.Subscribe(_ => _view.EndLoop()).AddTo(_disposable);

            _view.Exited.Subscribe(_ => {
                _timerView.HaltTimer();
                _model.EndSingle(); 
            }).AddTo(_disposable);

            _timerView.TimeUped.Subscribe(_ => _haltable.Halt()).AddTo(_disposable);
            _timerView.TimeRemained.Subscribe(time => _pointModel.AddRemainTimePoint(time)).AddTo(_disposable);
            _pointableView.Pointed.Subscribe(_ => _pointModel.AddUnitPoint()).AddTo(_disposable);

            _pointModel.PointUpdated.Subscribe(_pointView.UpdatePoint).AddTo(_disposable);
        }
    }
}