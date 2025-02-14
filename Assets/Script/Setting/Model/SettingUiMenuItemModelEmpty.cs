using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class SettingUiMenuItemModelEmpty : IUiMenuItemModel
    {
        IUiMenuItemModel _uiMenuItemModel;

        public IObservable<Unit> Entered => _uiMenuItemModel.Entered;
        public IObservable<Unit> Exited => _uiMenuItemModel.Exited;

        public bool IsEnterable => _uiMenuItemModel.IsEnterable;

        string _errorConversationId;
        SettingEventCatcher _eventCatcher;

        [Inject]
        public SettingUiMenuItemModelEmpty(string errorConversationId, SettingEventCatcher eventCatcher)
        {
            _uiMenuItemModel = new UiMenuItemModel(false);
            _errorConversationId = errorConversationId;
            _eventCatcher = eventCatcher;
        }

        public async UniTask Enter()
        {
            if (IsEnterable)
            {
                await _uiMenuItemModel.Enter();

            }
            else
            {
                _eventCatcher.OnEnterDenied(_errorConversationId);
            }
        }

        public void End()
        {
            _uiMenuItemModel.End();
        }
    }
}