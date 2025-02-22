using Cysharp.Threading.Tasks;
using MessagePipe;
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
    public class AdvancedMenuModel : IUiMenuModel
    {

        UiMenuModel _menuModel;
        public int ItemIndex => _menuModel.ItemIndex;
        public int MaxItemRange => _menuModel.MaxItemRange;
        public bool IsEnable => _menuModel.IsEnable;

        public IObservable<int> FocusChanged =>_menuModel.FocusChanged;
        public IObservable<int> Decided => _menuModel.Decided;
        public IObservable<int> Entered => _menuModel.Entered;

        ISubscriber<FlagConst.Key, string> _subscriber;

        [Inject]
        public AdvancedMenuModel(AdvancedMenuItemListFactory factory, ISubscriber<FlagConst.Key, string> subscriber, IDisposablePure disposable)
        {
            _menuModel = new UiMenuModel(factory.CreateList());
            _subscriber = subscriber;
            _subscriber.Subscribe(FlagConst.Key.IsAdvancedSettingEnabled, OnFlagValueChanged).AddTo(disposable);
        }

        public void MoveFocus(int menuIndex) => _menuModel.MoveFocus(menuIndex);

        public void Decide() => _menuModel.Decide();

        public void Enter() => _menuModel.Enter();

        public void Exit() => _menuModel.Exit();

        Subject<bool> _settedEnable = new Subject<bool>();
        public IObservable<bool> SettedEnable => _settedEnable;


        string _value;

        void OnFlagValueChanged(string value)
        {
            if (_value != value)
            {
                _value = value;

                if(_value == Tarahiro.Const.c_true)
                {
                    if (!IsEnable)
                    {
                        SetEnable(true);
                    }
                }

                if(_value == Tarahiro.Const.c_false)
                {
                    if (IsEnable)
                    {
                        SetEnable(false);
                    }

                }
            }


        }

        void SetEnable(bool b)
        {
            _menuModel.SetEnable(b);
            _settedEnable.OnNext(b);

        }
    }
}