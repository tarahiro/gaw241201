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
        [Inject] Skippable _skippable;
        public IClickInputProcessor Create(ClickInputConst.Key key)
        {
            switch (key)
            {
                case ClickInputConst.Key.DoubleAffirmation:
                    return _doubleAffirmation;

                case ClickInputConst.Key.Skippable:
                    return _skippable;

                default:
                    Log.DebugAssert(key + "‚Í–¢’è‹`‚Å‚·");
                    return null;
            }
        }
    }
}