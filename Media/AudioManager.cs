using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VN_System.Media.Audio;
using VN_System.Media.Data;

namespace VN_System.Media
{
    [RequireComponent(typeof(MediaManager))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private MediaDatabaseSO _mediaDatabase = null;

        [SerializeField]
        private AudioVoice _audioVoice = null;

        [SerializeField]
        private AudioSFX _audioSFX = null;

        [SerializeField]
        private AudioMusic _audioMusic = null;

        [SerializeField, Header("Voice Settings")]
        private bool _useDefaultVoiceLoop = true;

        [SerializeField]
        private AudioClip _defaultVoiceLoop = null;

        public void StopAllAudio()
        {
            _audioVoice.StopMedia();
            _audioVoice.StopAllCoroutines();
            _audioSFX.StopMedia();
            _audioMusic.StopMedia();

        }

        public void PlayVoice()
        {
            if (!_useDefaultVoiceLoop) return;
            _audioVoice.PlayMedia(_defaultVoiceLoop, true);
        }

        public void PlayVoice(string filename, bool isLooping)
        {
            if (_useDefaultVoiceLoop) return;
            AudioClip voice = _mediaDatabase.GetSoundClip(filename);
            _audioVoice.PlayMedia(voice, isLooping);
        }

        public void StopVoice()
        {
            _audioVoice.StopMedia();
        }

        public void PlaySFX(string filename)
        {
            AudioClip sfx = _mediaDatabase.GetSoundClip(filename);
            _audioSFX.PlayMedia(sfx);
        }

        public void PlayMusic(string filename, bool isLooping)
        {
            AudioClip music = _mediaDatabase.GetSoundClip(filename);
            _audioMusic.PlayMedia(music, isLooping);
        }
    }
}
