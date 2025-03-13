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
    public interface IFreeInputCharHundler
    {
        public void CatchChar(char c);
        public void End();
        public void Delete();
        public IObservable<string> Ended { get; }
    }
}