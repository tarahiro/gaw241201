using Cysharp.Threading.Tasks;
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
    public class PointModel : IPointable,IPointGettable
    {
        const int c_unitPoint = 100;
        const int c_penaltyPoint = 300;
        const float c_pointPerTime = 30f;

        int m_point = 0;

        Subject<int> _pointUpdated = new Subject<int>();


        Subject<Unit> _initialized = new Subject<Unit>();
        public IObservable<int> PointUpdated => _pointUpdated;
        public IObservable<Unit> Initialized => _initialized;

        public void InitializePoint()
        {
            _initialized.OnNext(Unit.Default);
            DecrementPoint(m_point);
        }

        public void AddUnitPoint()
        {
            IncrementPoint(c_unitPoint);
        }
        public void ReducePenaltyPoint()
        {
            DecrementPoint(c_penaltyPoint);
        }

        public void AddRemainTimePoint(float remainTime)
        {
            IncrementPoint((int)(remainTime * c_pointPerTime));
        }

        public int GetPoint()
        {
            return m_point;
        }

        void IncrementPoint(int addedPoint)
        {
            m_point += addedPoint;
            _pointUpdated.OnNext(m_point);
        }

        void DecrementPoint(int decrementedPoint)
        {
            m_point -= decrementedPoint;
            _pointUpdated.OnNext(m_point);
        }
    }
}