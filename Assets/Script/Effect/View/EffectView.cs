using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using UnityEditor;

namespace gaw241201.View
{
    public class EffectView : MonoBehaviour, IEffectViewEnterable, IEffectViewEndable
    {
        [Inject] EffectViewItemFactory _itemFactory;

        Dictionary<EffectConst.Key, IEffectItemView> _itemDictionary = new Dictionary<EffectConst.Key, IEffectItemView> ();
        const float c_waitTime = 1f;

        Subject<Unit> _enterExited = new Subject<Unit>();
        public IObservable<Unit> EnterExited => _enterExited;

        Subject<Unit> _endExited = new Subject<Unit>();
        public IObservable<Unit> EndExited => _endExited;

        public async UniTask Enter(EffectArgs args)
        {
            Log.DebugLog(args.Key + "‚ÌEffectViewŠJŽn");

            var item = _itemFactory.Create(args.Key,transform);
            args.CancellationToken.Register(() => _enterExited.OnNext(Unit.Default));
            _itemDictionary.Add(args.Key, item);

            await item.Enter(args.CancellationToken);

            if (item.IsAutoEnd)
            {
                await EndItem(args);
            }

            _enterExited.OnNext(Unit.Default);
        }

        public async UniTask End(EffectArgs args)
        {
            args.CancellationToken.Register(() => _endExited.OnNext(Unit.Default));
            await EndItem(args);

            _endExited.OnNext(Unit.Default);
        }


        async UniTask EndItem(EffectArgs args)
        {
            await _itemDictionary[args.Key].End(args.CancellationToken);
            Destroy(_itemDictionary[args.Key].transform.gameObject);
            _itemDictionary.Remove(args.Key);

        }

    }
}