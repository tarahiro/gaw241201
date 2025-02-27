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
    public class InputHundlerKeyStroke : IInputHundlerKeyStroke
    {
        [Inject] PureSingletonKey _key;

        public string StrokedKey()
        {
            return _key.GetStrokedKey();
        }
        
        //NotifyUse�͂���Ȃ������H
        //���Ȃ�L�͂ȓ��͂�L���Ƃ��Ď��̂ŁA���̃L�[���͂�j�Q������
    }
}