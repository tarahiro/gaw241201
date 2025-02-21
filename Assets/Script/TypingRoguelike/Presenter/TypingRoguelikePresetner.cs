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
        [Inject] ISingleTextSequenceEnterable<ITypingRoguelikeSingleSequenceMaster> _enterable;
        [Inject] ITypingEnterView _view;
        [Inject] TypingInputProcessor _inputProcessor;

        [Inject] TypingRoguelikeViewArgsFactory _argsFactory;
        [Inject] EnterKeyHundler _enterKeyHundler;
        [Inject] ITypingStarter _typingInitializer;
        [Inject] ISelectDataRegisterableView _selectDataRegisterableView;
        [Inject] ITextRegisterableView _textView;
        [Inject] IQuestionDisplayTextModel _questionTextGenerator;
        [Inject] ICorrectInputEnterableView _correctInputEnterableView;
        [Inject] IRestrictionRegisterableView _restrictionRegisterableView;
        [Inject] ISelectionDataSettable _selectionDataSettable;
        [Inject] IRequiredScoreGeneratable _requiredScoreGeneratable;
        [Inject] ISelectionDataWithIndexCatchableFake _selectionDataWithIndexCatchableFake;
        [Inject] RequiredPointView _requiredPointView;
        [Inject] KeyInputProcesser _keyInputProcesser;

        //----------point Œã‚Å•ª‚¯‚é‚©‚à------------------
        [Inject] IPointable _pointModel;
        [Inject] PointView _pointView;

        //---------timer Œã‚Å•ª‚¯‚é‚©‚à---------------
        [Inject] ITimerStartableModel _timerStartable;
        [Inject] ITimerEndableModel _timerEndable;
        [Inject] ITimerModel _timerModel;
        [Inject] ITimerView _timerView;
        [Inject] IHaltable _haltable;

        //fake
        [Inject] SelectionDataInitializer _selectionDataInitializer;


        [Inject] IDisposablePure _disposable;

        public void PostInitialize()
        {
            _enterable.Entered.Subscribe(x => _view.Enter(_argsFactory.Create(x).CancellationToken).Forget()).AddTo(_disposable);
            _timerStartable.TimerStarted.Subscribe(args  => _timerModel.Enter(args).Forget()).AddTo(_disposable);
            _inputProcessor.KeyEntered.Subscribe(x => _enterKeyHundler.EnterKey(x)).AddTo(_disposable);
            _selectionDataSettable.SelectionDataCreated.Subscribe(_selectDataRegisterableView.RegisterSelectData).AddTo(_disposable);
            _enterKeyHundler.Ended.Subscribe(_ => _view.EndLoop()).AddTo(_disposable);
            _typingInitializer.RestrictionDataLoaded.Subscribe(_restrictionRegisterableView.RegisterRestriction).AddTo(_disposable);
            _typingInitializer.SampleInputted.Subscribe(_textView.SetSampleText);
            _questionTextGenerator.TextUpdated.Subscribe(_textView.RegisterText);
            _questionTextGenerator.CorrectInputted.Subscribe(_correctInputEnterableView.EnterCorrectInput);

            _textView.Initialize();

            _view.Exited.Subscribe(_ => {
                _model.EndSingle(); 
            }).AddTo(_disposable);

            _timerEndable.TimerEnded.Subscribe(_ => _timerModel.EndTimer()).AddTo(_disposable);
            _timerModel.Entered.Subscribe(_timerView.EnterTimer).AddTo(_disposable);
            _timerModel.Updated.Subscribe(_timerView.UpdateTimer).AddTo(_disposable);
            _timerModel.TimeUped.Subscribe(_ => _haltable.Halt()).AddTo(_disposable);
            _timerModel.TimeRemained.Subscribe(time =>
            {
                _pointModel.AddRemainTimePoint(time);
                _timerView.EndTimer();
            }).AddTo(_disposable);
            
            _pointModel.PointUpdated.Subscribe(_pointView.UpdatePoint).AddTo(_disposable);
            _pointModel.Initialized.Subscribe(_ => _pointView.Initialize()).AddTo(_disposable);
            _requiredScoreGeneratable.RequiredScoreGenerated.Subscribe(_requiredPointView.UpdatePoint).AddTo(_disposable);

            _typingInitializer.Initialize();
            _timerStartable.Initialize();
            _requiredScoreGeneratable.Initialize();
            _argsFactory.Initialize();

            //fake
            _selectionDataInitializer.SelectionDataInitialized.Subscribe(_selectionDataWithIndexCatchableFake.SetSelectionDataWithIndex).AddTo(_disposable);
            // _keyInputProcesser.ReplaceDataSelected.Subscribe(_selectionDataWithIndexCatchableFake.OnReplacedListSelected).AddTo(_disposable);
        }
    }
}