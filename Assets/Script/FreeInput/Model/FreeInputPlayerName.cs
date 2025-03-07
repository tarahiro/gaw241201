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
    public class FreeInputPlayerName : ISettingItemModelInputtable, IStringDecidable
    {
        //現状の責務
        //MenuItemとしてのEnterを受け取る責務
        //PlayerNameの変更を受け取る責務
        //FreeInputの決定を受け取る責務


        [Inject] FreeInputUnfixedText _freeInputUnfixedText;
        [Inject] IGlobalFlagProvider _globalFlagProvider;
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;

        Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;
        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;




        public void  Enter()
        {
            Log.Comment("ProfileItemPlayerNameにEnter");

            //Initializerを別クラスに分けるかも
            _freeInputUnfixedText.Enter(_globalFlagProvider.GetFlag(FlagConst.Key.Name));
            _entered.OnNext(Unit.Default);
        }

        public void End()
        {
            _freeInputUnfixedText.Exit();
            _exited.OnNext(Unit.Default);
        }


        public void Decide(string text)
        {
            Log.DebugLog("ProfileItemPlayeName:Decide");
            _globalFlagRegisterer.RegisterFlag(FlagConst.Key.Name, text);
            End();
        }
    }
}