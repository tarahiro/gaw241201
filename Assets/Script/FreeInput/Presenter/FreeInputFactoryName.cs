using VContainer;
using VContainer.Unity;
using gaw241201.View;
using gaw241201.Model;
using Tarahiro;

namespace gaw241201.Presenter
{
    public class FreeInputFactoryName
    {
        //Model
        FreeInputIndexer _freeInputIndexer;
        CharJudgerName _playerNameInputJudger;
        FreeInputUnfixedText _freeInputUnfixedText;
        IEndableJudger _endableJudger;
        IFreeInputCharHundler _freeInputCharHundler;
        IFreeInputGateFlowModel _freeInputGateModel;


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
            _freeInputIndexer = new FreeInputIndexer(FlagConst.c_NameMaxLength);
            _playerNameInputJudger = new CharJudgerName(_freeInputIndexer);
            _freeInputUnfixedText = new FreeInputUnfixedText(_freeInputIndexer);
            _endableJudger = new EnterableJudgerByLength(_freeInputUnfixedText, FlagConst.c_NameMinLength);
            _freeInputCharHundler = new FreeInputCharHundlerRestrictedEnd(new FreeInputCharHundler(_playerNameInputJudger, _freeInputUnfixedText),
                _endableJudger);
            _freeInputGateModel = new FreeInputModelRegisterFlag(new FreeInputGateModel(_freeInputUnfixedText), _globalFlagRegisterer, FlagConst.Key.Name);

            //View
            _freeInputProcessor = new FreeInputProcessor(_decide,_cancel,_keyStroke,_disposablePure);
            _freeInputInputView = new FreeInputInputView(_viewFactory, _freeInputProcessor);
            _freeInputEntererView = new FreeInputEntererView(_freeInputTextDisplayView, _freeInputInputView);

            //Presenter
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
                _freeInputEndableDisplayView
                );
            _freeInputPresenterCore.ActivatePresenter();

        }

        public IFreeInputGateFlowModel GetGateModel()
        {
            return _freeInputGateModel;
        }
    }
}
