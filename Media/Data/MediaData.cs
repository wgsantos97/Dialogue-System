using System;
using UnityEngine;
using UnityEngine.Video;

namespace VN_System.Media.Data
{
    [Serializable]
    public abstract class MediaData
    {
        public abstract string GetFileName();
    }

    [Serializable]
    public class MediaDataImage : MediaData
    {
        [SerializeField]
        private Sprite _sprite = null;
        public Sprite sprite => _sprite;

        public override string GetFileName()
        {
            return _sprite.name;
        }
    }

    [Serializable]
    public class MediaDataAudio : MediaData
    {
        [SerializeField]
        private AudioClip _audioClip = null;
        public AudioClip audioClip => _audioClip;

        public override string GetFileName()
        {
            return _audioClip.name;
        }
    }

    [Serializable]
    public class MediaDataVideo : MediaData
    {
        [SerializeField]
        private VideoClip _video = null;
        public VideoClip video => _video;

        public override string GetFileName()
        {
            return _video.name;
        }
    }
}
