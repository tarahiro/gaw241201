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
    public class PlayerNameSetter : IPlayerNameSettable
    {
        [Inject] FreeInputTextDisplayView _textDisplayView;

        public void SetText(string text)
        {
            _textDisplayView.SetText(text);
        }

    }
}