using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class PlayerNameInputJudger : ICharInputJudger
    {
        public bool IsCharAvailable(char c)
        {
            return char.IsLetterOrDigit(c);
        }
    }
}