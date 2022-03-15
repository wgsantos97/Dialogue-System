using System;
using UnityEngine;

namespace VN_System.Actors
{
    [Serializable]
    public class ActorPrefab
    {
        [SerializeField]
        private ActorTypeID _actorTypeID = ActorTypeID.Static;
        public ActorTypeID actorTypeID => _actorTypeID;


        [SerializeField]
        private GameObject _prefab = null;
        public GameObject prefab => _prefab;
    }
}
