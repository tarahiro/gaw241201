using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using System.Threading;

namespace gaw241201
{
    public class StartMonitorFlowModel : ICategoryEnterableModel
    {
        [Inject] MonitorModel _monitorModel; 

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "��StartMonitor�J�n");
            _monitorModel.StartMonitor(bodyId);
        }
        public void ForceEndFlow()
        {
            
        }
    }
}