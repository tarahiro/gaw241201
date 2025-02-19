using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.SwitchConst;

namespace gaw241201
{
    public interface ISwitchCommandProcessor
    {
        UniTask Process(string value);
    }

}