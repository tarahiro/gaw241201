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
        string _errorConversationId;
        SettingEventCatcher _eventCatcher;

        [Inject]
        public SettingUiMenuItemModelEmpty(string errorConversationId, SettingEventCatcher eventCatcher)
        {
            _errorConversationId = errorConversationId;
            _eventCatcher = eventCatcher;
        }

        public void Enter()
        {
                _eventCatcher.OnEnterDenied(_errorConversationId);
        }

    }
}