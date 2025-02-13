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
    public class StartHighlightFlowModel : ICategoryEnterableModel
    {
        [Inject] SettingEnterMonitorHighlightModel _highlightModel;

        public async UniTask EnterFlow(string bodyId)
        {
            //Provider‚ÉˆÚ‚µ‚Ä‚à‚¢‚¢

            Log.Comment(bodyId + "‚ÌStartMonitorŠJŽn");
            if(bodyId == "Setting")
            {
                _highlightModel.Highlight();
            }
        }

        public void ForceEndFlow()
        {

        }
    }
}