using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class TmpAnimationTickInitializer
    {
        TMP_Text _text;

        [Inject]
        public TmpAnimationTickInitializer(TMP_Text text)
        {
            _text = text;
        }

        public TMP_TextInfo TickStart()
        { 
            _text.ForceMeshUpdate(true);
            return _text.textInfo;
        }

        public void TickEnd(TMP_TextInfo tmpInfo)
        {

            //”½‰f
            for (int i = 0; i < tmpInfo.materialCount; i++)
            {
                if (tmpInfo.meshInfo[i].mesh == null) { continue; }

                tmpInfo.meshInfo[i].mesh.vertices = tmpInfo.meshInfo[i].vertices;
                _text.UpdateGeometry(tmpInfo.meshInfo[i].mesh, i);
            }


        }
    }
}