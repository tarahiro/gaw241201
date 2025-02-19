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
    public class CmdHaltModel
    {
        [Inject] FlowSwitchPublisher _publisher;
        [Inject] FlowHundler _flowHundler;
        public void Halt()
        {
            _publisher.Publish(new FlowSwitchArgs(FlowMasterConst.FlowMasterLabel.TrueEndFlow,""));
        }
    }
}