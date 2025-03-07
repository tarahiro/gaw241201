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
    public class FreeInputPlayerName : ISettingItemModelInputtable, IStringDecidable
    {
        //����̐Ӗ�
        //MenuItem�Ƃ��Ă�Enter���󂯎��Ӗ�
        //PlayerName�̕ύX���󂯎��Ӗ�
        //FreeInput�̌�����󂯎��Ӗ�


        [Inject] FreeInputUnfixedText _freeInputUnfixedText;
        [Inject] ISubscriber<FlagConst.Key, string> _subscriber;
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;
        [Inject] IDisposablePure _disposablePure;

        Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;
        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;

        Subject<string> _valueChanged = new Subject<string>();
        public IObservable<string> ValueChanged => _valueChanged;

        string _playerName = "Error";


        public void Initialize()
        {
            _subscriber.Subscribe(FlagConst.Key.Name, OnSetFlag).AddTo(_disposablePure);
        }

        public void  Enter()
        {
            Log.Comment("ProfileItemPlayerName��Enter");

            //Initializer��ʃN���X�ɕ����邩��
            _freeInputUnfixedText.Enter(_playerName);
            _entered.OnNext(Unit.Default);
        }

        public void End()
        {
            _freeInputUnfixedText.Exit();
            _exited.OnNext(Unit.Default);
        }

        public void OnSetFlag(string s)
        {
            Log.DebugLog("���b�Z�[�W�󂯎��:" + s);

            _playerName = s;
            _valueChanged.OnNext(s);
        }

        public void Decide(string text)
        {
            Log.DebugLog("ProfileItemPlayeName:Decide");
            _globalFlagRegisterer.RegisterFlag(FlagConst.Key.Name, text);
            End();
        }
    }
}