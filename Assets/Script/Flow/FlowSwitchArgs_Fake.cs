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
    public class FlowSwitchArgs_Fake
    {
        public FlowConst.FlowName FlowName { get; set; }
        public string InitialFlowId {  get; set; }

        public FlowSwitchArgs_Fake(FlowConst.FlowName flowName, string initialFlowId)
        {
            FlowName = flowName;
            InitialFlowId = initialFlowId;
        }
    }
}