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
    public class FreeInputPlayerNameModel : IEnterTimingNotifiable, IStringDecidable, IPlayerNameInputtableModel
    {
        //Œ»ó‚ÌÓ–±
        //PlayerName‚Ì•ÏX‚ğó‚¯æ‚éÓ–±
        //FreeInput‚ÌŒˆ’è‚ğó‚¯æ‚éÓ–±


        [Inject] FreeInputUnfixedText _freeInputUnfixedText;
        [Inject] IGlobalFlagProvider _globalFlagProvider;
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;

        Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;
        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;




        public void Enter()
        {
            Log.Comment("ProfileItemPlayerName‚ÉEnter");

            //Initializer‚ğ•ÊƒNƒ‰ƒX‚É•ª‚¯‚é‚©‚à
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