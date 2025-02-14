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
    public class SettingUiMenuItemEmptyFactory
    {
        [Inject] SettingEventCatcher _eventCatcher;
        public SettingUiMenuItemModelEmpty Create(string conversationId)
        {
            return new SettingUiMenuItemModelEmpty(conversationId, _eventCatcher);
        }
    }
}