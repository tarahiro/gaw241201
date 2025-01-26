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
    public class SettingExitArgs
    {
        public CancellationToken CancellationToken { get; private set; }

        public SettingExitArgs(CancellationToken _cancellationToken)
        {
            CancellationToken = _cancellationToken;
        }
    }
}