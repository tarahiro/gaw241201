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
    public class EyesView : MonoBehaviour, IFacialChangable, IGazable, IRobbable
    {
        const float c_length = .2f;

        [SerializeField] List<EyeView> _eyeViewList;
        [SerializeField] GameObject _lefteyeRobbedParts;


        public void SetFacial(FacialConst.Key facialKey)
        {
            Vector2 direction;

            switch (facialKey)
            {
                case FacialConst.Key.Center:
                    direction = Vector2.zero;
                    break;

                case FacialConst.Key.Top:
                    direction = Vector2.up;
                    break;

                case FacialConst.Key.RightTop:
                    direction = Vector2.up + Vector2.right;
                    break;

                case FacialConst.Key.Right:
                    direction = Vector2.right;
                    break;

                case FacialConst.Key.RightBottom:
                    direction = Vector2.down + Vector2.right;
                    break;

                case FacialConst.Key.Bottom:
                    direction = Vector2.down;
                    break;

                case FacialConst.Key.LeftBottom:
                    direction = Vector2.left + Vector2.down;
                    break;

                case FacialConst.Key.Left:
                    direction = Vector2.left;
                    break;

                case FacialConst.Key.LeftTop:
                    direction = Vector2.left + Vector2.up;
                    break;

                default:
                    Log.DebugAssert(facialKey + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                    direction = Vector2.zero;
                    break;

            }

            SetEyePosition(direction);


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

        public void RobParts()
        {
            _lefteyeRobbedParts.SetActive(true);
        }
    }
}