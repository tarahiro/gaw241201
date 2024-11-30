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
    public class UiDeletableProvider : IUiDeletableProvider
    {
        [Inject] DeleteFreeInputUi _deleteFreeInputUi;

        //FlowConst�ŌĂԂ̂��K�؂łȂ��Ȃ�\��������
        public IUiDeletable GetUiDeletable(FlowConst.Category category)
        {
            switch (category)
            {
                case FlowConst.Category.FreeInput:
                    return _deleteFreeInputUi;

                default:
                    Log.DebugAssert(category + "�̋����͖���`�ł�");
                    return null;
            }
        }
    }
}