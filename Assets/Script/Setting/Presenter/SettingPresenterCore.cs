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
    public class SettingPresenterCore
    {
        IMenuModelStartable _modelEnterable;
        IMenuModelEndable _modelExitable;

        IIndexerInputtableView _inputProcessor;
        IUiMenuModel _uiModel;
        IDisposablePure _disposable;

        [Inject]
        public SettingPresenterCore(IIndexerInputtableView inputProcessor, IUiMenuModel uiMenuModel, IDisposablePure disposablePure)
        {
            _inputProcessor = inputProcessor;
            _uiModel = uiMenuModel;
            _disposable = disposablePure;
        }


        public void PostInitialize()
        {
            //���j���[�J�n�E�I��
            //���ꂼ��̃N���X�̓�����underlying���Ȃ��̂ŁA�璷�ɂȂ邩������Ȃ�


            //InputProcessor�ƁAUIModel�̕R�Â�
            _inputProcessor.IndexerMoved.Subscribe(x => _uiModel.MoveFocus(x)).AddTo(_disposable);
            _inputProcessor.Decided.Subscribe(_ => _uiModel.Decide()).AddTo(_disposable);
        }
    }
}