using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class FreeInputModel : ICategoryEnterableModel
    {
        [Inject] IFreeInputSwithcerModel _switcherModel;

        bool _isEnded = false;
        string _bodyId;
        [Inject] ICancellationTokenPure _cts;
        Subject<FlowArgs> _entered  = new Subject<FlowArgs>();

        public IObservable<FlowArgs> Entered => _entered;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "‚ÌFreeInputŠJŽn");
            _cts.SetNew();
            _bodyId = bodyId;
            _isEnded = false;

            var model = _switcherModel.GetGateModel(EnumUtil.KeyToType<FreeInputConst.FreeInputCategory>(bodyId));
            model.Enter();
            RegisterCanceled(_cts.Token, model);

            await UniTask.WaitUntil(() =>  _isEnded);
        }

        public void EndFlow()
        { 
            _isEnded = true;

        }

        public void ForceEndFlow()
        {
            _cts.Cancel();
        }

        
#if ENABLE_DEBUG
        void RegisterCanceled(CancellationToken ct, IFreeInputGateFlowModel _model )
        {
            _cts.Token.Register(() => _model.ForceDecide());

        }
#endif
        
    }
}