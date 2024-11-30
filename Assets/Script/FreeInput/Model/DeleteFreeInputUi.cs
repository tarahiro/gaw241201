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
    public class DeleteFreeInputUi : IUiDeletable
    {
        Subject<Unit> _uiDeleted = new Subject<Unit>();
        public IObservable<Unit> UiDeleted => _uiDeleted;

        public void DeleteUi()
        {
            Log.Comment("FreeInputUiÇÃè¡ãéäJén");
            _uiDeleted.OnNext(Unit.Default);
        }

    }
}