using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Transitions
{
    public class FadeIn : TransitionBase
    {
        protected override IEnumerator Transition(Action OnComplete, float duration = 1f)
        {
            StopAnimation();
            OnComplete();
            yield return new WaitForSeconds(1f);
        }
    }
}
