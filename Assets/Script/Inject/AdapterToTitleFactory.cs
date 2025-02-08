using Cysharp.Threading.Tasks;
using PlasticGui.WorkspaceWindow.Replication;
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
    public class AdapterToTitleFactory : IAdapterFactory
    {
        [Inject] TitleEnterModel _titleEnterModel;
        public IAdapterManagerToModel CreateAdapter()
        {
            return _titleEnterModel;
        }
    }
}