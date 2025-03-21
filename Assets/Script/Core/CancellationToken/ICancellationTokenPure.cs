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
    public interface ICancellationTokenPure
    {
        void Cancel();
        void SetNew();
        CancellationToken Token { get; }

        bool IsCancellationRequested { get; }
    }
}