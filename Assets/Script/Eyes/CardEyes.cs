using Cysharp.Threading.Tasks;
using MessagePipe;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class CardEyes : MonoBehaviour
    {
        [SerializeField] float _eyeMoveLength = 6f;
        public GazeConst.GazingKey GazingKey { get; set; } = GazeConst.GazingKey.Card;

        [SerializeField] List<EyeView> _eyeViewList;
        ISubscriber<GazeConst.GazingKey, Vector2> _subscriber;
        ISubscriber<GazeConst.GazingKey, ConversationViewConst.Facial> _facialSubscriber;
        ISubscriber<GazeConst.GazingKey, Unit> _gazeResetSubscriber;

       


        public void Construct(ISubscriber<GazeConst.GazingKey, Vector2> subscriber, ISubscriber<GazeConst.GazingKey, ConversationViewConst.Facial> facialSubscriber, ISubscriber<GazeConst.GazingKey, Unit> gazeResetSubscriber)
        {
            _subscriber = subscriber;
            _facialSubscriber = facialSubscriber;
            _gazeResetSubscriber = gazeResetSubscriber;
        }

        void Start()
        {
            _subscriber.Subscribe(GazingKey, Gaze).AddTo(this);
            _facialSubscriber.Subscribe(GazingKey, SetEffect).AddTo(this);
            _gazeResetSubscriber.Subscribe(GazingKey, _ => ResetGaze()).AddTo(this);

        }

        public void Gaze(Vector2 screenPosition)
        {
            Log.Comment("CardEyes: Gaze");
            Vector2 direction = screenPosition - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
            SetEyePosition(direction);
        }
        public void ResetGaze()
        {
            SetEyePosition(Vector2.zero);
        }

        void SetEyePosition(Vector2 direction)
        {
            var position = direction.normalized * _eyeMoveLength;

            foreach (var item in _eyeViewList)
            {
                item.SetEyePosition(position);
            }

        }
        public void SetEffect(ConversationViewConst.Facial facialKey)
        {
            switch (facialKey)
            {
                case ConversationViewConst.Facial.Mad:
                    foreach (var eye in _eyeViewList)
                    {
                        eye.SetRotation(new Vector3(0, 0, 90f));
                    }
                    break;

                case ConversationViewConst.Facial.Big:
                    foreach (var eye in _eyeViewList)
                    {
                        eye.SetScale(1.5f);
                    }
                    break;

            }
        }
    }
}