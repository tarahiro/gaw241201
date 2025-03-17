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
    public class FreeInputFlowBirthModel : IFreeInputGateModel
    {
        IFreeInputGateModel _underlying;
        IGlobalFlagRegisterer _globalFlagRegisterer;

        public FreeInputFlowBirthModel(IFreeInputGateModel gateModel, IGlobalFlagRegisterer globalFlagRegisterer)
        {
            _underlying = gateModel;
            _globalFlagRegisterer = globalFlagRegisterer;
        }

        public IObservable<Unit> Entered => _underlying.Entered;
        public IObservable<Unit> Exited => _underlying.Exited;
        public void Enter() => _underlying.Enter();


        public void Decide(string text)
        {
            _globalFlagRegisterer.RegisterFlag(FlagConst.Key.BirthDate, text);
            _underlying.Decide(text);
        }
    }
}