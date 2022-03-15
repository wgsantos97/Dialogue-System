using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Media.Audio
{
    public abstract class AudioBase : MonoBehaviour
    {
        protected AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public virtual void PlayMedia(AudioClip audioClip, bool isLooping = false)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        public virtual void StopMedia()
        {
            _audioSource.Stop();
            _audioSource.clip = null;
        }
    }
}
