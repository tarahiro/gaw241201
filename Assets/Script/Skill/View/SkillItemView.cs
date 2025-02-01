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
        const string c_picturePath = "Prefab/CardPicture/";
        SkillArgs.Data _args;

        [SerializeField] TextMeshProUGUI _name;
        [SerializeField] TextMeshProUGUI _description;
        [SerializeField] Transform _skillPictureLocator;

        SkillPicture _skillPicture;

        Subject<SkillArgs.Data> _decided = new Subject<SkillArgs.Data>();
        public IObservable<SkillArgs.Data> Decided => _decided;

        public void SetData(SkillArgs.Data args)
        {
            _args = args;
            _name.text = GenerateLabel(args.Category) + args.Name;
            _description.text = args.Description;
            if(ResourceUtil.IsExist(c_picturePath + args.Id))
            {
                _skillPicture = Instantiate(ResourceUtil.GetResource<SkillPicture>(c_picturePath + args.Id), _skillPictureLocator);
                _skillPicture.transform.localPosition = Vector3.zero;
            }
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
                    return "Åyíuä∑Åz";
                case SkillConst.SkillCategory.Animal:
                    return "ÅyìÆï®Åz";
                case SkillConst.SkillCategory.Human:
                    return "ÅyêlñºÅz";
                case SkillConst.SkillCategory.Vi:
                    return "Åyé©ìÆéåÅz";
                case SkillConst.SkillCategory.Vt:
                    return "ÅyëºìÆéåÅz";
                default:
                    Log.DebugLog("Error: ïsê≥Ç»ÉJÉeÉSÉäÅ[Ç≈Ç∑: " + category);
                    return "Error";
            }
        }
    }
}