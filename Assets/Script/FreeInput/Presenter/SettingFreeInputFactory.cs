using VContainer;
using VContainer.Unity;
using gaw241201.View;
using gaw241201.Model;
using Tarahiro;

namespace gaw241201.Presenter
{
    public class SettingFreeInputFactory
    {
        FreeInputIndexer _freeInputIndexer;
        PlayerNameInputJudger _playerNameInputJudger;
        FreeInputUnfixedText _freeInputUnfixedText;
        IFreeInputCharHundler _freeInputCharHundler;
        IFreeInputGateModel _freeInputGateModel => _freeInputPlayerNameModel;

        [Inject] SettingFreeInputDisplayView _freeInputTextDisplayView;
        IFreeInputProcessor _iFreeInputProcessor => _freeInputProcessor;
        FreeInputEntererView _freeInputEntererView;

        FreeInputPresenterCore _freeInputPresenterCore;

        FreeInputInputView _freeInputInputView;

        FreeInputProcessor _freeInputProcessor;
        FreeInputPlayerNameModel _freeInputPlayerNameModel;

        [Inject] IGlobalFlagProvider _globalFlagProvider;
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;

        [Inject] InputExecutorCommand _decide;
        [Inject] InputExecutorCommand _cancel;
        [Inject] InputExecutorKeyStroke _keyStroke;

        [Inject] InputViewFactory _viewFactory;

        [Inject] IDisposablePure _disposablePure;

        public void Initialize()
        { 
            Log.Comment("SettingFreeInputLifetimeScope‚Ì“o˜^ŠJŽn");

            _freeInputIndexer = new FreeInputIndexer();
            _playerNameInputJudger = new PlayerNameInputJudger(_freeInputIndexer);
            _freeInputUnfixedText = new FreeInputUnfixedText(_freeInputIndexer);
            _freeInputCharHundler = new FreeInputCharHundler(_playerNameInputJudger, _freeInputUnfixedText);
            _freeInputPlayerNameModel = new FreeInputPlayerNameModel(_freeInputUnfixedText, _globalFlagProvider, _globalFlagRegisterer);

            _freeInputProcessor = new FreeInputProcessor(_decide,_cancel,_keyStroke,_disposablePure);
            _freeInputInputView = new FreeInputInputView(_viewFactory, _freeInputProcessor);
            _freeInputEntererView = new FreeInputEntererView(_freeInputTextDisplayView, _freeInputInputView);

            _freeInputPresenterCore = new FreeInputPresenterCore(
                _freeInputIndexer,
                _freeInputUnfixedText,
                _freeInputCharHundler,
                _freeInputGateModel,
                _freeInputTextDisplayView,
                _iFreeInputProcessor,
                _freeInputEntererView,
                _disposablePure);
            _freeInputPresenterCore.PostInitialize();

        }

        public IPlayerNameInputtableModel Get()
        {
            return _freeInputPlayerNameModel;
        }
    }
}
