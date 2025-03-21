using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using gaw241201.Model;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class UiDeletableProvider : IUiDeletableProvider
    {
        [Inject] DeleteClickInputUi _deleteClickInputUi;

        //FlowConstで呼ぶのが適切でなくなる可能性がある
        public IUiDeletable GetUiDeletable(FlowConst.Category category)
        {
            switch (category)
            {
                case FlowConst.Category.ClickInput: return _deleteClickInputUi;

                default:
                    Log.DebugAssert(category + "の挙動は未定義です");
                    return null;
            }
        }
    }
}