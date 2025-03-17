using Cysharp.Threading.Tasks;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public interface IFreeInputPresenterCore
    { 
        public void ActivatePresenter();

        FreeInputIndexer FreeInputIndexer { get; }
        FreeInputUnfixedText FreeInputUnfixedText { get; }
        IFreeInputCharHundler FreeInputCharHundler { get; }
        IFreeInputGateModel FreeInputGateModel { get; }

        //View
        IFreeInputDisplayView FreeInputTextDisplayView { get; }
        IFreeInputProcessor FreeInputProcessor { get; }
        FreeInputEntererView FreeInputEntererView { get; }

        IDisposablePure Disposable { get; }
    }
}