using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    public class ActorPool : MonoBehaviour
    {
        [SerializeField] private int maxLimit;
        [SerializeField] private List<Actor> actors = new List<Actor>();

        public int MaxLimit { get { return maxLimit; } }
        public List<Actor> Actors { get { return actors; } }

        public int Count { get { return actors.Count; } }

        public void Add(Actor actor) { actors.Add(actor); }
        public Actor GetWaitingActorByPrefabId(string prefabId, out bool isOkToInstantiate) 
        {
            isOkToInstantiate = (maxLimit > 0) ? (actors.Count < maxLimit - 1) : true;

            int numberOfActors = actors.Count;
            for (int i = 0; i < numberOfActors; i++)
            {
                if (actors[i].FilterData.PrefabId == prefabId && actors[i].IsWaitingInPool)
                    return actors[i];
            }

            return null;
        }
    }
}