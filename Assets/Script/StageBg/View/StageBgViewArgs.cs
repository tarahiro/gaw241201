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

namespace gaw241201.View
{
    public class StageBgViewArgs
    {
        public string BodyId { get; set; }

        public int WaveNumber { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public StageBgViewArgs(string bodyId, int waveNumber, CancellationToken cancellationToken)
        {
            BodyId = bodyId;
            WaveNumber = waveNumber;
            CancellationToken = cancellationToken;
        }
    }
}