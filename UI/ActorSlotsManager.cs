using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VN_System.Actors;

namespace VN_System.UI
{
    public class ActorSlotsManager : MonoBehaviour
    {
        private ActorSlot[] _actorSlots = null;

        private void Awake()
        {
            InitActorSlots();
        }

        private void InitActorSlots()
        {
            if (_actorSlots == null || _actorSlots.Length == 0)
            {
                _actorSlots = GetComponentsInChildren<ActorSlot>();
                if(_actorSlots.Length == 0)
                {
                    Debug.LogError("Unable to find Actor Slots.");
                }
            }
        }

        public ActorSlot FindActorSlot(StagePositionID stageID)
        {
            InitActorSlots();
            foreach(ActorSlot actorSlot in _actorSlots)
            {
                if(actorSlot.stagePosition == stageID)
                {
                    return actorSlot;
                }
            }
            return null;
        }

        public ActorSlot FindActorSlot(ActorDefinitionBase actorDefinition)
        {
            InitActorSlots();
            foreach (ActorSlot actorSlot in _actorSlots)
            {
                if (actorDefinition == actorSlot.GetActiveActorDefinition())
                {
                    return actorSlot;
                }
            }
            return null;
        }

    }
}
