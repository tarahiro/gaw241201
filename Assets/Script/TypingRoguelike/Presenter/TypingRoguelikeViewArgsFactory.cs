using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class TypingRoguelikeViewArgsFactory
    {
        [Inject] MessageKeyHundler _messageKeyHundler;

        public TypingViewArgs Create(ModelArgs<ITypingRoguelikeSingleSequenceMaster> modelArgs)
        {
            return new TypingViewArgs(_messageKeyHundler.HundleKey(modelArgs.Master.JpText), 
                _messageKeyHundler.HundleKey(modelArgs.Master.RomanText), modelArgs.CancellationToken);
        }
    }
}