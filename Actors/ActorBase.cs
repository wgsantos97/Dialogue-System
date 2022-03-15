using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VN_System.UI;

namespace VN_System.Actors
{
    public abstract class ActorBase : MonoBehaviour
    {
        [SerializeField]
        protected ActorTypeID _actorTypeID = ActorTypeID.Static;
        public ActorTypeID actorTypeID => _actorTypeID;

        [SerializeField]
        protected GameObject _main = null;

        [SerializeField]
        protected Sprite _defaultImage = null;

        protected ActorDefinitionBase _actorDefinition = null;
        public ActorDefinitionBase actorDefinition => _actorDefinition;

        public EmotionID currentEmotion { get; protected set; }

        public abstract void EnterActor(ActorDefinitionBase aD, EmotionID emotionID);

        public abstract void ExitActor();

        public abstract void SetEmotion(EmotionID newEmotion);

        public virtual void Register(ActorSlot slot)
        { }
    }
}
