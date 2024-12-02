using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201.View
{
    public class TypingView : MonoBehaviour
    {
        TypingItemView _currentItem;
        const string c_prefabPath = "Prefab/Typing/TypingItemView";

        public async UniTask Enter(TypingViewArgs args)
        {
            Log.Comment(args.JpText + "ŠJŽn");
            _currentItem = Instantiate(ResourceUtil.GetResource<TypingItemView>(c_prefabPath), transform);
            _currentItem.Construct(args);

            await _currentItem.Enter();
        }
    }
}