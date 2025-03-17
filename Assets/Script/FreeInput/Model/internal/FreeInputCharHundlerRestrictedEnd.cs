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
    public class FreeInputCharHundlerRestrictedEnd : IFreeInputCharHundler
    {
        IFreeInputCharHundler _underlying;
        IEndableJudger _enterableJudger;
        public FreeInputCharHundlerRestrictedEnd(IFreeInputCharHundler charHundler, IEndableJudger enterableJudger)
        {
            _underlying = charHundler;
            _enterableJudger = enterableJudger;
        }

        public IObservable<string> Ended => _underlying.Ended;

        public void CatchChar(char c) => _underlying.CatchChar(c);

        public void TryEnd()
        {
            if (_enterableJudger.IsEnterable())
            {
                _underlying.TryEnd();
            }
        }

        public void ForceEnd() => _underlying.ForceEnd();

        public void Delete() => _underlying.Delete();

    }
}