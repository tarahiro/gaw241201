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
        //Œ»ó‚ÌÓ–±
        //MenuItem‚Æ‚µ‚Ä‚ÌEnter‚ğó‚¯æ‚éÓ–±
        //PlayerName‚Ì•ÏX‚ğó‚¯æ‚éÓ–±
        //FreeInput‚ÌŒˆ’è‚ğó‚¯æ‚éÓ–±

        [Inject] IPlayerNameInputtableModel _playerNameInputtableModel;


        public void  Enter()
        {
            Log.Comment("ProfileItemPlayerName‚ÉEnter");

            //Initializer‚ğ•ÊƒNƒ‰ƒX‚É•ª‚¯‚é‚©‚à
            _playerNameInputtableModel.Enter();
        }

    }
}