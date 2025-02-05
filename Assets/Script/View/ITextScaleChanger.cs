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
    public interface ITextScaleChanger
    {
        void Construct(TMP_Text tmpText);
        void Initialize();
        void TextScaleChange(int textIndex, Vector2 scale);
    }
}