using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using System.Threading;

namespace gaw241201
{
    public class SettingEnterArgs
    {
        public CancellationToken CancellationToken { get; private set; }

        public SettingEnterArgs(CancellationToken _cancellationToken)
        {
            CancellationToken = _cancellationToken;
        }
    }
}