using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201
{
    public static class EndGameConst
    {
        public enum Key
        {
            GameOver,
            GameClear,
            GameOverExhibition,
            GameClearExhibition,
        }
    }
}