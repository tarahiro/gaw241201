using Cysharp.Threading.Tasks;
using MessagePipe;
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
    public class DisposablePure : IDisposablePure
    {
        CompositeDisposable _disposable;

        [Inject] ISubscriber<ISceneUnit> _subscriber;

        public void Add(IDisposable item) => _disposable.Add(item);
        public bool Remove(IDisposable item) => _disposable.Remove(item);
        public bool Contains(IDisposable item) => _disposable.Contains(item);
        public void Clear() => _disposable.Clear();
        public void Dispose() 
        { 
            _disposable.Dispose();
        }
        public void CopyTo(IDisposable[] array, int arrayIndex) => _disposable.CopyTo(array, arrayIndex);
        public IEnumerator<IDisposable> GetEnumerator() => _disposable.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _disposable.GetEnumerator();
        public int Count => _disposable.Count;
        public bool IsReadOnly => _disposable.IsReadOnly;

        public DisposablePure(ISubscriber<ISceneUnit> subscriber)
        {
            _disposable = new CompositeDisposable();
            _subscriber = subscriber;
            _subscriber.Subscribe(_ => Dispose()).AddTo(_disposable);
        }

    }
}