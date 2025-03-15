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
    public class FreeInputSettingNameModel :IFreeInputGateModel, IPlayerNameInputtableModel
    {
        FreeInputGateModel _underlying;
        IGlobalFlagProvider _globalFlagProvider;
        IGlobalFlagRegisterer _globalFlagRegisterer;

        public FreeInputSettingNameModel(FreeInputGateModel freeInputUnfixedText, IGlobalFlagProvider globalFlagProvider, IGlobalFlagRegisterer globalFlagRegisterer)
        {
            _underlying = freeInputUnfixedText;
            _globalFlagProvider = globalFlagProvider;
            _globalFlagRegisterer = globalFlagRegisterer;
        }

        public IObservable<Unit> Entered => _underlying.Entered;
        public IObservable<Unit> Exited => _underlying.Exited;




        public void Enter()
        {
            Log.Comment("ProfileItemPlayerName‚ÉEnter");
            _underlying.InitialText = _globalFlagProvider.GetFlag(FlagConst.Key.Name);
            _underlying.Enter();
        }



        public void Decide(string text)
        {
            _globalFlagRegisterer.RegisterFlag(FlagConst.Key.Name, text);
            _underlying.Decide(text);
        }
    }
}