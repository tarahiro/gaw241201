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
    public class TypingViewArgs
    {
        public string JpText { get; private set; }
        public string RomanText { get; private set; }
        public CancellationToken CancellationToken { get; private set; }

        public TypingViewArgs(string jpText, string romanText, CancellationToken cancellationToken)
        {
            JpText = jpText;
            RomanText = romanText;
            CancellationToken = cancellationToken;
        }
    }
}