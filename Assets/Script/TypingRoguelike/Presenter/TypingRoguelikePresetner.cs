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
        [Inject] ITypingView _view;
        [Inject] TypingViewArgsFactory _argsFactory;
        [Inject] ITimerView _timerView;
        [Inject] IHaltable _haltable;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            Log.Comment("TypingRogueLikePresenter‚ÉEntry");
            _enterable.Entered.Subscribe(x => _view.Enter(_argsFactory.Create(x)).Forget()).AddTo(_disposable);
            _view.Exited.Subscribe(_ => _model.EndSingle()).AddTo(_disposable);
            _timerView.TimeUped.Subscribe(_ => _haltable.Halt()).AddTo(_disposable);
        }
    }
}