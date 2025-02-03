using Cysharp.Threading.Tasks;
using MessagePipe;
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
    public class SkillItemView : MonoBehaviour, IMenuItemView
    {
        const string c_picturePath = "Prefab/CardPicture/";
        SkillArgs.Data _args;

        [SerializeField] TextMeshProUGUI _description;
        [SerializeField] Transform _skillPictureLocator;
        [SerializeField] SkillItemHeaderView _header;
        [SerializeField] Transform _root;

        [Inject] ISkillCardGazePublisher _skillCardGazePublisher;

        SkillPicture _skillPicture;

        Subject<SkillArgs.Data> _decided = new Subject<SkillArgs.Data>();
        public IObservable<SkillArgs.Data> Decided => _decided;




        public void SetData(SkillArgs.Data args)
        {
            _args = args;
            _header.Set(args.Name, args.Category);
            _description.text = args.Description;
            if(ResourceUtil.IsExist(c_picturePath + args.Id))
            {
                _skillPicture = Instantiate(ResourceUtil.GetResource<SkillPicture>(c_picturePath + args.Id), _skillPictureLocator);
                _skillPicture.transform.localPosition = Vector3.zero;
            }
        }

        public void Focus()
        {
            _root.localScale = Vector3.one * 1.5f;
            _skillCardGazePublisher.OnSkillCardFocus(Camera.main.WorldToScreenPoint(transform.position));
        }

        public void UnFocus()
        {
            _root.localScale = Vector3.one * 1f;
        }


        public void Decide()
        {
            _decided.OnNext(_args);
        }

    }
}