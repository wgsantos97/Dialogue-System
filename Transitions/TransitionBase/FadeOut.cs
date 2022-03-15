using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Transitions
{
    public class FadeOut : TransitionBase
    {
        protected override IEnumerator Transition(Action OnComplete, float duration = 1f)
        {
            StartAnimation();
            yield return new WaitForSeconds(1f);
            OnComplete();
        }
    }
}
