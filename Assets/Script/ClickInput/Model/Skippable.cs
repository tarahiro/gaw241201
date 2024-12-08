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
    public class Skippable : IClickInputProcessor, IFlowSwitchable_Fake
    {
        Subject<FlowSwitchArgs_Fake> _switchFlow = new Subject<FlowSwitchArgs_Fake>();
        public IObservable<FlowSwitchArgs_Fake> SwitchFlow => _switchFlow;


        public ClickInputArgs CreateArgs(CancellationToken cancellationToken)
        {
            return new ClickInputArgs(new List<string>()
            {
                "ìríÜÇ©ÇÁ","èâÇﬂÇ©ÇÁ"
            }, cancellationToken);
        }

        public void Process(int _argsIndex)
        {
            switch (_argsIndex)
            {
                case 0:
                    _switchFlow.OnNext(new FlowSwitchArgs_Fake(FlowMasterConst.FlowMasterLabel.MainFlow, "205000TypingConversation"));
                    break;

                case 1:
                    _switchFlow.OnNext(new FlowSwitchArgs_Fake(FlowMasterConst.FlowMasterLabel.MainFlow, ""));
                    break;
            }
        }
    }
}