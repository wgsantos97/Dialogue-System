using System;
using UnityEngine;

namespace VN_System.UI
{
    [Serializable]
    public class BasePortrait
    {
        [SerializeField]
        private Sprite _sprite = null;
        public Sprite sprite => _sprite;
    }
}
