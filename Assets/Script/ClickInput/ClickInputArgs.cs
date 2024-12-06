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
    public class ClickInputArgs
    {
        public List<string> LabelList { get; set; } = new List<string>();
        public CancellationToken CancellationToken { get; set; }

        public ClickInputArgs(List<string> labelList, CancellationToken cancellationToken)
        {
            LabelList = labelList;
            CancellationToken = cancellationToken;
        }
    }
}