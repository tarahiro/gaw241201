using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class ImpressionView : IImpressionChangable
    {
        public void SetEffect(ConversationViewConst.Impression key)
        {
            switch (key)
            {
                case ConversationViewConst.Impression.Silent:
                    SoundManager.StopBGM(0);
                    break;
            }
        }
        public void ResetEffect(ConversationViewConst.Impression key)
        {
            switch (key)
            {
                case ConversationViewConst.Impression.Silent:
                    SoundManager.PlayBGM("Main");
                    break;
            }

        }
    }
}