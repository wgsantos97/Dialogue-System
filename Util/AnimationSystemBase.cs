using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Util
{
    public class AnimationSystemBase : MonoBehaviour
    {
        protected Animator _animator = null;
        protected string _emotion = "";
        protected int _currentHashState = 0;

        protected void InitAnimator()
        {
            if(_animator == null)
            {
                _animator = GetComponent<Animator>();
                if(_animator == null)
                {
                    Debug.LogError("Unable to find Animator!");
                }
            }
        }

        protected virtual void ChangeAnimationState(string emotion, int newHashState)
        {
            InitAnimator();
            if (_currentHashState == newHashState || _animator.runtimeAnimatorController == null) return;
            _emotion = emotion;
            _currentHashState = newHashState;
            _animator.Play(_currentHashState);
        }

        protected void StopAnimation()
        {
            _animator.StopPlayback();
        }

        protected void PlayAnimation()
        {
            _animator.Play(_currentHashState);
        }

        protected void PlayAnimation(int layer)
        {
            _animator.Play(_currentHashState, layer);
        }

        protected void PlayAnimation(int layer, float normalizedTime)
        {
            _animator.Play(_currentHashState, layer, normalizedTime);
        }
    }
}
