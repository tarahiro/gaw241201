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
    public class ProfileMenuItemListFactory
    {
        [Inject] ProfileItemPlayerName _playerName;
        [Inject] SettingUiMenuItemEmptyFactory _emptyFactory;

        public List<IUiMenuItemModel> CreateList()
        {
            var _returnable = new List<IUiMenuItemModel>();
            _returnable.Add(_emptyFactory.Create("ErrorConversationSignature"));
            _returnable.Add(_playerName);
            _returnable.Add(_emptyFactory.Create("ErrorConversationSex"));
            return _returnable;
        }
    }
}