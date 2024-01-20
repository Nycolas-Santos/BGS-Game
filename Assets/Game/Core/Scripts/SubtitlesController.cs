using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Scripts
{
    public class SubtitlesController : MonoBehaviour
    {
        private Text _text;

        public event Action OnFinishSubtitleLine;
        public event Action OnStartSubtitleLine;
        
        private void Start()
        {
            _text = GetComponentInChildren<Text>();
        }

        public void DisplaySubtitle(string subtitle,float time)
        {
            
            StartCoroutine(IEDisplaySubtitle(subtitle,time));
        }

        private IEnumerator IEDisplaySubtitle(string subtitle,float time)
        {
            if (OnStartSubtitleLine != null) OnStartSubtitleLine.Invoke();
            _text.text = subtitle;
            yield return new WaitForSeconds(time);
            _text.text = null;
            if (OnFinishSubtitleLine != null) OnFinishSubtitleLine.Invoke();
        }
    }
}
