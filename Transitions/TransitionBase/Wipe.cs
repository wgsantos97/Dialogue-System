using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Transitions
{
    public class Wipe : TransitionBase
    {
        public void PlayAnimation()
        {
            StartCoroutine(Transitioning());
        }

        protected IEnumerator Transitioning()
        {
            StartAnimation();
            yield return new WaitForSeconds(1f);
            StopAnimation();
        }

        protected override IEnumerator Transition(Action OnComplete, float duration = 0f)
        {
            StartAnimation();
            yield return new WaitForSeconds(1f);
            OnComplete();
            yield return new WaitForSeconds(1f);
            StopAnimation();
        }
    }
}
