using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Media.Audio
{
    public class AudioSFX : AudioBase
    {
        public override void PlayMedia(AudioClip audioClip, bool isLooping = false)
        {
            base.PlayMedia(audioClip);
        }

        public override void StopMedia()
        {
            base.StopMedia();
        }
    }
}
