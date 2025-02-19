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
    public class CommandProcessorFlow : ISwitchCommandProcessor
    {

        FlowSwitchPublisher _publisher;

        public CommandProcessorFlow(FlowSwitchPublisher publisher)
        {
            _publisher = publisher;
        }

        public async UniTask Process(string value)
        {
            _publisher.Publish(new FlowSwitchArgs(EnumUtil.KeyToType<FlowMasterConst.FlowMasterLabel>(value), ""));
        }
    }
}