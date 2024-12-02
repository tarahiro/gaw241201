using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class TypingModelArgs
    {
        public ITypingMaster Master { get; private set; }
        public CancellationToken CancellationToken { get; private set; }

        public TypingModelArgs(ITypingMaster master, CancellationToken cancellationToken)
        {
            Master = master;
            CancellationToken = cancellationToken;
        }
    }
}