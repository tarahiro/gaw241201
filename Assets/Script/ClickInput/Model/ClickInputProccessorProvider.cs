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
    public class ClickInputProccessorProvider
    {
        [Inject] DoubleAffirmation _doubleAffirmation;
        public IClickInputProcessor Create(ClickInputConst.Key key)
        {
            switch (key)
            {
                case ClickInputConst.Key.DoubleAffirmation:
                    return _doubleAffirmation;

                default:
                    Log.DebugAssert(key + "�͖���`�ł�");
                    return null;
            }
        }
    }
}