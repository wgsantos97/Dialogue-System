using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace VN_System.UI
{
    public class SpeakerUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _containerBox = null;

        [SerializeField]
        private TextMeshProUGUI _label = null;

        [SerializeField]
        private UnityEvent _onLabelUpdated = new UnityEvent();

        private void OnEnable()
        {
            _containerBox.SetActive(false);
        }

        public void ClearLabel()
        {
            _label.text = "";
            _containerBox.SetActive(false);
        }

        public void SetLabel(string newLabel)
        {
            _containerBox.SetActive(true);
            _label.text = newLabel;
            _onLabelUpdated.Invoke();
        }

    }
}

