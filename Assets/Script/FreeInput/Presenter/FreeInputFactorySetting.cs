using VContainer;
using VContainer.Unity;
using gaw241201.View;
using gaw241201.Model;
using Tarahiro;

namespace gaw241201.Presenter
{
    public class FreeInputFactorySetting
    {
        FreeInputIndexer _freeInputIndexer;
        CharJudgerName _playerNameInputJudger;
        FreeInputUnfixedText _freeInputUnfixedText;
        IEndableJudger _endableJudger;
        IFreeInputCharHundler _freeInputCharHundler;
        IFreeInputGateModel _freeInputGateModel => _freeInputPlayerNameModel;

        [Inject] SettingFreeInputDisplayView _freeInputTextDisplayView;
        [Inject] FreeInputEndableDisplayView _freeInputEndableDisplayView;
        IFreeInputProcessor _iFreeInputProcessor => _freeInputProcessor;
        FreeInputEntererView _freeInputEntererView;

        IFreeInputPresenterCore _freeInputPresenterCore;

        FreeInputInputView _freeInputInputView;

        FreeInputProcessor _freeInputProcessor;
        FreeInputSettingNameModel _freeInputPlayerNameModel;

        [Inject] IGlobalFlagProvider _globalFlagProvider;
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;

        [Inject] InputExecutorCommand _decide;
        [Inject] InputExecutorCommand _cancel;
        [Inject] InputExecutorKeyStroke _keyStroke;

        [Inject] InputViewFactory _viewFactory;

        [Inject] IDisposablePure _disposablePure;

        public void Initialize()
        { 
            _freeInputIndexer = new FreeInputIndexer(FlagConst.c_NameMaxLength);
            _playerNameInputJudger = new CharJudgerName(_freeInputIndexer);
            _freeInputUnfixedText = new FreeInputUnfixedText(_freeInputIndexer);
            _endableJudger = new EnterableJudgerByLength(_freeInputUnfixedText, FlagConst.c_NameMinLength);
            _freeInputCharHundler = new FreeInputCharHundlerRestrictedEnd(
                new FreeInputCharHundler(_playerNameInputJudger, _freeInputUnfixedText),
                _endableJudger);
            _freeInputPlayerNameModel = new FreeInputSettingNameModel(new FreeInputGateModel(_freeInputUnfixedText), _globalFlagProvider, _globalFlagRegisterer);

            _freeInputProcessor = new FreeInputProcessor(_decide,_cancel,_keyStroke,_disposablePure);
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

        public IPlayerNameInputtableModel Get()
        {
            return _freeInputPlayerNameModel;
        }
    }
}
