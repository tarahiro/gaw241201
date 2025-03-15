using Cysharp.Threading.Tasks;
using gaw241201.Inject;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using gaw241201.Presenter;

namespace gaw241201
{
    public class ProfileMenuItemListFactory : ISettingProfileItemListProvider
    {
        SettingMenuItemModelPlayerName _playerName;
        [Inject] SettingUiMenuItemEmptyFactory _emptyFactory;
        [Inject] SettingFreeInputFactory _freeInputFactory;


        public List<IUiMenuItemModel> ProvideList()
        {
            var _returnable = new List<IUiMenuItemModel>();
            _returnable.Add(_emptyFactory.Create("ErrorConversationSignature"));

            if(_playerName == null)
            {
                _playerName = new SettingMenuItemModelPlayerName(_freeInputFactory.Get());
            }
            _returnable.Add(_playerName);

            _returnable.Add(_emptyFactory.Create("ErrorConversationSex"));
            return _returnable;
        }
    }
}