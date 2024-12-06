using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.Experimental.AI;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class DoubleAffirmation : IClickInputProcessor
    {
        public ClickInputArgs CreateArgs(CancellationToken cancellationToken)
        {
            return new ClickInputArgs(new List<string>()
            {
                "‚Í‚¢","Yes"
            }, cancellationToken);
        }

        public void Process(int _argsIndex)
        {

        }
    }
}