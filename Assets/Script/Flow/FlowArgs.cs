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
    public class FlowArgs
    {
        public string BodyId { get; set; }
        public CancellationToken CancellationToken { get; set; }

        public FlowArgs(string bodyId, CancellationToken cancellationToken)
        {
            this.BodyId = bodyId;
            this.CancellationToken = cancellationToken;
        }
    }
}