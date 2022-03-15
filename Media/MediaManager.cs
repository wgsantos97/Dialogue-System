using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;
using VN_System.Media.Data;

namespace VN_System.Media
{
    public class MediaManager : MonoBehaviour
    {
        [SerializeField]
        private MediaDatabaseSO _mediaDatabase = null;

        [SerializeField]
        private Image _background = null;

        [SerializeField]
        private Image _foreground = null;

        [SerializeField]
        private VideoPlayer _videoPlayer = null;

        [SerializeField]
        private UnityEvent _onSkipVideo = new UnityEvent();

        private bool _isPlayingVideo = false;
        private bool _skipVideo = false;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) && _isPlayingVideo)
            {
                _skipVideo = true;
                _onSkipVideo.Invoke();
            }
        }

        public void SetBackground(string filename)
        {
            _background.gameObject.SetActive(true);
            Sprite image = _mediaDatabase.GetImage(filename);
            _background.sprite = image;
        }

        public void ClearBackground()
        {
            _background.sprite = null;
            _background.gameObject.SetActive(false);
        }

        public void SetForeground(string filename)
        {
            _foreground.gameObject.SetActive(true);
            Sprite image = _mediaDatabase.GetImage(filename);
            _foreground.sprite = image;
        }

        public void ClearForeground()
        {
            _foreground.sprite = null;
            _foreground.gameObject.SetActive(false);
        }

        public void SetVideo(string filename, Action OnComplete)
        {
            _videoPlayer.gameObject.SetActive(true);
            _videoPlayer.clip = _mediaDatabase.GetVideoClip(filename);
            StartCoroutine(PlayVideo(OnComplete));
        }

        private IEnumerator PlayVideo(Action OnComplete)
        {
            _videoPlayer.Play();
            _isPlayingVideo = true;
            yield return null;

            double time = _videoPlayer.clip.length;
            float videoLength = (float) time;
            float currentTime = 0f;
            
            while (currentTime < videoLength)
            {
                if(_skipVideo)
                {
                    yield return new WaitForSeconds(.5f);
                    break;
                }
                yield return new WaitForSeconds(1f);
                currentTime += 1f;
            }

            StopVideo();
            _skipVideo = false;
            OnComplete();
        }

        private void StopVideo()
        {
            _videoPlayer.Stop();
            _isPlayingVideo = false;
            _videoPlayer.clip = null;
            _videoPlayer.gameObject.SetActive(false);
        }
    }
}
