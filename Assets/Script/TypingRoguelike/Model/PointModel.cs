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
    public class PointModel
    {
        const int c_unitPoint = 100;
        const int c_penaltyPoint = 300;
        const float c_pointPerTime = 100f;

        int m_point = 0;

        Subject<int> _pointUpdated = new Subject<int>();

        public IObservable<int> PointUpdated => _pointUpdated;

        public void InitializeModel()
        {
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

        void IncrementPoint(int addedPoint)
        {
            m_point += addedPoint;
            _pointUpdated.OnNext(m_point);
            Log.DebugLog(m_point);
        }

        void DecrementPoint(int decrementedPoint)
        {
            m_point -= decrementedPoint;
            _pointUpdated.OnNext(m_point);
            Log.DebugLog(m_point);
        }
    }
}