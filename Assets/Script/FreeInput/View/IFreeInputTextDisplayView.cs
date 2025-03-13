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
    public interface IFreeInputTextDisplayView
    {
        public void SetCharacter(FreeInputArgs args);

        public void Focus(int index);

        public void Unfocus(int index);
    }
}