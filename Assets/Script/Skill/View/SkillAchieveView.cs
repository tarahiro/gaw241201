using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201.View
{
    public class SkillAchieveView : MonoBehaviour
    {
        [SerializeField] SkillItemView _itemViewPrefab;
        [SerializeField] Transform _root;

        const float c_intervalX = 480f;

        CompositeDisposable _disposable;

        List<SkillItemView> _itemViewList;

        bool isLoop = true;

        Subject<SkillArgs.Data> _ended = new Subject<SkillArgs.Data>();
        public IObservable<SkillArgs.Data> Ended => _ended;

        public async UniTask Enter(SkillArgs args)
        {
            _disposable = new CompositeDisposable();
            _itemViewList = new List<SkillItemView>();

            isLoop = true;

            for(int i = 0; i < args.DataList.Count; i++)
            {
                SkillItemView item = Instantiate(_itemViewPrefab, _root);
                item.SetData(args.DataList[i]);
                item.transform.localPosition = Vector2.right * (-1f + i) * c_intervalX;
                item.Decided.Subscribe(OnDecide).AddTo(_disposable);

                _itemViewList.Add(item);
            }

            while(isLoop && !args.CancellationToken.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _itemViewList[0].Decide();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _itemViewList[1].Decide();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _itemViewList[2].Decide();
                }
            }
        }

        public void OnDecide(SkillArgs.Data id)
        {
            _ended.OnNext(id);
            _disposable.Dispose();
            for(int i = 0; i < _itemViewList.Count; i++)
            {
                Destroy(_itemViewList[i].gameObject);
            }
        }

    }
}