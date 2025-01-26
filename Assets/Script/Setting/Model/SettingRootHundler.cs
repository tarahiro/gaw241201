using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201
{
    public class SettingRootHundler
    {
        [Inject] SettingStarter _starter;
        [Inject] SettingExiter _exiter;

        bool _isSettingStarted = false;

        public async UniTask Enter()
        {
            if (!_isSettingStarted)
            {
                await _starter.Enter();
                _isSettingStarted = true;
            }
            else
            {
                await _exiter.Enter();
                _isSettingStarted = false;
            }
        }
    }
}