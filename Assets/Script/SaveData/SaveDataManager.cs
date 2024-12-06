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
            Log.Comment("ロード開始");
            foreach(var key in SaveDataConst.SavableKeys)
            {
                if(_saveData.TryGetString(key.ToString(), out var s))
                {
                    Log.Comment(key + "を登録");
                    _registerer.RegisterFlag(key, s);
                }
            }
        }

        public void Save()
        {
            Log.Comment("セーブ開始");
            if (!_provider.IsContainskey(FlagConst.Key.IsSaveDataExist))
            {
                Log.Comment("セーブフラグ追加");
                _registerer.RegisterFlag(FlagConst.Key.IsSaveDataExist, "True");
            }
            else if (_provider.GetFlag(FlagConst.Key.IsSaveDataExist) == "False")
            {
                Log.Comment("セーブフラグ追加");
                _registerer.RegisterFlag(FlagConst.Key.IsSaveDataExist, "True");
            }


            foreach (var key in SaveDataConst.SavableKeys)
            {
                Log.Comment(key + "をセーブ");
                _saveData.SaveString(key.ToString(), _provider.GetFlag(key));
            }
        }

        public void DeleteSaveData()
        {
            Log.Comment("セーブ消去");
            _saveData.DeleteSave();
        }
    }
}