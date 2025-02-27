using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class EndGameArgs
    {
        public CancellationToken Ct { get; private set; }
        public EndGameConst.Key Key { get; private set; }

        public EndGameArgs(CancellationToken ct, EndGameConst.Key bodyId)
        {
            Ct = ct;
            Key = bodyId;
        }
    }
}