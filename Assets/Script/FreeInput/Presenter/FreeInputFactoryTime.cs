using Cysharp.Threading.Tasks;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public class FreeInputFactoryTime
    {
        FreeInputIndexer _freeInputIndexer;
        ICharJudger _charJudger;
        FreeInputUnfixedText _freeInputUnfixedText;
        IEndableJudger _endableJudger;
        IFreeInputCharHundler _freeInputCharHundler;
        IFreeInputGateModel _freeInputGateModel;

        [Inject] FreeInputEndableDisplayView _freeInputEndableDisplayView;
        [Inject] FreeInputDisplayFlowView _freeInputTextDisplayView;
        IFreeInputProcessor _iFreeInputProcessor => _freeInputProcessor;
        FreeInputEntererView _freeInputEntererView;

        IFreeInputPresenter _freeInputPresenterCore;

        FreeInputInputView _freeInputInputView;

        FreeInputProcessor _freeInputProcessor;

        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;

        [Inject] InputExecutorCommand _decide;
        [Inject] InputExecutorCommand _cancel;
        [Inject] InputExecutorKeyStroke _keyStroke;

        [Inject] InputViewFactory _viewFactory;

        [Inject] IDisposablePure _disposablePure;

        public void Initialize()
        {
            _freeInputIndexer = new FreeInputIndexer(FlagConst.c_TimeMaxLength);
            _freeInputUnfixedText = new FreeInputUnfixedText(_freeInputIndexer);
            _charJudger = new CharJudgerTime(_freeInputIndexer,_freeInputUnfixedText);
            _endableJudger = new EnterableJudgerByLength( _freeInputUnfixedText, FlagConst.c_TimeMaxLength);
            _freeInputCharHundler = new FreeInputCharHundlerRestrictedEnd(
                new FreeInputCharHundler(_charJudger, _freeInputUnfixedText),
                _endableJudger);
            _freeInputGateModel = new FreeInputFlowTimeModel(new FreeInputGateModel(_freeInputUnfixedText), _globalFlagRegisterer);

            _freeInputProcessor = new FreeInputProcessor(_decide, _cancel, _keyStroke, _disposablePure);
            _freeInputInputView = new FreeInputInputView(_viewFactory, _freeInputProcessor);
            _freeInputEntererView = new FreeInputEntererView(_freeInputTextDisplayView, _freeInputInputView);

            _freeInputPresenterCore = new FreeInputPresenterRestrictedEnter(
                new FreeInputPresenterCore(
                _freeInputIndexer,
                _freeInputUnfixedText,
                _freeInputCharHundler,
                _freeInputGateModel,
                _freeInputTextDisplayView,
                _iFreeInputProcessor,
                _freeInputEntererView,
                _disposablePure),

                _endableJudger,
                _freeInputEndableDisplayView);

            _freeInputPresenterCore.ActivatePresenter();

        }

        public IFreeInputGateModel GetGateModel()
        {
            return _freeInputGateModel;
        }
    }
}