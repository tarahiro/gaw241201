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
    public class FreeInputArgs
    {
        public char Character { get; private set; }
        public int Index { get; private set; }

        public FreeInputArgs(char character, int index)
        {
            Character = character;
            Index = index;
        }
    }
}