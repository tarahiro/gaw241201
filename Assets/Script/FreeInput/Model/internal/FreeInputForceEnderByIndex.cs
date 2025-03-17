using Cysharp.Threading.Tasks;
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
    public class FreeInputForceEnderByIndex 
    {
        int _prevIndex;
        int _index;

        IFreeInputCharHundler _freeInputCharHundler;
        int _indexCriteria;

        public FreeInputForceEnderByIndex(IFreeInputCharHundler freeInputCharHundler, int indexCriteria)
        {
            _freeInputCharHundler = freeInputCharHundler;
            _indexCriteria = indexCriteria;
        }

        public void OnUpdateIndex(int index)
        {
            _prevIndex = _index;
            _index = index;

            if(_index >= _indexCriteria && _prevIndex < _indexCriteria)
            {
                _freeInputCharHundler.ForceEnd();
            }
        }

        public void OnExit()
        {
            _prevIndex = 0;
            _index = 0;
        }
    }
}