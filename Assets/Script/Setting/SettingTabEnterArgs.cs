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
    public class SettingTabEnterArgs
    {
        public int TabIndex { get; private set; }
        public int MenuIndex { get; private set; }


        public CancellationToken CancellationToken { get; private set; }

        public SettingTabEnterArgs(int _tabIndex, int _menuIndex, CancellationToken _cancellationToken)
        {
            TabIndex = _tabIndex;
            MenuIndex = _menuIndex;
            CancellationToken = _cancellationToken;
        }
    }
}