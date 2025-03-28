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
    public class Skippable : IClickInputProcessor
    {
        public ClickInputArgs CreateArgs(CancellationToken cancellationToken)
        {
            return new ClickInputArgs(new List<string>()
            {
                "途中から","初めから"
            }, cancellationToken);
        }

        public void Process(int _argsIndex)
        {
            //現状動かない

            /*
            switch (_argsIndex)
            {
                case 0:
                    _switchFlow.OnNext(new FlowSwitchArgs(FlowMasterConst.FlowMasterLabel.HorrorStoryMainFlow, "205000TypingConversation"));
                    break;

                case 1:
                    _switchFlow.OnNext(new FlowSwitchArgs(FlowMasterConst.FlowMasterLabel.HorrorStoryMainFlow, ""));
                    break;
            }
            */
        }
    }
}