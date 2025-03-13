using Cysharp.Threading.Tasks;
using MessagePipe;
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
    public class SettingMenuItemModelPlayerName : IUiMenuItemModel
    {
        IPlayerNameInputtableModel _playerNameInputtableModel;

        public SettingMenuItemModelPlayerName(IPlayerNameInputtableModel playerNameInputtableModel)
        {
            _playerNameInputtableModel = playerNameInputtableModel;
        }

        public void  Enter()
        {
            Log.Comment("ProfileItemPlayerName‚ÉEnter");

            //Initializer‚ð•ÊƒNƒ‰ƒX‚É•ª‚¯‚é‚©‚à
            _playerNameInputtableModel.Enter();
        }

    }
}