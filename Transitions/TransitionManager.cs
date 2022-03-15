using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Transitions
{
    public class TransitionManager : MonoBehaviour
    {
        private TransitionBase[] _transitions;

        private void Awake()
        {
            _transitions = GetComponentsInChildren<TransitionBase>();
        }

        public void Transition(TransitionID transitionID, Action OnComplete, float duration)
        {
            foreach(TransitionBase transition in _transitions)
            {
                if(transition.transitionID == transitionID)
                {
                    transition.StartTransition(OnComplete, duration);
                    return;
                }
            }
            Debug.LogError("Unable to find transition type: " + transitionID);
        }
    }
}
