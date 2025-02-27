using Cysharp.Threading.Tasks;
using MessagePipe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class SkillMenuView : MonoBehaviour, IMenuView 
    {
        [Inject] IObjectResolver _diContainer;
        [SerializeField] SkillItemView _itemViewPrefab;
        [SerializeField] Transform _root;

        List<SkillItemView> _itemViewList = new List<SkillItemView>();
        const float c_intervalX = 540f;

        [Inject] ISubscriber<string> _animationSubscriberFake;

        int _index = 0;

        float[] _fakeInitialRotation = new float[]
        {
            -4f,6f,3f
        };

        public void SetData(SkillArgs.Data data)
        {

            SkillItemView item = _diContainer.Instantiate<SkillItemView>(_itemViewPrefab, _root);
            item.SetData(data);
            item.transform.localPosition = Vector2.right * (-1f + _itemViewList.Count ) * c_intervalX;
            item.transform.localRotation = Quaternion.Euler(0, 0, _fakeInitialRotation[_itemViewList.Count]);

            _itemViewList.Add(item);

        }

        public async UniTask Enter(int index)
        {
            _index = index;
            await SetFocus(_index);
        }

        public async UniTask Exit()
        {
            for (int i = 0; i < _itemViewList.Count; i++)
            {
                Destroy(_itemViewList[i].gameObject);
            }

            _itemViewList = new List<SkillItemView>();
        }

        public IMenuItemView Current()
        {
            return _itemViewList[_index];
        }

        public int CurrentIndex => _index;

        public async UniTask SetFocus(int index)
        {
            Log.Comment("SkillMenuView: SetFocus");

            Current().UnFocus();
            _index = index;
            Current().Focus();
        }

        public async UniTask Decide(int index)
        {

        }

        void HorrorFake()
        {
            foreach(var item in _itemViewList)
            {
                item.transform.localRotation = Quaternion.identity;
            }
        }
    }
}