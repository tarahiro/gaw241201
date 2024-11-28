using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;
using static SoundManager;

namespace Tarahiro.Ui
{
    public static class TextUtil
    {
        const float c_defaultTextIntervalTime = .1f;
        public static async UniTask DisplayTextByCharacter(string text, TextMeshProUGUI textMeshProUGUI, string SeLabel, KeyCode decide, CancellationToken ct, bool IsSeRun = true, float textIntervalTime = c_defaultTextIntervalTime)
        {
            await TextCount(text,textMeshProUGUI, SeLabel, decide, ct, textIntervalTime);
        }

        static async UniTask TextCount(string m_text, TextMeshProUGUI m_textMeshProUGUI, string SeLabel, KeyCode m_decide, CancellationToken ct, float m_textIntervalTime)
        {
            SoundManager.PlaySEWithLoop(SeLabel);
            float m_Tick = 0;
            int textCount = 0;
            bool _isEnd = false;

            while (!_isEnd && ! ct.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);

                if (Input.GetKeyDown(m_decide))
                {
                    _isEnd = true;
                }

                m_Tick += Time.deltaTime;
                if (m_Tick > m_textIntervalTime)
                {
                    m_textMeshProUGUI.text += m_text[textCount];

                    m_Tick = 0;
                    textCount++;

                    if (textCount >= m_text.Length)
                    {
                        _isEnd = true;

                    }
                }
            }

            SoundManager.StopLoopSE();
            m_textMeshProUGUI.text = m_text;


        }
    }
}