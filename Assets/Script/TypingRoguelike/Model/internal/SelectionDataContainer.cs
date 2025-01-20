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
    public class SelectionDataContainer: ISelectionDataGettable, ISelectionDataSettable
    {
        List<ReplaceData> _selectionData;

        Subject<List<ReplaceData>> _selectionDataCreated = new Subject<List<ReplaceData>>();
        public IObservable<List<ReplaceData>> SelectionDataCreated => _selectionDataCreated;
        public void SetSelectionData(List<ReplaceData> selectionData)
        {
            _selectionData = selectionData;
            _selectionDataCreated.OnNext(selectionData);
        }
        public List<ReplaceData> GetSelectionData()
        {
            return _selectionData;
        }
    }
}