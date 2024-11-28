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
    public class GameManager : IPostStartable
    {
        [Inject] IAdapterManagerToModel _adapter;
        public void PostStart()
        {
            Log.Comment("�Q�[���J�n");

            _adapter.Enter().Forget();
        }
    }
}