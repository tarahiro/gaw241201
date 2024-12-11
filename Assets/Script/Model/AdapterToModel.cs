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
    public class AdapterToModel : IAdapterManagerToModel
    {
        [Inject] IMainLoopHundler _flowHundler;
        [Inject] ILoadable _loadable;

        public async UniTask Enter()
        {
            Log.Comment("���f���J�n");

            Log.Comment("���[�h�J�n");
            _loadable.Load();

            Log.Comment("���C�����[�v�J�n");
            _flowHundler.EnterMainLoop();
            //_flowHundler.EnterTypingTestFlow();
            //_flowHundler.FreeInputTestFlow();
        }
    }
}