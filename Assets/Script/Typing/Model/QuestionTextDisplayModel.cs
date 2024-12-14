using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.TypingUtil;

namespace gaw241201
{
    public class QuestionTextDisplayModel : IQuestionTextDisplayModel
    {

        bool _isInitialized = false;

        Subject<string> _textUpdated = new Subject<string>();
        Subject<int> _correctInputted = new Subject<int>();
        public IObservable<string> TextUpdated => _textUpdated;
        public IObservable<int> CorrectInputted => _correctInputted;


        public void GenerateQuestionText(string questionChar, int charIndex)
        {


            int _viewIndex = charIndex - CountCharactersBetweenBrackets(questionChar, charIndex);
            string _viewString = RemoveBracketsAndContents(questionChar);


          

            //í ím
            Log.Comment("ñ‚ëËï∂ÇÃçXêVäÆóπ");
            _textUpdated.OnNext(_viewString);
            _correctInputted.OnNext(_viewIndex);
        }
    }
}