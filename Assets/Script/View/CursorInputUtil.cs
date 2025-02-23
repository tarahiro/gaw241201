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
    public static class CursorInputUtil
    {
        public static Vector2Int GetCursorDirection(KeyCode key)
        {
            switch (key)
            {
                case KeyCode.RightArrow:
                    return new Vector2Int(1, 0);
                case KeyCode.LeftArrow:
                    return new Vector2Int(-1, 0);
                case KeyCode.UpArrow:
                    return new Vector2Int(0, 1);
                case KeyCode.DownArrow:
                    return new Vector2Int(0, -1);
                default:
                    return new Vector2Int(0, 0);
            }
        }
    }
}