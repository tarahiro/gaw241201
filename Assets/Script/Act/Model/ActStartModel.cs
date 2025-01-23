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

        //Unitask��Subject�̕ϊ����g���Ă��ꂢ�ɂ�����
        bool _isEnded = false;

        public async UniTask EnterFlow(string bodyId)

        {   /*TextSequenceModel<T>�Ƃ̋��ʕ���*/
            Log.Comment(bodyId + "��Group�J�n");

            _cts = new CancellationTokenSource();
            IActMaster _master = _masterDataProvider.TryGetFromId(bodyId).GetMaster();
            List<IStageMaster> _thisGroup = _groupMasterGettable.GetGroupMaster(_master.StageGroupId);
            /*���ʕ����I���*/

            Log.DebugLog(_thisGroup[1].Id);


            //�X�e�[�W���ɉ��𐧌����邩�Z�o
            List<List<char>> list = new List<List<char>>();
            
            for(int i = 0; i < _thisGroup.Count; i++)
            {
               list.Add( _restrictionGenerator.GenerateRestriction(
                    i == 0 ? _restrictedCharProvider.GetRestrictedChar() : list[i - 1],
                    _thisGroup[i].AddedRestrictedCharIdList.ToList()
                    ));

            }

            List<List<char>> addableWholeList = new List<List<char>>();


            //��������ɁA�^�C�s���O���[�O���C�N���֓o�^
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


            //������Wave����View�ɔ��f
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


            //Bg���擾
            _bgDecided.OnNext(new ActBgViewArgs(_master.BgId, _thisGroup.Sum(x => x.WaveCount), _cts.Token));

        }


        /*TextSequenceModel<T>�Ƃ̋��ʕ���*/
        public void EndSingle()
        {
            Log.Comment("�I�������m");
            _isEnded = true;
        }

        public void ForceEndFlow()
        {
            _cts.Cancel();
        }
        /*���ʕ����I���*/
    }
}