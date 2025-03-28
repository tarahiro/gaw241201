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
    public class FlowSwitchArgs
    {
        public FlowMasterConst.FlowMasterLabel FlowName { get; set; }
        public string InitialFlowId {  get; set; }

        public FlowSwitchArgs(FlowMasterConst.FlowMasterLabel flowName, string initialFlowId)
        {
            FlowName = flowName;
            InitialFlowId = initialFlowId;
        }
    }
}