using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VN_System.Actors;

namespace VN_System.UI
{
    public class ActorSlot : MonoBehaviour
    {
        [SerializeField]
        private StagePositionID _stagePosition = StagePositionID.Left;
        public StagePositionID stagePosition => _stagePosition;

        [SerializeField]
        private SpeakerUI _speakerUI = null;
        public SpeakerUI speakerUI => _speakerUI;

        [SerializeField]
        private List<ActorBase> _actors = null;

        private ActorBase _activeActor = null;

        private void Awake()
        {
            InitActors();
        }

        public ActorDefinitionBase GetActiveActorDefinition()
        {
            return _activeActor != null ? _activeActor.actorDefinition : null;
        }

        public void EnterActor(ActorDefinitionBase aD, EmotionID emotionID)
        {
            ExitActor(); // Clear previous Actor

            // Determine if the correct prefab already exists under the ActorSlot
            ActorBase actorBase = FindActorBase(aD.GetActorType());
            if (actorBase != null) // True --> Update the activeActor
            {
                _activeActor = actorBase;
            }
            else // False --> Instantiate a new instance of that prefab
            {
                var actor = Instantiate(aD.GetPrefab(), gameObject.transform);
                _activeActor = actor.GetComponent<ActorBase>();
                _activeActor.Register(this);
                _actors.Add(_activeActor);
            }
            _activeActor.EnterActor(aD, emotionID);
            _speakerUI.SetLabel(aD.firstName);
        }

        public void ExitActor()
        {
            if(_activeActor != null)
            {
                _activeActor.ExitActor();
                _activeActor = null;
            }
            _speakerUI.ClearLabel();
        }

        public void SetEmotion(EmotionID newEmotion)
        {
            if(_activeActor != null)
            {
                _activeActor.SetEmotion(newEmotion);
            }
        }

        private void InitActors()
        {
            if (_actors == null)
            {
                _actors = GetComponentsInChildren<ActorBase>().ToList();
            }
        }

        private ActorBase FindActorBase(ActorTypeID actorTypeID)
        {
            InitActors();
            foreach(ActorBase actor in _actors)
            {
                if(actor.actorTypeID == actorTypeID)
                {
                    return actor;
                }
            }
            return null;
        }
    }
}
