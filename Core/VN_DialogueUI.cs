using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;

namespace VN_System.Core
{
    public class VN_DialogueUI : DialogueUI
    {
        [SerializeField]
        private TextMeshProUGUI _speakerNameUI = null;

        public void UpdateSpeakerLabel(string newSpeaker)
        {
            if(string.IsNullOrEmpty(newSpeaker))
            {
                Debug.LogWarning("UpdateSpeakerLabel input has an empty or null value!");
                return;
            }
            _speakerNameUI.text = newSpeaker;
        }
    }
}
