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
    public class EndGameCoreModelProvider
    {
        [Inject] EndGameCore_Old _oldModel;

        public IEndGameCore Provide(string bodyId)
        {
            //�Ƃ肠�����Œ�ŏo��
            return _oldModel;
        }
    }
}