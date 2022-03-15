using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Media.Audio
{
    public class AudioVoice : AudioBase
    {
        private Coroutine _dialogueAudio = null;
        private bool _isSpeaking = true;
        private bool _isLooping = false;

        public override void PlayMedia(AudioClip audioClip, bool isLooping)
        {
            _audioSource.clip = audioClip;
            _isLooping = isLooping;
            if(_isLooping)
            {
                _isSpeaking = true;
                _dialogueAudio = StartCoroutine(Speaking());
            }
            else
            {
                base.PlayMedia(audioClip);
            }
        }

        public override void StopMedia()
        {
            if (_isLooping)
            {
                _isSpeaking = false;
                StopCoroutine(_dialogueAudio);
                _audioSource.clip = null;
            }
            _isLooping = false;
        }

        protected virtual IEnumerator Speaking()
        {
            while (_isSpeaking)
            {
                if (_audioSource.isPlaying)
                {
                    yield return null;
                    continue;
                }
                _audioSource.Play();
                yield return null;
            }
        }
    }
}
