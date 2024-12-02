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
    public class FreeInputArgsFactory
    {
        public FreeInputArgs Create(string bodyId, CancellationToken cancellationToken)
        {
            return new FreeInputArgs(bodyId, cancellationToken);
        }
    }
}