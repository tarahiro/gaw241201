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

namespace gaw241201
{
    public class EffectViewItemFactory
    {
        public const string c_pathPrefix = "Prefab/EffectViewItem/";

        [Inject] IRemovedable _removedable;
        [Inject] IChangableEye _robbable;
        [Inject] EyesPositionChangable _eyePositionChangable;
        [Inject] ConversationTextPositionChangable _conversationTextPositionChangable;

        [Inject] ChangeEyeViewFactory _changeEyeViewFactory;
        [Inject] ConfiscateViewFactory _confiscateViewFactory;

        public IEffectItemView Create(EffectConst.Key key, Transform parent)
        {
            switch (key)
            {
                case EffectConst.Key.ConfiscateLeftEye:
                case EffectConst.Key.ConfiscateBothEye:
                    return _confiscateViewFactory.Create(key, parent);

                case EffectConst.Key.SetGoatEye:
                case EffectConst.Key.SetNormalEye:
                    return _changeEyeViewFactory.Create(key, parent);

                case EffectConst.Key.ChangeEyesPositionToMiddleUp:
                    return GetInstance<EyeMoveView>("ChangeEyesPosition", parent)
                        .Construct(_eyePositionChangable,_conversationTextPositionChangable, EyeMoveView.EyePositionKey.MiddleUp);

                case EffectConst.Key.ChangeEyesPositionToMiddleDown:
                    return GetInstance<EyeMoveView>("ChangeEyesPosition", parent)
                        .Construct(_eyePositionChangable, _conversationTextPositionChangable, EyeMoveView.EyePositionKey.MiddleDown);


                default:
                    return GameObject.Instantiate<GameObject>(ResourceUtil.GetResource<GameObject>(c_pathPrefix + key), parent).GetComponent<IEffectItemView>();
            }
        }

        T GetInstance<T>(string key, Transform parent) where T : MonoBehaviour
        {
            return GameObject.Instantiate<T>(ResourceUtil.GetResource<T>(c_pathPrefix + key), parent);
        }
    }
}