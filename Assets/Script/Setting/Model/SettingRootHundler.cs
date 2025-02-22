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
    public class SettingRootHundler : IChangeValueMonitorBySettings
    {
        [Inject] SettingStarter _starter;
        [Inject] SettingExiter _exiter;
        [Inject] IGlobalFlagProvider _globalFlagProvider;

        bool _isSettingStarted = false;
        Dictionary<FlagConst.Key, string> dictionary;


        Subject<List<MoniteredChanged>> _onChangedValue = new Subject<List<MoniteredChanged>>();
        public IObservable<List<MoniteredChanged>> OnChangedValue => _onChangedValue;



        public void Enter()
        {
            if (!_isSettingStarted)
            {
                dictionary = new Dictionary<FlagConst.Key, string>();
                foreach(var key in MoniterableKey)
                {
                    dictionary.Add(key, _globalFlagProvider.GetFlag(key));
                }
                _starter.MenuStart();
                _isSettingStarted = true;
            }
            else
            {
                List<MoniteredChanged> _moniteredChanged = new List<MoniteredChanged>();
                foreach(var key in MoniterableKey)
                {
                    if (dictionary[key] != _globalFlagProvider.GetFlag(key))
                    {
                        _moniteredChanged.Add(new MoniteredChanged( key, dictionary[key], _globalFlagProvider.GetFlag(key)));
                    }
                }
                _onChangedValue.OnNext(_moniteredChanged);
                _exiter.MenuEnd();
                _isSettingStarted = false;
            }
        }


        public readonly static FlagConst.Key[] MoniterableKey = new FlagConst.Key[]
        {
           FlagConst.Key. ApplicationTime,
             FlagConst.Key. InputTime,
            FlagConst.Key.  Name,
             FlagConst.Key. NameLower,
            FlagConst.Key.BirthDate,
            FlagConst.Key.IsRoguelikeEnabled,
        };

        public class MoniteredChanged
        {
            public FlagConst.Key Key;
            public string PrevValue;
            public string NowValue;

            public MoniteredChanged(FlagConst.Key key, string prevValue, string nowValue)
            {
                Key = key;
                PrevValue = prevValue;
                NowValue = nowValue;
            }
        }
    }
}