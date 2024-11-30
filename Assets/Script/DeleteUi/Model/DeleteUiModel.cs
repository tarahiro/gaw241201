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
    public class DeleteUiModel : IFlowModel
    {
        [Inject] IUiDeletableProvider _uiDeletableProvider;
        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment("DeleteUiŠJŽn");
            _uiDeletableProvider.GetUiDeletable(EnumUtil.KeyToType<FlowConst.Category>(bodyId)).DeleteUi();
        }
#if ENABLE_DEBUG
        public void ForceEndFlow()
        {
        }
        public string ForceGetCategory => "DeleteUi";
#endif
    }
}