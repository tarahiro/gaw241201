using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class ConversationModel : IFlowModel {

        [Inject] IConversationMasterDataProvider _masterDataProvider;

        Subject<IConversationMaster> _entered = new Subject<IConversationMaster>();
        public IObservable<IConversationMaster> Entered => _entered;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "‚ÌConversationŠJŽn");

            _entered.OnNext(_masterDataProvider.TryGetFromId(bodyId).GetMaster());
        }
    }
}