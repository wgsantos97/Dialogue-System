using UnityEngine;
using VN_System.UI;

namespace VN_System.Actors
{
    public abstract class ActorDefinitionBase
    {
        [SerializeField, Header("Template")]
        private ActorPrefab _actorPrefab = new ActorPrefab();

        // TODO abstract the names into a unique class that a badass programmer can extend if needed.
        [Header("Speaker Name")]
        public string firstName = "";
        public string lastName = "";
        public string fullName => firstName + " " + lastName;
        
        public ActorTypeID GetActorType()
        {
            return _actorPrefab.actorTypeID;
        }

        public GameObject GetPrefab()
        {
            return _actorPrefab.prefab;
        }

        public abstract BasePortrait GetPortrait(EmotionID emotionID);
    }
}

