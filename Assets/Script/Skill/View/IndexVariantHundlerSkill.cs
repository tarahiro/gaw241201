using Cysharp.Threading.Tasks;
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
    public class IndexVariantHundlerSkill : IIndexVariantHundler
    {
        [Inject] SkillMenuView _skillMenuView;

        int _maxNumber;
        public void SetMaxNumber(int number)
        {
            _maxNumber = number;
        }
        public int IndexVariant(Vector2Int cursorDirection)
        {
            int index = _skillMenuView.CurrentIndex;

            if(cursorDirection.x == 1)
            {
                index++;
            }
            if(cursorDirection.x == -1)
            {
                index--;
            }

            if (index < 0) index = _maxNumber - 1;
            if (index >= _maxNumber) index = 0;

            return index;
        }
    }
}