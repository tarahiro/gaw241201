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
    public class FreeInputView : MonoBehaviour, IUiViewDeletable
    {
        [Inject] IGazable _gazable;

        FreeInputItemView _currentItem;

        const string c_prefix = "Prefab/FreeInput/";

        Subject<string> _exited = new Subject<string>();
        public IObservable<string> Exited => _exited;
        

        public async UniTask Enter(FreeInputArgs args)
        {
            Log.Comment(c_prefix + args.BodyId + "ÇÃprefabê∂ê¨");
            _currentItem = Instantiate(Resources.Load<FreeInputItemView>(c_prefix + args.BodyId), transform);
            _currentItem.Construct(_gazable);

            string value = "UnRegistered";
            _currentItem.Exited.Subscribe(x => value = x);
            args.CancellationToken.Register(() => OnExit(value));

            await _currentItem.Enter(args.CancellationToken);

            OnExit(value);

        }

        void OnExit(string value)
        {
            _exited.OnNext(value);
        }

        public void Delete()
        {
            Log.Comment("FreeInputViewItemÇçÌèú");
            //êFÅXdisposeÇµÇ»Ç¢Ç∆Ç¢ÇØÇ»Ç¢ãCÇ‡Ç∑ÇÈ
            Destroy(_currentItem.gameObject);
        }
    }
}