using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using TMPro;

namespace gaw241201.View
{
    public class SkillItemView : MonoBehaviour
    {
        SkillArgs.Data _args;

        [SerializeField] TextMeshProUGUI _name;
        [SerializeField] TextMeshProUGUI _description;

        Subject<SkillArgs.Data> _decided = new Subject<SkillArgs.Data>();
        public IObservable<SkillArgs.Data> Decided => _decided;

        public void SetData(SkillArgs.Data args)
        {
            _args = args;
            _name.text = GenerateLabel(args.Category) + args.Name;
            _description.text = args.Description;
        }

        public void Decide()
        {
            _decided.OnNext(_args);
        }

        public string GenerateLabel(SkillConst.SkillCategory category)
        {
            switch (category)
            {
                case SkillConst.SkillCategory.Leet:
                    return "�y�u���z";
                case SkillConst.SkillCategory.Animal:
                    return "�y�����z";
                case SkillConst.SkillCategory.Human:
                    return "�y�l���z";
                case SkillConst.SkillCategory.Vi:
                    return "�y�������z";
                case SkillConst.SkillCategory.Vt:
                    return "�y�������z";
                default:
                    Log.DebugLog("Error: �s���ȃJ�e�S���[�ł�: " + category);
                    return "Error";
            }
        }
    }
}