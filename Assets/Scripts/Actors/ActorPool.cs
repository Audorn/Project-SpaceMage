using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Actors
{
    /// <summary>
    /// A list of Actors that are deactivated and activated to avoid unnecessary instantiations and destroys.
    /// </summary>
    public class ActorPool : MonoBehaviour
    {
        [SerializeField] private int maxLimit;                              // Editor configurable.
        [SerializeField] private List<Actor> actors = new List<Actor>();    // Editor viewable.

        public int MaxLimit => maxLimit;
        public List<Actor> Actors => actors;

        public int Count => actors.Count;
        public void Add(Actor actor) => actors.Add(actor);

        /// <summary>
        /// Find an actor to activate.
        /// </summary>
        /// <param name="prefabId">The prefab Id to look for.</param>
        /// <param name="isOkToInstantiate">Out param: Is there room to instantiate a new one?</param>
        /// <returns>A waiting actor, or null if none found.</returns>
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