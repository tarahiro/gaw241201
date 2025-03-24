using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.UI;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class UiMenuInputProcessorFactory : IUiMenuInputProcessorFactory
    {
        [Inject] InputExecutorCommand _decide;
        [Inject] InputExecutorDiscreteDirectionVertical _vertical;
        [Inject] IDisposablePure _disposable;


        public UiMenuInputProcessor Create(MenuView menuView)
        {
            return new UiMenuInputProcessor(new IndexVariantHundlerUiMenu(menuView), _decide, _vertical, _disposable);
        }
    }
}