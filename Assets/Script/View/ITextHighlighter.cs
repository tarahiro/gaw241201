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
    public interface ITextHighlighter
    {
        void Construct(ITextScaleChanger textScaleChanger);
        void StartHighlight(int textIndex);
        void StopHighlight(int textIndex);
        TMP_TextInfo Tick(TMP_TextInfo info);
    }
}