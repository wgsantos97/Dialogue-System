using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Transitions
{
    public abstract class TransitionBase : MonoBehaviour
    {
        [SerializeField]
        protected TransitionID _transitionID = TransitionID.Crossfade;
        public TransitionID transitionID => _transitionID;

        protected Animator _animator = null;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public virtual void StartTransition(Action OnComplete, float duration)
        {
            StartCoroutine(Transition(OnComplete, duration));
        }

        protected void StartAnimation()
        {
            _animator.Play("Enter");
        }

        protected void StopAnimation()
        {
            _animator.Play("Exit");
        }

        protected abstract IEnumerator Transition(Action OnComplete, float seconds);
    }
}
