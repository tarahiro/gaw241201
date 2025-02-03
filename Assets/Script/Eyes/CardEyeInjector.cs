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

namespace gaw241201.View
{
    public class CardEyeInjector : MonoBehaviour
    {
        ISubscriber<GazeConst.GazingKey, Vector2> _subscriber;
        ISubscriber<GazeConst.GazingKey, ConversationViewConst.Facial> _facialSubscriber;
         ISubscriber<GazeConst.GazingKey, Unit> _gazeResetSubscriber;

        [Inject]
        public void Construct(ISubscriber<GazeConst.GazingKey, Vector2> subscriber, ISubscriber<GazeConst.GazingKey, ConversationViewConst.Facial> facialSubscriber, ISubscriber<GazeConst.GazingKey, Unit> gazeResetSubscriber)
        {
            _subscriber = subscriber;
            _facialSubscriber = facialSubscriber;
            _gazeResetSubscriber = gazeResetSubscriber;

        }

        public void Start()
        {
            CardEyes _cardEyes = Util.GetComponentInChildrenRecursive<CardEyes>(transform);
            if (_cardEyes != null)
            {
                Log.DebugLog("CardEyes‚ÖInject");
                _cardEyes.Construct(_subscriber,_facialSubscriber,_gazeResetSubscriber);
            }
        }
    }
}