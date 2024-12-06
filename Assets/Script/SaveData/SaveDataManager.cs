using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201
{
    public class SaveDataManager : ILoadable, ISavable, ISaveDeletable
    {
        [Inject] SaveData _saveData;
        [Inject] IGlobalFlagRegisterer _registerer;
        [Inject] IGlobalFlagProvider _provider;

        public void Load()
        {
            Log.Comment("���[�h�J�n");
            foreach(var key in SaveDataConst.SavableKeys)
            {
                if(_saveData.TryGetString(key.ToString(), out var s))
                {
                    Log.Comment(key + "��o�^");
                    _registerer.RegisterFlag(key, s);
                }
            }
        }

        public void Save()
        {
            Log.Comment("�Z�[�u�J�n");
            if (!_provider.IsContainskey(FlagConst.Key.IsSaveDataExist))
            {
                Log.Comment("�Z�[�u�t���O�ǉ�");
                _registerer.RegisterFlag(FlagConst.Key.IsSaveDataExist, "True");
            }
            else if (_provider.GetFlag(FlagConst.Key.IsSaveDataExist) == "False")
            {
                Log.Comment("�Z�[�u�t���O�ǉ�");
                _registerer.RegisterFlag(FlagConst.Key.IsSaveDataExist, "True");
            }


            foreach (var key in SaveDataConst.SavableKeys)
            {
                Log.Comment(key + "���Z�[�u");
                _saveData.SaveString(key.ToString(), _provider.GetFlag(key));
            }
        }

        public void DeleteSaveData()
        {
            Log.Comment("�Z�[�u����");
            _saveData.DeleteSave();
        }
    }
}