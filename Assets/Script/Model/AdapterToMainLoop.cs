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
    public class AdapterToMainLoop : IAdapterManagerToModel
    {
        IMainLoopStarter _flowHundler;
        ILoadable _loadable;

        public AdapterToMainLoop(IMainLoopStarter mainLoopHundler, ILoadable loadable)
        {
            _flowHundler = mainLoopHundler;
            _loadable = loadable;
        }

        public async UniTask Enter()
        {
            Log.Comment("���f���J�n");

            Log.Comment("���[�h�J�n");
            _loadable.Load();

            Log.Comment("���C�����[�v�J�n");
            _flowHundler.EnterMainLoop();
        }
    }
}