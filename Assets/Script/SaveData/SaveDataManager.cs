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
        [Inject] FakeStaticSaveData fakeStaticSaveData;
        [Inject] ISaveDataProvider _dataProvider;
        [Inject] IGlobalFlagRegisterer _registerer;
        [Inject] IGlobalFlagProvider _provider;

        public void Load()
        {
            Log.Comment("ロード開始");
            foreach(var key in SaveDataConst.SavableKeys)
            {
                if(_dataProvider.Provide().TryGetString(key.ToString(), out var s))
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
                _registerer.RegisterFlag(FlagConst.Key.IsSaveDataExist, Tarahiro.Const.c_true);
            }
            else if (_provider.GetFlag(FlagConst.Key.IsSaveDataExist) == Tarahiro.Const.c_false)
            {
                Log.Comment("セーブフラグ追加");
                _registerer.RegisterFlag(FlagConst.Key.IsSaveDataExist, Tarahiro.Const.c_true);
            }


            foreach (var key in SaveDataConst.SavableKeys)
            {
                Log.Comment(key + "をセーブ : " + _provider.GetFlag(key));
                fakeStaticSaveData.SaveString(key.ToString(), _provider.GetFlag(key));
            }
        }

        public void DeleteSaveData()
        {
            Log.Comment("セーブ消去");
            _dataProvider.Provide().DeleteSave();
        }
    }
}