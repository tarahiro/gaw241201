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
    public class EyesView : MonoBehaviour
    {
        const float c_length = .2f;

        [SerializeField] List<EyeView> _eyeViewList;

        public void SetEye(string directionKey)
        {
            Vector2 direction;

            switch (directionKey)
            {
                case "Center":
                    direction = Vector2.zero;
                    break;

                case "Top":
                    direction = Vector2.up;
                    break;

                case "RightTop":
                    direction = Vector2.up + Vector2.right;
                    break;

                case "Right":
                    direction = Vector2.right;
                    break;

                case "RightBottom":
                    direction = Vector2.down + Vector2.right;
                    break;

                case "Bottom":
                    direction = Vector2.down;
                    break;

                case "LeftBottom":
                    direction = Vector2.left + Vector2.down;
                    break;

                case "Left":
                    direction = Vector2.left;
                    break;

                case "LeftTop":
                    direction = Vector2.left + Vector2.up;
                    break;

                default:
                    Log.DebugAssert(directionKey + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                    direction = Vector2.zero;
                    break;

            }

            var position = direction.normalized * c_length;

            foreach (var item in _eyeViewList)
            {
                item.SetEyePosition(position);
            }

        }
    }
}