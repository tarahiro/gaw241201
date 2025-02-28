using Cysharp.Threading.Tasks;
using gaw241201.Model;
using MessagePipe;
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
    public class FlowHundler : IFlowHundler
    {
        IFlowMasterDataDictionaryProvider _masterDataDictionaryProvider;
         IFlowProvider _flowProvider;
        IGlobalFlagProvider _globalFlagProvider;
        ISubscriber<FlowSwitchArgs> _subject;

        ICategoryEnterableModel _currentFlow;

        CompositeDisposable _reserveDisposable;
        ICancellationTokenPure _cancellationTokenSource;

        [Inject]
        public FlowHundler(IFlowMasterDataDictionaryProvider flowMasterDataDictionaryProvider,
            IFlowProvider flowProvider,
            IGlobalFlagProvider globalFlagProvider,
            ISubscriber<FlowSwitchArgs> subject,
            IDisposablePure disposables,
            ICancellationTokenPure cancellationTokenPure)
        {
            _masterDataDictionaryProvider = flowMasterDataDictionaryProvider;
            _flowProvider = flowProvider;
            _globalFlagProvider = globalFlagProvider;
            _subject = subject;
            _cancellationTokenSource = cancellationTokenPure;

            _subject.Subscribe(SwitchFlow).AddTo(disposables);
        }


        public void Enter()
        {
        }


        public void SwitchFlow(FlowSwitchArgs _args)
        {
            _cancellationTokenSource.Cancel();

            //本当は_cancellationTokenにRegisterしたかったが、
            if(_currentFlow != null)_currentFlow.ForceEndFlow();

            EnterFlowLoop(_args.FlowName, _args.InitialFlowId);
        }

        public void EnterFlowLoop(FlowMasterConst.FlowMasterLabel flowName, string specificId = "")
        {
            _cancellationTokenSource.SetNew();
            FlowLoop(flowName, _cancellationTokenSource.Token, specificId).Forget();
        }

        async UniTask FlowLoop(FlowMasterConst.FlowMasterLabel flowName, CancellationToken cancellationToken, string specificId)
        {
            Log.Comment("フローループ開始");

            var _provider = _masterDataDictionaryProvider.GetProvider(flowName);


            int firstId = 0;

            if (specificId != "")
            {
                firstId = _provider.TryGetFromId(specificId).Index;
            }

            //とりあえず上から読んでいく 後々はConditionを見る
            for (int i = firstId; i < _provider.Count && !cancellationToken.IsCancellationRequested; i++)
            {
                Log.Comment("フロー開始");
                var master = _provider.TryGetFromIndex(i).GetMaster();
                _currentFlow = _flowProvider.GetFlowModel(EnumUtil.KeyToType<FlowConst.Category>(master.Category));
                await _currentFlow.EnterFlow(master.BodyId);

                if (!cancellationToken.IsCancellationRequested)
                {
                    _currentFlow = null;
                }
            }

        }

#if ENABLE_DEBUG
        public ICategoryEnterableModel ForceGetCurrentFlow()
        {
            return _currentFlow;
        }
#endif
    }
}