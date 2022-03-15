using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Transitions
{
    public class Crossfade : TransitionBase
    {
        [SerializeField]
        protected float _waitTimeEnd = 1f;

        protected override IEnumerator Transition(Action OnComplete, float duration)
        {
            StartAnimation();
            yield return new WaitForSeconds(duration);
            StopAnimation();
            yield return new WaitForSeconds(_waitTimeEnd);
            OnComplete();
        }
    }
}
