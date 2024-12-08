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
    public class HaltModel
    {
        [Inject] FlowHundler _flowHundler;
        public void Halt()
        {
            _flowHundler.SwitchFlow(new FlowSwitchArgs_Fake(FlowMasterConst.FlowMasterLabel.TrueEndFlow,""));
        }
    }
}