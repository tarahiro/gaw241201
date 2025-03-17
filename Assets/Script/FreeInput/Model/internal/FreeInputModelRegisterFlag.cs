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
    public class FreeInputModelRegisterFlag : IFreeInputGateFlowModel
    {
        IFreeInputGateModel _underlying;
        IGlobalFlagRegisterer _globalFlagRegisterer;
        FlagConst.Key _key;

        public FreeInputModelRegisterFlag(IFreeInputGateModel gateModel, IGlobalFlagRegisterer globalFlagRegisterer, FlagConst.Key key)
        {
            _underlying = gateModel;
            _globalFlagRegisterer = globalFlagRegisterer;
            _key = key;
        }

        public IObservable<Unit> Entered => _underlying.Entered;
        public IObservable<Unit> Exited => _underlying.Exited;
        public void Enter() => _underlying.Enter();


        public void Decide(string text)
        {
            _globalFlagRegisterer.RegisterFlag(_key, text);
            _underlying.Decide(text);
        }

        public void ForceDecide()
        {
            Decide(FlagConst.InitialValue(_key));
        }
    }
}