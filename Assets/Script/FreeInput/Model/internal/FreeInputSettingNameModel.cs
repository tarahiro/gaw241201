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
        FreeInputUnfixedText _freeInputUnfixedText;
        IGlobalFlagProvider _globalFlagProvider;
        IGlobalFlagRegisterer _globalFlagRegisterer;

        public FreeInputSettingNameModel(FreeInputUnfixedText freeInputUnfixedText, IGlobalFlagProvider globalFlagProvider, IGlobalFlagRegisterer globalFlagRegisterer)
        {
            _freeInputUnfixedText = freeInputUnfixedText;
            _globalFlagProvider = globalFlagProvider;
            _globalFlagRegisterer = globalFlagRegisterer;
        }


        Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;

        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;




        public void Enter()
        {
            Log.Comment("ProfileItemPlayerName‚ÉEnter");

            _freeInputUnfixedText.Enter(_globalFlagProvider.GetFlag(FlagConst.Key.Name));
            _entered.OnNext(Unit.Default);
        }



        public void Decide(string text)
        {
            Log.DebugLog("ProfileItemPlayeName:Decide");
            _globalFlagRegisterer.RegisterFlag(FlagConst.Key.Name, text);
            _freeInputUnfixedText.Exit();
            _exited.OnNext(Unit.Default);
        }
    }
}