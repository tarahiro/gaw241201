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
    public class TypingModelArgsFactory
    {
        public TypingModelArgs Create(ITypingMaster master , CancellationToken cancellationToken)
        {
            return new TypingModelArgs(master, cancellationToken);
        }
    }
}