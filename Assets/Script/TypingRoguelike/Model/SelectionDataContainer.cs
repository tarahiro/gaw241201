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
        List<SelectionData> _selectionData;

        Subject<List<SelectionData>> _selectionDataCreated = new Subject<List<SelectionData>>();
        public IObservable<List<SelectionData>> SelectionDataCreated => _selectionDataCreated;
        public void SetSelectionData(List<SelectionData> selectionData)
        {
            _selectionData = selectionData;
            _selectionDataCreated.OnNext(selectionData);
        }
        public List<SelectionData> GetSelectionData()
        {
            return _selectionData;
        }
    }
}