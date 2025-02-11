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
    using UnityEngine;
    using TMPro;
    using UnityEngine.UIElements;

    [ExecuteInEditMode, RequireComponent(typeof(TMP_Text))]
    public class FakeIdleTextMover : MonoBehaviour, IIdleTextMover
    {
        [SerializeField] private float amp;
        [SerializeField] private float speed;

        [SerializeField] private float rotAmp;
        [SerializeField] private float sclAmp;
        [SerializeField] private float sclWaitMaxTime;
        [SerializeField] private float sclWaitMinTime;

        private TMP_Text _tmpText;
        ITextScaleChanger _textScaleChanger;

        public void Construct(TMP_Text tmpText, ITextScaleChanger textScaleChanger)
        {
            _tmpText = tmpText;
            _textScaleChanger = textScaleChanger;
        }

        private TMP_TextInfo tmpInfo;

        List<List<float>> randomTime;
        int count;

        bool _isEnabled = false;

        public void StartIdle()
        {
            _isEnabled = true;
        }

        public void StopIdle()
        {
            _isEnabled = false;
        }



        public void Initialize()
        {
            randomTime = new List<List<float>>();

            for (int i = 0; i < 10; i++)
            {
                randomTime.Add(new List<float>());
                for (int j = 0; j < 100; j++)
                {
                    randomTime[i].Add(UnityEngine.Random.Range(0f, 100f));
                }
            }

            scaleWaitTime = new List<float>();
            scaleActiveTime = new List<float>();

            isScaleWait = new List<bool>();

            for (int j = 0; j < 100; j++)
            {
                scaleWaitTime.Add(UnityEngine.Random.Range(sclWaitMinTime, sclWaitMaxTime));
                scaleActiveTime.Add(0);
                isScaleWait.Add(true);
            }

        }

        public TMP_TextInfo Tick(TMP_TextInfo tmpInfo)
        {
            if (_isEnabled)
            {
                return UpdateAnimation(tmpInfo);
            }
            else
            {
                return tmpInfo;
            }
        }

        const int c_vertex = 4;

        List<bool> isScaleWait;

        List<float> scaleWaitTime;
        List<float> scaleActiveTime;


        private TMP_TextInfo UpdateAnimation(TMP_TextInfo tmpInfo)
        {

            count = Mathf.Min(tmpInfo.characterCount, tmpInfo.characterInfo.Length);
            for (int i = 0; i < count; i++)
            {
                var charInfo = tmpInfo.characterInfo[i];
                if (!charInfo.isVisible)
                    continue;

                int matIndex = charInfo.materialReferenceIndex;
                int vertIndex = charInfo.vertexIndex;


                Vector3[] verts = tmpInfo.meshInfo[matIndex].vertices;


                //位置変動
                Vector3 vec = new Vector3(
Mathf.Sin((randomTime[0][i] + Time.realtimeSinceStartup) * Mathf.PI * speed) * amp,
 Mathf.Sin((randomTime[1][i] + Time.realtimeSinceStartup) * Mathf.PI * speed) * amp,
 0
                    );
                verts[vertIndex + 0] += vec;
                verts[vertIndex + 1] += vec;
                verts[vertIndex + 2] += vec;
                verts[vertIndex + 3] += vec;


                //回転変動
                var angle = Mathf.Sin((randomTime[2][i] + Time.realtimeSinceStartup) * Mathf.PI * speed) * rotAmp;
                Vector3 center = Vector3.zero;
                for (int k = 0; k < c_vertex; k++)
                {
                    center += verts[vertIndex + k];
                }
                center /= c_vertex;


                for (int k = 0; k < c_vertex; k++)
                {
                    verts[vertIndex + k] = center + Quaternion.Euler(0, 0, angle) * (verts[vertIndex + k] - center);
                }

                //スケール変動
                if (isScaleWait[i])
                {
                    scaleWaitTime[i] -= Time.deltaTime;
                    if (scaleWaitTime[i] < 0)
                    {
                        isScaleWait[i] = false;
                        scaleActiveTime[i] = 0f;
                    }
                }
                else
                {
                    float scale;
                    if (scaleActiveTime[i] < .5f / speed)
                    {
                        scale = 1f + sclAmp * scaleActiveTime[i] / (.5f / speed);

                    }
                    else
                    {
                        scale = 1f + sclAmp * (1f / speed - scaleActiveTime[i]) / (.5f / speed);

                        if (scale < 1f)
                        {
                            isScaleWait[i] = true;
                            scaleWaitTime[i] = UnityEngine.Random.Range(sclWaitMinTime, sclWaitMaxTime);
                        }
                    }

                    tmpInfo = _textScaleChanger.TextScaleChange(tmpInfo, i, new Vector2(1f, scale));
                    scaleActiveTime[i] += Time.deltaTime;
                }
            }

            return tmpInfo;
        }
    }
}