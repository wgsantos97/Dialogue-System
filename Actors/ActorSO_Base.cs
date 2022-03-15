using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN_System.Actors
{
    public abstract class ActorSO_Base : ScriptableObject
    {
        public abstract string GetActorID();

        public abstract ActorDefinitionBase GetActorDefinition();
    }
}
