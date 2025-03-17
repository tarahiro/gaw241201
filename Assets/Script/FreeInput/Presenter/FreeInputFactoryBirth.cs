using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class FreeInputFactoryBirth
    {
        //Model
        FreeInputIndexer _freeInputIndexer;
        ICharJudger _playerNameInputJudger;
        FreeInputUnfixedText _freeInputUnfixedText;
        IEndableJudger _endableJudger;
        IFreeInputCharHundler _freeInputCharHundler;
        IFreeInputGateModel _freeInputGateModel;
        FreeInputForceEnderByIndex _freeInputForceEnderByIndex;


        //View
        [Inject] FreeInputEndableDisplayView _freeInputEndableDisplayView;
        [Inject] FreeInputDisplayFlowView _freeInputTextDisplayView;
        IFreeInputProcessor _iFreeInputProcessor => _freeInputProcessor;
        FreeInputEntererView _freeInputEntererView;


        //Presenter
        IFreeInputPresenterCore _freeInputPresenterCore;


        //Presenter‚É‚Í–³ŠÖŒW‚Ì‚à‚Ì
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
            //Model
            _freeInputIndexer = new FreeInputIndexer(FlagConst.c_BirthMaxLength);
            _freeInputUnfixedText = new FreeInputUnfixedText(_freeInputIndexer);
            _playerNameInputJudger = new CharJudgerBirth(_freeInputIndexer, _freeInputUnfixedText);
            _endableJudger = new EnterableJudgerByLength(_freeInputUnfixedText, FlagConst.c_BirthMaxLength);
            _freeInputCharHundler = new FreeInputCharHundler(_playerNameInputJudger, _freeInputUnfixedText);
            _freeInputGateModel = new FreeInputFlowNameModel(new FreeInputGateModel(_freeInputUnfixedText), _globalFlagRegisterer);
            _freeInputForceEnderByIndex = new FreeInputForceEnderByIndex(_freeInputCharHundler, 7);

            //View
            _freeInputProcessor = new FreeInputProcessor(_decide, _cancel, _keyStroke, _disposablePure);
            _freeInputInputView = new FreeInputInputView(_viewFactory, _freeInputProcessor);
            _freeInputEntererView = new FreeInputEntererView(_freeInputTextDisplayView, _freeInputInputView);

            //Presenter
            _freeInputPresenterCore = 
                new FreeInputPresenterForcedEndByIndex(

                    new FreeInputPresenterRestrictedEnter(

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
                    _freeInputEndableDisplayView),
                _freeInputForceEnderByIndex);

            _freeInputPresenterCore.ActivatePresenter();

        }

        public IFreeInputGateModel GetGateModel()
        {
            return _freeInputGateModel;
        }
    }
}