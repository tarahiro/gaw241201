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
        FreeInputCharHundler _freeInputCharHundler;
        FreeInputPlayerNameModel _freeInputPlayerNameModel;

        FreeInputProcessor _freeInputProcessor;
        FreeInputInputView _freeInputInputView;
        FreeInputEntererView _freeInputEntererView;

        SettingMenuItemModelPlayerName _settingMenuItemModelPlayerName;

        FreeInputPresenterCore _freeInputPresenterCore;

        [Inject] IGlobalFlagProvider _globalFlagProvider;
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;

        [Inject] InputExecutorCommand _decide;
        [Inject] InputExecutorCommand _cancel;
        [Inject] InputExecutorKeyStroke _keyStroke;

        [Inject] InputViewFactory _viewFactory;
        [Inject] FreeInputTextDisplayView _freeInputTextDisplayView;

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

            _settingMenuItemModelPlayerName = new SettingMenuItemModelPlayerName(_freeInputPlayerNameModel);

            _freeInputPresenterCore = new FreeInputPresenterCore(
                _freeInputIndexer,
                _freeInputUnfixedText,
                _freeInputCharHundler,
                _freeInputPlayerNameModel,
                _freeInputTextDisplayView,
                _freeInputProcessor,
                _freeInputEntererView,
                _disposablePure);
            _freeInputPresenterCore.PostInitialize();

        }

        public SettingMenuItemModelPlayerName Get()
        {
            return _settingMenuItemModelPlayerName;
        }
    }
}
