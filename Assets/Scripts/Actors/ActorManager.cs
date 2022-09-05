using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Actors
{
    /// <summary>
    /// Tracks ActorPools and instantiates/activates Actors.
    /// </summary>
    public class ActorManager : MonoBehaviour
    {
        // Singleton.
        private static ActorManager _;
        public static ActorManager Singleton { get { return _; } }
        private void Awake() { _ = this; }


        [SerializeField] private ActorPool primary;             // Editor configurable.
        [SerializeField] private ActorPool secondary;           // Editor configurable.
        [SerializeField] private ActorPool tertiary;            // Editor configurable.
        [SerializeField] private ActorPool quaternary;          // Editor configurable.

        /// <summary>
        /// Activates a valid actor waiting in the correct pool, or instantiates a new one if possible.
        /// </summary>
        /// <param name="parentVelocity">The velocity of the parent at the moment of spawning.</param>
        /// <param name="actorPrefab">The actor to be spawned.</param>
        /// <param name="transform">The transform of the parent at the moemnt of spawning.</param>
        /// <param name="momentum">How the child will use the parent velocity and its own generated velocity.</param>
        /// <param name="spawnPool">The spawn pool this child should exist in.</param>
        /// <returns>The activated or new actor.</returns>
        public static Actor Instantiate(Vector2 parentVelocity, Actor actorPrefab, Transform transform, HandleMomentum momentum = HandleMomentum.OVERRIDE, SpawnPool spawnPool = SpawnPool.PRIMARY)
        {
            return Instantiate(parentVelocity, actorPrefab, transform.position, transform.rotation, momentum, spawnPool);
        }

        /// <summary>
        /// Activates a valid actor waiting in the correct pool, or instantiates a new one if possible.
        /// </summary>
        /// <param name="parentVelocity">The velocity of the parent at the moment of spawning.</param>
        /// <param name="actorPrefab">The actor to be spawned.</param>
        /// <param name="position">The position to be spawned at.</param>
        /// <param name="quaternion">The rotation to be spawned at.</param>
        /// <param name="momentum">How the child will use the parent velocity and its own generated velocity.</param>
        /// <param name="spawnPool">The spawn pool this child should exist in.</param>
        /// <returns>The activated or new actor.</returns>
        public static Actor Instantiate(Vector2 parentVelocity, Actor actorPrefab, Vector3 position, Quaternion quaternion, HandleMomentum momentum, SpawnPool spawnPool = SpawnPool.PRIMARY)
        {

            bool isOkToInstantiate = false;
            Actor actorInPool = (spawnPool == SpawnPool.PRIMARY) ? _.primary.GetWaitingActorByPrefabId(actorPrefab.FilterData.PrefabId, out isOkToInstantiate) :
                                (spawnPool == SpawnPool.SECONDARY) ? _.secondary.GetWaitingActorByPrefabId(actorPrefab.FilterData.PrefabId, out isOkToInstantiate) :
                                (spawnPool == SpawnPool.TERTIARY) ? _.tertiary.GetWaitingActorByPrefabId(actorPrefab.FilterData.PrefabId, out isOkToInstantiate) :
                                (spawnPool == SpawnPool.QUATERNARY) ? _.quaternary.GetWaitingActorByPrefabId(actorPrefab.FilterData.PrefabId, out isOkToInstantiate) : null;

            // None found - try to instantiate one.
            if (!actorInPool)
            {
                // Early Out - No room for a new one.
                if (!isOkToInstantiate)
                    return null;

                Actor actor = GameObject.Instantiate(actorPrefab, position, quaternion);
                actor.SetSpawnPool(spawnPool);
                SpawnWithMotion spawnWithMotion = actor.GetComponent<SpawnWithMotion>();
                if (spawnWithMotion)
                    spawnWithMotion.SetHandleMomentum(momentum);

                spawnWithMotion.RecordParentVelocity(parentVelocity);

                if (spawnPool == SpawnPool.PRIMARY) _.primary.Add(actor);
                else if (spawnPool == SpawnPool.SECONDARY) _.secondary.Add(actor);
                else if (spawnPool == SpawnPool.TERTIARY) _.tertiary.Add(actor);
                else if (spawnPool == SpawnPool.QUATERNARY) _.quaternary.Add(actor);

                return actor;
            }

            // Waiting Actor found.
            actorInPool.ActivateInPool(position, quaternion);
            return actorInPool;
        }
    }
}