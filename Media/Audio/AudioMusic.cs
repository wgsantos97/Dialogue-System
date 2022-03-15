using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Media.Audio
{
    public class AudioMusic : AudioBase
    {
        public override void PlayMedia(AudioClip audioClip, bool isLooping = true)
        {
            base.PlayMedia(audioClip);
        }

        public override void StopMedia()
        {
            base.StopMedia();
        }
    }
}
