using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace gaw241201.Switch.Model
{
    public class SwitchByTypedFlag : ICategoryEnterableModel
    {
        [Inject] FlowSwitchPublisher _publisher;
        [Inject] TypedFlagContainer _typedFlagContainer;
        [Inject] IGlobalFlagProvider _globalFlagProvider;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog(bodyId + "ŠJŽn");
            if(bodyId == "KillByName")
            {
                _typedFlagContainer.TryGetTypedName(out string name);

                if(name.ToLower() == _globalFlagProvider.GetFlag(FlagConst.Key.Name).ToLower())
                {
                    _publisher.Publish(new FlowSwitchArgs_Fake(FlowMasterConst.FlowMasterLabel.GameoverFlow, ""));
                }
                else
                {
                    _publisher.Publish(new FlowSwitchArgs_Fake(FlowMasterConst.FlowMasterLabel.ExhibitionForest2Flow, ""));
                }

                Log.DebugLog(name);
            }

            if (bodyId == "KillByEye")
            {
                _typedFlagContainer.TryGetHolder(out string name);

                if (name == "my")
                {
                    _publisher.Publish(new FlowSwitchArgs_Fake(FlowMasterConst.FlowMasterLabel.GameoverByEyeFlow, ""));
                }
                else
                {
                    _publisher.Publish(new FlowSwitchArgs_Fake(FlowMasterConst.FlowMasterLabel.TrueEndFlow, ""));
                }

                Log.DebugLog(name);
            }
        }

        public void ForceEndFlow()
        {
        }

    }
}
