using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using log4net.Util;
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
    public class ConversationTextPositionChangable : MonoBehaviour, IPositionChangable

    {
        const float c_moveTime = .5f;

        Vector3 offset = new Vector3(0, 0, 0f);

        public async UniTask ChangePosition(EyeMoveView.EyePositionKey key)
        {
            await LMotion.Create(transform.localPosition, TargetPosition(key), c_moveTime).BindToLocalPosition(transform);
        }

        public Vector3 TargetPosition(EyeMoveView.EyePositionKey key)
        {
            switch (key)
            {
                case EyeMoveView.EyePositionKey.Up:
                    return Vector3.up * 440f + offset;

                case EyeMoveView.EyePositionKey.MiddleUp:
                    return Vector3.up * 440f + offset;

                case EyeMoveView.EyePositionKey.Middle:
                    return Vector3.up * 310f + offset;

                case EyeMoveView.EyePositionKey.MiddleDown:
                    return Vector3.up * 130f + offset;

                case EyeMoveView.EyePositionKey.Down:
                    return Vector3.up * 130f + offset;

                default:
                    Log.DebugAssert("ïsê≥Ç»ílÇ≈Ç∑:" + key);
                    return Vector3.zero;
            }
        }
    }
}