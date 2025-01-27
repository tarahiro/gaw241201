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
    public class DeleteUiModel : ICategoryEnterableModel
    {
        [Inject] IUiDeletableProvider _uiDeletableProvider;
        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment("DeleteUiŠJŽn");
            _uiDeletableProvider.GetUiDeletable(EnumUtil.KeyToType<FlowConst.Category>(bodyId)).DeleteUi();
        }
        public void ForceEndFlow()
        {
        }
    }
}