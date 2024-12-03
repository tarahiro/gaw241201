using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201.View
{
    public class ConfiscateView
    {
        [Inject] IRemovedable _removedable;
        [Inject] IRobbable _robbable;

        public void Confiscate(ConfiscateConst.Type type)
        {
            Log.Comment(type +"‚ÌView‚ÌConfiscateŠJŽn");

            _removedable.RemoveParts();
            _robbable.RobParts();
        }
    }
}