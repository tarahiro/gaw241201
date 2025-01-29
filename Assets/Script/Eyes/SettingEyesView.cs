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
    public class SettingEyesView : MonoBehaviour, IEyePositionChangable, IFacialChangable, IGazable, IChangableEye, ISettingOrnament
    {
        const float c_length = .2f;

        [SerializeField] List<SettingEyeView> _eyeViewList;
        [SerializeField] Animator _animator;

        public void SetEffect(ConversationViewConst.EyePosition facialKey)
        {
            Vector2 direction;

            switch (facialKey)
            {
                case ConversationViewConst.EyePosition.Center:
                    direction = Vector2.zero;
                    break;

                case ConversationViewConst.EyePosition.Top:
                    direction = Vector2.up;
                    break;

                case ConversationViewConst.EyePosition.RightTop:
                    direction = Vector2.up + Vector2.right;
                    break;

                case ConversationViewConst.EyePosition.Right:
                    direction = Vector2.right;
                    break;

                case ConversationViewConst.EyePosition.RightBottom:
                    direction = Vector2.down + Vector2.right;
                    break;

                case ConversationViewConst.EyePosition.Bottom:
                    direction = Vector2.down;
                    break;

                case ConversationViewConst.EyePosition.LeftBottom:
                    direction = Vector2.left + Vector2.down;
                    break;

                case ConversationViewConst.EyePosition.Left:
                    direction = Vector2.left;
                    break;

                case ConversationViewConst.EyePosition.LeftTop:
                    direction = Vector2.left + Vector2.up;
                    break;

                default:
                    Log.DebugAssert(facialKey + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                    direction = Vector2.zero;
                    break;

            }

            SetEyePosition(direction);
        }
        public void SetEffect(ConversationViewConst.Facial facialKey)
        {
            switch (facialKey)
            {
                case ConversationViewConst.Facial.Mad:
                    SetMad();
                    break;

                case ConversationViewConst.Facial.Big:
                    foreach (var eye in _eyeViewList)
                    {
                        eye.SetScale(3f);
                    }
                    break;

            }
        }

        public void ResetEffect(ConversationViewConst.EyePosition position)
        {

        }
        public void ResetEffect(ConversationViewConst.Facial facialKey)
        {
            switch (facialKey)
            {
                case ConversationViewConst.Facial.Mad:
                    SetMad();
                    break;

                case ConversationViewConst.Facial.Big:
                    foreach (var eye in _eyeViewList)
                    {
                        eye.SetScale(1f);
                    }
                    break;

            }
        }

        public void Gaze(Vector2 screenPosition)
        {
            Vector2 direction = screenPosition - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
            SetEyePosition(direction);
        }

        void SetEyePosition(Vector2 direction)
        {
            var position = direction.normalized * c_length;

            foreach (var item in _eyeViewList)
            {
                item.SetEyePosition(position);
            }

        }

        public void ChangeParts(EffectConst.EyeParts parts, EffectConst.WhichEye whichEye)
        {
            switch (whichEye)
            {
                case EffectConst.WhichEye.Both:
                    _eyeViewList[(int)EyeLR.Left].SetEyeParts(parts);
                    _eyeViewList[(int)EyeLR.Right].SetEyeParts(parts);
                    break;

                case EffectConst.WhichEye.Left:
                    _eyeViewList[(int)EyeLR.Left].SetEyeParts(parts);
                    break;

                case EffectConst.WhichEye.Right:
                    _eyeViewList[(int)EyeLR.Right].SetEyeParts(parts);
                    break;
            }

        }

        public void SetNormal()
        {
            _animator.SetTrigger("Normal");
        }

        public void SetMad()
        {
            _animator.SetTrigger("Mad");
        }

        enum EyeLR
        {
            Right,
            Left,
        }
    }
}