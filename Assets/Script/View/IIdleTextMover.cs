using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public interface IIdleTextMover
    {
        void Construct(TMP_Text tmpText, ITextScaleChanger textScaleChanger);

        void Initialize();

        void StartIdle();

        void StopIdle();

        TMP_TextInfo Tick(TMP_TextInfo info);
    }
}