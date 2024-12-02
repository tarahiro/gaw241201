using Cysharp.Threading.Tasks;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public class ConversationViewArgsFactory
    {
        [Inject] MessageKeyHundler _messageKeyHundler;

        public ConversationViewArgs Create(ModelArgs<IConversationMaster> modelArgs)
        {
            return new ConversationViewArgs(_messageKeyHundler.HundleKey(modelArgs.Master.Message), EnumUtil.KeyToType<FacialConst.Key>(modelArgs.Master.Facial),modelArgs.CancellationToken);
        }
    }
}