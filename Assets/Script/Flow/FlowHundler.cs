using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class FlowHundler : IMainLoopHundler
    {
        [Inject] IFlowMasterDataDictionaryProvider _masterDataDictionaryProvider;
        [Inject] IFlowProvider _flowProvider;
        [Inject] IGlobalFlagProvider _globalFlagProvider;
        [Inject] IFlowSwitchable_Fake _flowSwitchable;

        IFlowModel _currentFlow;

        CompositeDisposable _reserveDisposable;
        CancellationTokenSource _cancellationTokenSource;

        Subject<Unit> _onFlowExited = new Subject<Unit> ();
        IObservable<Unit> OnFlowExited => _onFlowExited;

        public void EnterMainLoop()
        {
            SoundManager.PlayBGM("Main");

            //Fake
            _flowSwitchable.SwitchFlow.Subscribe(args =>  ReserveNextFlow(args,SwitchFlow));

            if (_globalFlagProvider.GetFlag(FlagConst.Key.IsSaveDataExist) == "False")
            {
                EnterFlowLoop(FlowMasterConst.FlowMasterLabel.MainFlow);
            }
            else
            {
                EnterFlowLoop(FlowMasterConst.FlowMasterLabel.SaveDataExistFlow);
            }
        }

#if ENABLE_DEBUG
        public void EnterTypingTestFlow()
        {
            SoundManager.PlayBGM("Main");
            EnterFlowLoop(FlowMasterConst.FlowMasterLabel.TypingTestFlow);
        }
        public void FreeInputTestFlow()
        {
            SoundManager.PlayBGM("Main");
            EnterFlowLoop(FlowMasterConst.FlowMasterLabel.FreeInputTestFlow);
        }
#endif

        public void SwitchFlow(FlowSwitchArgs_Fake _args)
        {
            _cancellationTokenSource.Cancel();

            //�{����_cancellationToken��Register�������������A
            if(_currentFlow != null)_currentFlow.ForceEndFlow();

            EnterFlowLoop(_args.FlowName, _args.InitialFlowId);
        }

        void EnterFlowLoop(FlowMasterConst.FlowMasterLabel flowName, string specificId = "")
        {
            _cancellationTokenSource = new CancellationTokenSource();
            FlowLoop(flowName, _cancellationTokenSource.Token, specificId).Forget();
        }

        async UniTask FlowLoop(FlowMasterConst.FlowMasterLabel flowName, CancellationToken cancellationToken, string specificId)
        {
            Log.Comment("�t���[���[�v�J�n");

            var _provider = _masterDataDictionaryProvider.GetProvider(flowName);


            int firstId = 0;

            if (specificId != "")
            {
                firstId = _provider.TryGetFromId(specificId).Index;
            }

            //�Ƃ肠�����ォ��ǂ�ł��� ��X��Condition������
            for (int i = firstId; i < _provider.Count && !cancellationToken.IsCancellationRequested; i++)
            {
                Log.Comment("�t���[�J�n");
                var master = _provider.TryGetFromIndex(i).GetMaster();
                _currentFlow = _flowProvider.GetFlowModel(EnumUtil.KeyToType<FlowConst.Category>(master.Category));
                await _currentFlow.EnterFlow(master.BodyId);

                if (!cancellationToken.IsCancellationRequested)
                {
                    _currentFlow = null;
                }
            }

            _onFlowExited.OnNext(Unit.Default);
            _onFlowExited.Dispose();
        }

        void ReserveNextFlow(FlowSwitchArgs_Fake args, Action<FlowSwitchArgs_Fake> action)
        {
            _reserveDisposable = new CompositeDisposable();
            _onFlowExited.Subscribe(_=> action.Invoke(args)).AddTo(_reserveDisposable);
        }


#if ENABLE_DEBUG
        public IFlowModel ForceGetCurrentFlow()
        {
            return _currentFlow;
        }
#endif
    }
}