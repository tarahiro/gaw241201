using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{

    public class NameFreeInputView : FreeInputItemView
    {
        protected override string defaultValue { get; set; } = "NAME";
        protected override bool IsInputCharValid(int index, char key)
        {
            return char.IsLetterOrDigit(key);
        }

        protected override bool IsAcceptEnter()
        {
            return _index >= 1;
        }
    }
}