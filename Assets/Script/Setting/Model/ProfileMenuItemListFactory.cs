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
    public class ProfileMenuItemListFactory : ISettingProfileItemListProvider
    {
        [Inject] ISettingItemModelInputtable _playerName;
        [Inject] SettingUiMenuItemEmptyFactory _emptyFactory;

        public List<IUiMenuItemModel> ProvideList()
        {
            var _returnable = new List<IUiMenuItemModel>();
            _returnable.Add(_emptyFactory.Create("ErrorConversationSignature"));
            _returnable.Add(_playerName);
            _returnable.Add(_emptyFactory.Create("ErrorConversationSex"));
            return _returnable;
        }

        public ISettingItemModelInputtable GetPlayerName()
        {
            return _playerName;
        }
    }
}