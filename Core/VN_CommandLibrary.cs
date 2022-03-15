using System;
using UnityEngine;
using VN_System.Actors;
using VN_System.UI;
using VN_System.Media;
using VN_System.Transitions;
using TMPro;
using Yarn.Unity;

namespace VN_System.Core
{
    public class VN_CommandLibrary : MonoBehaviour
    {
        [SerializeField]
        private GameObject _dialogueContainer = null;
        [SerializeField]
        private TextMeshProUGUI _textMeshProUGUI = null;
        [SerializeField]
        private TransitionManager _transitionManager = null;
        [SerializeField]
        private MediaManager _mediaManager = null;
        [SerializeField]
        private AudioManager _audioManager = null;

        private DialogueSystem _dialogueSystem = null;
        private DialogueRunner _dialogueRunner = null;

        private void Awake()
        {
            _dialogueRunner = GetComponent<DialogueRunner>();
            _dialogueSystem = GetComponent<DialogueSystem>();
            RegisterCommands();
        }

        protected virtual void RegisterCommands()
        {
            _dialogueRunner.AddCommandHandler("show", Show);
            _dialogueRunner.AddCommandHandler("hide", Hide);
            _dialogueRunner.AddCommandHandler("setSpeaker", SetSpeaker);
            _dialogueRunner.AddCommandHandler("enter", EnterActor);
            _dialogueRunner.AddCommandHandler("exit", ExitActor);
            _dialogueRunner.AddCommandHandler("clearAll", ClearAll);
            _dialogueRunner.AddCommandHandler("clearActors", ClearActors);
            _dialogueRunner.AddCommandHandler("say", Say);
            _dialogueRunner.AddCommandHandler("playSFX", PlaySFX);
            _dialogueRunner.AddCommandHandler("playMusic", PlayMusic);
            _dialogueRunner.AddCommandHandler("setBackground", SetBackground);
            _dialogueRunner.AddCommandHandler("clearBackground", ClearBackground);
            _dialogueRunner.AddCommandHandler("setForeground", SetForeground);
            _dialogueRunner.AddCommandHandler("clearForeground", ClearForeground);
            _dialogueRunner.AddCommandHandler("setVideo", SetVideo);
            _dialogueRunner.AddCommandHandler("transition", Transition);
        }

        public void ShowDialogueContainer()
        {
            if (_dialogueContainer.activeInHierarchy) return;
            _dialogueContainer.SetActive(true);
        }

        protected virtual void Show(string[] info)
        {
            ShowDialogueContainer();
        }

        protected virtual void Hide(string[] info)
        {
            _textMeshProUGUI.text = "";
            _dialogueContainer.SetActive(false);
        }

        protected virtual void SetSpeaker(string[] info)
        {
            if (info.Length == 0 || info.Length > 2)
            {
                Debug.LogError("Invalid argument length: " + info.Length);
                return;
            }
            
            ShowDialogueContainer(); // Catches the case where the Hide command was used to disable the Container, and now we need to access the Container again.
            ActorDefinitionBase actorDefinition = _dialogueSystem.GetActorDefinition(info[0]);
            if (actorDefinition == null)
            {
                _dialogueSystem.SetSpeaker(info[0], StagePositionID.Left);
            }
            else
            {
                _dialogueSystem.SetSpeaker(info[0], actorDefinition);
                if (info.Length == 2 && Enum.TryParse(info[1], out EmotionID emotion))
                {
                    _dialogueSystem.SetEmotion(actorDefinition, emotion);
                }
            }
        }
         
        protected virtual void EnterActor(string[] info)
        {
            if (info.Length < 2)
            {
                Debug.LogError("Invalid argument length: " + info.Length);
                return;
            }

            ActorDefinitionBase actorDefinition = _dialogueSystem.GetActorDefinition(info[0]);
            bool isValidPosition = Enum.TryParse(info[1], out StagePositionID position);
            EmotionID emotion;
            if(info.Length != 3 || !Enum.TryParse(info[2], out emotion))
            {
                emotion = EmotionID.Neutral;
            }

            if (actorDefinition == null)
            {
                Debug.LogErrorFormat("Could not find actor: {0}", info[0]);
                return;
            }
            else if(!isValidPosition)
            {
                Debug.LogErrorFormat("Invalid stageID: {0}", info[1]);
                return;
            }

            ShowDialogueContainer(); // Catches the case where the Hide command was used to disable the Container, and now we need to access the Container again.
            _dialogueSystem.EnterActor(actorDefinition, position, emotion);
        }

        protected virtual void ExitActor(string[] info)
        {
            if (info.Length != 1)
            {
                Debug.LogError("Invalid argument length: " + info.Length);
                return;
            }

            // Try to find by name.
            ActorDefinitionBase actorDefinition = _dialogueSystem.GetActorDefinition(info[0]);
            if(actorDefinition != null)
            {
                _dialogueSystem.ExitActor(actorDefinition);
            }
            else if(Enum.TryParse(info[0], out StagePositionID position))
            {
                _dialogueSystem.ExitActor(position);
            }
            else
            {
                Debug.LogErrorFormat("Could not find name or stageID that matches: {0}", info[0]);
            }
        }

        protected virtual void ClearAll(string[] info)
        {
            _audioManager.StopAllAudio();
            _mediaManager.ClearBackground();
            _mediaManager.ClearForeground();
            Hide(info);
            ClearActors(info);
        }

        protected virtual void ClearActors(string[] info)
        {
            foreach(StagePositionID stagePosition in Enum.GetValues(typeof(StagePositionID)))
            {
                _dialogueSystem.ExitActor(stagePosition);
            }
        }

        protected virtual void Say(string[] info)
        {
            if(info.Length < 1)
            {
                Debug.LogError("Invalid argument length: " + info.Length);
                return;
            }

            string filename = info[0];

            bool isLooping = false;
            if(info.Length == 2)
            {
                isLooping = bool.Parse(info[1]);
            }

            _audioManager.PlayVoice(filename, isLooping);
        }

        protected virtual void PlaySFX(string[] info)
        {
            if(info.Length != 1)
            {
                Debug.LogError("Invalid argument length: " + info.Length);
                return;
            }

            string filename = info[0];

            _audioManager.PlaySFX(filename);
        }

        protected virtual void PlayMusic(string[] info)
        {
            if (info.Length < 1 || info.Length > 2)
            {
                Debug.LogError("Invalid argument length: " + info.Length);
                return;
            }

            string filename = info[0];

            bool isLooping = true;
            if (info.Length == 2)
            {
                isLooping = bool.Parse(info[1]);
                Debug.Log(isLooping);
            }

            _audioManager.PlayMusic(filename, isLooping);
        }

        protected virtual void SetBackground(string[] info)
        {
            if(info.Length != 1)
            {
                Debug.LogError("Invalid arguent length: " + info.Length);
                return;
            }

            string filename = info[0];
            _mediaManager.SetBackground(filename);
        }

        protected virtual void ClearBackground(string[] info)
        {
            _mediaManager.ClearBackground();
        }

        protected virtual void SetForeground(string[] info)
        {
            if (info.Length != 1)
            {
                Debug.LogError("Invalid arguent length: " + info.Length);
                return;
            }

            string filename = info[0];
            _mediaManager.SetForeground(filename);
        }

        protected virtual void ClearForeground(string[] info)
        {
            _mediaManager.ClearForeground();
        }

        protected virtual void SetVideo(string[] info, Action OnComplete)
        {
            if(info.Length != 1)
            {
                Debug.LogError("Invalid arguent length: " + info.Length);
                return;
            }

            string filename = info[0];
            _mediaManager.SetVideo(filename, OnComplete);
        }

        protected virtual void Transition(string[] info, Action OnComplete)
        {
            if(info.Length < 1 || info.Length > 2)
            {
                Debug.LogError("Invalid arguent length: " + info.Length);
                return;
            }

            TransitionID transitionID = (TransitionID) Enum.Parse(typeof(TransitionID), info[0]);

            float duration = 0f;
            if(info.Length == 2)
            {
                duration = float.Parse(info[1]);
            }
            
            _transitionManager.Transition(transitionID, OnComplete, duration);
        }
    }
}
