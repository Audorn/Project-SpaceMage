using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    /// <summary>
    /// Contains and handles ActorPools.
    /// </summary>
    public class ActorManager : MonoBehaviour
    {
        // Singleton.
        private static ActorManager _;
        public static ActorManager Singleton { get { return _; } }
        private void Awake() { _ = this; }


        [SerializeField] private ActorPool primary;
        [SerializeField] private ActorPool secondary;
        [SerializeField] private ActorPool tertiary;
        [SerializeField] private ActorPool quaternary;

        public static Actor Instantiate(Vector2 parentVelocity, Actor actorPrefab, Transform transform, Momentum momentum = Momentum.OVERRIDE, SpawnPool spawnPool = SpawnPool.PRIMARY)
        {
            return Instantiate(parentVelocity, actorPrefab, transform.position, transform.rotation, momentum, spawnPool);
        }
        public static Actor Instantiate(Vector2 parentVelocity, Actor actorPrefab, Vector3 position, Quaternion quaternion, Momentum momentum, SpawnPool spawnPool = SpawnPool.PRIMARY)
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
                    spawnWithMotion.SetMomentum(momentum);

                Debug.LogWarning($"Parent rigid body velocity {parentVelocity}");
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