using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.View.InputConst;


namespace gaw241201.View
{
    public class InputExecutorCommand : IInputExecutor
    {
        IInputHundlerCommand _hundler;

        Command _command = Command.None;

        Subject<Unit> _inputted = new Subject<Unit>();
        public IObservable<Unit> Inputted => _inputted;

        [Inject]
        public InputExecutorCommand(IInputHundlerCommand hundler)
        {
            _hundler = hundler;
        }

        public void Initialize(Command command)
        {
            _command = command;
        }

        public void TryExecute()
        {
            if(_command == Command.None)
            {
                Log.DebugAssert("�L���ȃR�}���h���ݒ肳��Ă��܂���");
            }

            //�������͂���������
            if (_hundler.IsInputtedCommand(_command))
            {
                _inputted.OnNext(Unit.Default);

                //�ʒm���΂�
                _hundler.NotifyUse(_command);
            }
        }
    }
}