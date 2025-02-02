using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class EyeMoveView : MonoBehaviour, IEffectItemView
    {
        public bool IsAutoEnd => true;

        IPositionChangable _eyePositionChangable;
        IPositionChangable _conversationPositionChangable;
        
        EyePositionKey _key;

        public EyeMoveView Construct(IPositionChangable eyePositionChangable,IPositionChangable conversationPositionChangable, EyePositionKey key)
        {
            _eyePositionChangable = eyePositionChangable;
            _conversationPositionChangable = conversationPositionChangable;
            _key = key;

            return this;
        }

        public async UniTask Enter(CancellationToken cancellationToken)
        {
            Log.Comment("SetGoatEyeView; Enter");
            await UniTask.WhenAll(_eyePositionChangable.ChangePosition(_key), _conversationPositionChangable.ChangePosition(_key));
        }

        public async UniTask End(CancellationToken cancellationToken)
        {
        }

        public enum EyePositionKey
        {
            Up,
            MiddleUp,
            Middle,
            MiddleDown,
            Down,
        }
    }
}