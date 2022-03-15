using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace VN_System.Media.Data
{
    [CreateAssetMenu(fileName = "MediaDatabaseSO", menuName = "Visual Novel System/Media Database", order = 0)]
    public class MediaDatabaseSO : ScriptableObject
    {
        [SerializeField]
        private MediaDataImage[] _images = null;

        [SerializeField]
        private MediaDataAudio[] _sounds = null;

        [SerializeField]
        private MediaDataVideo[] _videos = null;

        private Dictionary<string, MediaData> _mediaDatabase = null;

        public Sprite GetImage(string filename)
        {
            InitDatabase();
            string key = GetKey(filename, MediaTypeID.Image);
            MediaDataImage result = FindMediaData(key) as MediaDataImage;
            return result.sprite;
        }

        public AudioClip GetSoundClip(string filename)
        {
            InitDatabase();
            string key = GetKey(filename, MediaTypeID.Sound);
            MediaDataAudio result = FindMediaData(key) as MediaDataAudio;
            return result.audioClip;
        }

        public VideoClip GetVideoClip(string filename)
        {
            InitDatabase();
            string key = GetKey(filename, MediaTypeID.Video);
            MediaDataVideo result = FindMediaData(key) as MediaDataVideo;
            return result.video;
        }

        private void InitDatabase()
        {
            if (_mediaDatabase != null) return;

            _mediaDatabase = new Dictionary<string, MediaData>();
            foreach (MediaDataImage image in _images)
            {
                string key = GetKey(image.GetFileName(), MediaTypeID.Image);
                _mediaDatabase[key] = image;
            }
            foreach (MediaDataAudio sound in _sounds)
            {
                string key = GetKey(sound.GetFileName(), MediaTypeID.Sound);
                _mediaDatabase[key] = sound;
            }
            foreach (MediaDataVideo video in _videos)
            {
                string key = GetKey(video.GetFileName(), MediaTypeID.Video);
                _mediaDatabase[key] = video;
            }
        }

        private string GetKey(string filename, MediaTypeID mediaTypeID)
        {
            return mediaTypeID + "_" + filename;
        }

        private MediaData FindMediaData(string key)
        {
            if (_mediaDatabase.TryGetValue(key, out MediaData value))
            {
                return value;
            }
            Debug.LogError("Unable to find media file under key: " + key);
            return null;
        }
    }
}
