using System;
using System.Collections.Generic;
using UnityEngine;
using VN_System.Actors;
using VN_System.UI;
using VN_System.Media;
using Yarn.Unity;

namespace VN_System.Core
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField]
        private ActorSlotsManager _actorSlotsManager = null;

        [SerializeField, Tooltip("Resources folder where the system can find the ScriptableObjects where the ActorDefinitions are stored.")]
        private string _actorsFolder = "Actors";

        [SerializeField]
        private StagePositionID _activeSpeaker = StagePositionID.Left;
        public StagePositionID activeSpeaker => _activeSpeaker;
        private Dictionary<string, ActorDefinitionBase> ActorDatabase = new Dictionary<string, ActorDefinitionBase>();

        private DialogueRunner _dialogueRunner = null;

        private void Awake()
        {
            RegisterAllActors();
            RegisterYarnFiles();
        }

        private void RegisterYarnFiles()
        {
            _dialogueRunner = GetComponent<DialogueRunner>();
            _dialogueRunner.yarnScripts = Resources.LoadAll<YarnProgram>("Yarn Files");
        }

        public void SetSpeaker(string newLabel, StagePositionID stagePositionID)
        {
            ClearPreviousLabel();

            ActorSlot newActor = _actorSlotsManager.FindActorSlot(stagePositionID);
            SetSpeaker(newLabel, newActor);
        }

        public void SetSpeaker(string newLabel, ActorDefinitionBase actorDefinition)
        {
            ClearPreviousLabel();

            ActorSlot newActor = _actorSlotsManager.FindActorSlot(actorDefinition);
            SetSpeaker(newLabel, newActor);
        }

        public void SetEmotion(ActorDefinitionBase actorDefinition, EmotionID emotionID)
        {
            ActorSlot actorSlot = _actorSlotsManager.FindActorSlot(actorDefinition);
            if(actorSlot == null)
            {
                Debug.Log("You are setting the emotion of a character that has not entered the scene!");
                return;
            }
            actorSlot.SetEmotion(emotionID);
        }

        public ActorDefinitionBase GetActorDefinition(string actorName)
        {
            if(ActorDatabase.TryGetValue(actorName, out ActorDefinitionBase actorDefinition))
            {
                return actorDefinition;
            }
            return null;
        }

        public void EnterActor(ActorDefinitionBase actor, StagePositionID stageID, EmotionID emotionID)
        {
            ActorSlot actorSlot = _actorSlotsManager.FindActorSlot(stageID);
            if (actorSlot == null) return;
            ClearPreviousLabel();
            SetSpeaker(actor.firstName, actorSlot);
            actorSlot.EnterActor(actor, emotionID);
        }

        public void ExitActor(ActorDefinitionBase actorDefinition)
        {
            ActorSlot actorSlot = _actorSlotsManager.FindActorSlot(actorDefinition);
            if (actorSlot == null) return;
            actorSlot.ExitActor();
        }

        public void ExitActor(StagePositionID stageID)
        {
            ActorSlot actorSlot = _actorSlotsManager.FindActorSlot(stageID);
            if (actorSlot == null) return;
            actorSlot.ExitActor();
        }

        private void SetSpeaker(string newLabel, ActorSlot actorSlot)
        {
            actorSlot.speakerUI.SetLabel(newLabel);
            _activeSpeaker = actorSlot.stagePosition;
        }

        private void ClearPreviousLabel()
        {
            ActorSlot previousActor = _actorSlotsManager.FindActorSlot(_activeSpeaker);
            previousActor.speakerUI.ClearLabel();
        }

        private void RegisterAllActors()
        {
            var actors = Resources.LoadAll<ActorSO_Base>(_actorsFolder);
            foreach (var actorSO in actors)
            {
                ActorDefinitionBase actorDefinition = actorSO.GetActorDefinition();
                string id = actorSO.GetActorID();
                ActorDatabase[id] = actorDefinition;
            }
        }
    }
}
  