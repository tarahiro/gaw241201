using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class TextScaleChanger :MonoBehaviour, ITextScaleChanger
    {
        private TMP_Text _tmpText;
        private TMP_TextInfo tmpInfo;

        const int c_vertex = 4;

        public void Construct(TMP_Text tmpText)
        {
            _tmpText = tmpText;
        }

        public void Initialize()
        {
        }


        public TMP_TextInfo TextScaleChange(TMP_TextInfo tmpInfo, int textIndex, Vector2 scale)
        {

            var charInfo = tmpInfo.characterInfo[textIndex];
            if (!charInfo.isVisible) return tmpInfo;

            int matIndex = charInfo.materialReferenceIndex;
            int vertIndex = charInfo.vertexIndex;

            Vector3[] verts = tmpInfo.meshInfo[matIndex].vertices;

            Vector2 center = Vector2.zero;
            for (int k = 0; k < c_vertex; k++)
            {
                center += (Vector2)verts[vertIndex + k];
            }
            center /= c_vertex;


            for (int k = 0; k < c_vertex; k++)
            {
                verts[vertIndex + k] = center + ((Vector2)verts[vertIndex + k] - center) * scale;
            }


            return tmpInfo;

        }

    }
}