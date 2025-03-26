using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using gaw241201.View;

namespace gaw241201
{
    public class UiPresenterCore
    {
        IMenuModelStartable _starter;
        IMenuModelEndable _ender;

        IMenuRootView _rootView;
        IIndexerInputtableView _inputProcessor;
        IUiMenuModel _uiModel;
        IDisposablePure _disposable;

        List<IUiMenuModel> _menuModelList;
        IMenuView _menuView;

        [Inject]
        public UiPresenterCore(
            IMenuModelStartable starter,
            IMenuModelEndable ender, 
            IMenuRootView rootView,  
            IIndexerInputtableView inputProcessor, 
            IUiMenuModel uiMenuModel,
            List<IUiMenuModel> menuModelList,
            IMenuView menuView,
            IDisposablePure disposablePure)
        {
            _starter = starter;
            _ender = ender;
            _rootView = rootView;
            _inputProcessor = inputProcessor;
            _uiModel = uiMenuModel;
            _menuModelList = menuModelList;
            _menuView = menuView;
            _disposable = disposablePure;
        }


        public void Present()
        {
            //���j���[�J�n�E�I��
            //���ꂼ��̃N���X�̓�����underlying���Ȃ��̂ŁA�璷�ɂȂ邩������Ȃ�
            _starter.Started.Subscribe(_ => _rootView.EnterRoot()).AddTo(_disposable);
            _ender.MenuEnded.Subscribe(_ => _rootView.EndRoot()).AddTo(_disposable);

            //InputProcessor�ƁAUIModel�̕R�Â�
            _inputProcessor.IndexerMoved.Subscribe(x => _uiModel.MoveFocus(x)).AddTo(_disposable);
            _inputProcessor.Decided.Subscribe(_ => _uiModel.Decide()).AddTo(_disposable);

            //MenuModel�ƁAView��Current�̕R�Â�
            foreach(var item in _menuModelList)
            {
                item.Entered.Subscribe(x => _menuView.Enter(x).Forget()).AddTo(_disposable);
                item.Decided.Subscribe(x => _menuView.Decide(x).Forget()).AddTo(_disposable);
                item.FocusChanged.Subscribe(x => _menuView.SetFocus(x).Forget()).AddTo(_disposable);
                item.Exited.Subscribe(_ => _menuView.Exit().Forget()).AddTo(_disposable);
            }

        }
    }
}