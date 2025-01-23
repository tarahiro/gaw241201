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
    public class ActBgViewArgs
    {
        public string BodyId { get; set; }

        public int WaveNumber { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public ActBgViewArgs(string bodyId, int waveNumber, CancellationToken cancellationToken)
        {
            BodyId = bodyId;
            WaveNumber = waveNumber;
            CancellationToken = cancellationToken;
        }
    }
}