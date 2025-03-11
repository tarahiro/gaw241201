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
        //����̐Ӗ�
        //MenuItem�Ƃ��Ă�Enter���󂯎��Ӗ�
        //PlayerName�̕ύX���󂯎��Ӗ�
        //FreeInput�̌�����󂯎��Ӗ�

        [Inject] IPlayerNameInputtableModel _playerNameInputtableModel;


        public void  Enter()
        {
            Log.Comment("ProfileItemPlayerName��Enter");

            //Initializer��ʃN���X�ɕ����邩��
            _playerNameInputtableModel.Enter();
        }

    }
}