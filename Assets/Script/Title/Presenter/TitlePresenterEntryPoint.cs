using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class TitlePresenterEntryPoint : IPostInitializable
    {
        [Inject] PresenterCoreFactoryTitle _factory;
        [Inject] DrumRollPresenterFactory _drumRollFactory;

        [Inject] TitlePresenter _presenter;

        public void PostInitialize()
        {
            _factory.Create();
            _drumRollFactory.Create();

            _presenter.Present();
        }
    }
}