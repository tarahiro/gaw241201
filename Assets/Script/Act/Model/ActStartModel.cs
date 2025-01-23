using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using System.Linq;
using gaw241201.Model;

namespace gaw241201
{
    public class ActStartModel : IFlowModel
    {
        [Inject] IGroupMasterGettable<IStageMaster> _groupMasterGettable;
        [Inject] IRestrictedCharProvider _restrictedCharProvider;
        [Inject] IRestrictionGenerator _restrictionGenerator;
        [Inject] IStageMasterRegisterable _stageMasterRegisterable;
        [Inject] IActMasterDataProvider _masterDataProvider;

        [Inject] ModelArgsFactory<IStageMasterRegisteredRestrictedCharList> _modelArgsFactory;

        Subject<List<ModelArgs<IStageMasterRegisteredRestrictedCharList>>> _waveInformationDecided = new Subject<List<ModelArgs<IStageMasterRegisteredRestrictedCharList>>>();
        Subject<ActBgViewArgs> _bgDecided = new Subject<ActBgViewArgs>();
        
        
        CancellationTokenSource _cts = new CancellationTokenSource();
        public IObservable<List<ModelArgs<IStageMasterRegisteredRestrictedCharList>>> WaveInformationDecided => _waveInformationDecided;
        public IObservable<ActBgViewArgs> BgDecided => _bgDecided;

        //UnitaskとSubjectの変換を使ってきれいにしたい
        bool _isEnded = false;

        public async UniTask EnterFlow(string bodyId)

        {   /*TextSequenceModel<T>との共通部分*/
            Log.Comment(bodyId + "のGroup開始");

            _cts = new CancellationTokenSource();
            IActMaster _master = _masterDataProvider.TryGetFromId(bodyId).GetMaster();
            List<IStageMaster> _thisGroup = _groupMasterGettable.GetGroupMaster(_master.StageGroupId);
            /*共通部分終わり*/

            Log.DebugLog(_thisGroup[1].Id);


            //ステージ毎に何を制限するか算出
            List<List<char>> list = new List<List<char>>();
            
            for(int i = 0; i < _thisGroup.Count; i++)
            {
               list.Add( _restrictionGenerator.GenerateRestriction(
                    i == 0 ? _restrictedCharProvider.GetRestrictedChar() : list[i - 1],
                    _thisGroup[i].AddedRestrictedCharIdList.ToList()
                    ));

            }

            List<List<char>> addableWholeList = new List<List<char>>();


            //それを元に、タイピングローグライク側へ登録
            for (int i = 0; i < _thisGroup.Count; i++)
            {
                List<char> addable = new List<char>();

                List<char> removable = i == 0 ? _restrictedCharProvider.GetRestrictedChar() : list[i - 1];
                foreach(var c in list[i])
                {
                    if (!removable.Contains(c)) addable.Add(c);
                }

                addableWholeList.Add(addable);
                _stageMasterRegisterable.RegisterStageMaster(_thisGroup[i].Id, addable);
            }


            //制限とWave数をViewに反映
            List<ModelArgs<IStageMasterRegisteredRestrictedCharList>> notifyList = new List<ModelArgs<IStageMasterRegisteredRestrictedCharList>>();

            for (int i = 0; i < _thisGroup.Count; i++) {

                notifyList.Add(_modelArgsFactory.Create(new StageMasterRegisteredRestrictedCharList(
                    _thisGroup[i].Index,
                    _thisGroup[i].Id,
                    _thisGroup[i].Group,
                    _thisGroup[i].WaveCount,
                    _thisGroup[i].AddedRestrictedCharIdList,
                    addableWholeList[i]),
                    _cts.Token
                    ));

            }

            _waveInformationDecided.OnNext(notifyList);


            //Bg情報取得
            _bgDecided.OnNext(new ActBgViewArgs(_master.BgId, _thisGroup.Sum(x => x.WaveCount), _cts.Token));

        }


        /*TextSequenceModel<T>との共通部分*/
        public void EndSingle()
        {
            Log.Comment("終了を検知");
            _isEnded = true;
        }

        public void ForceEndFlow()
        {
            _cts.Cancel();
        }
        /*共通部分終わり*/
    }
}