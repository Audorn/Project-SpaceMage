using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Extensions;
using SpaceMage.Missions;

namespace SpaceMage.Entities
{
    public class ActorSpawner : MonoBehaviour
    {
        // ========== STATIC MANAGER ==========
        /// <summary>
        /// Handles spawning for objects that might get destroyed after initiating the spawn request.
        /// </summary>
        /// <param name="settings">The list of everything this actor can spawn.</param>
        /// <param name="spawnEvent">The type of event that just occurred.</param>
        public static void SpawnActors(Vector2 parentVelocity, List<ActorSpawnSettings> settings, SpawnEvent spawnEvent, SpawnPool poolToSpawnIn)
        {
            SpawnZoneManager.Singleton.StartCoroutine(_spawnActors(parentVelocity, settings, spawnEvent, poolToSpawnIn, new PersistentFlag(true)));
        }

        /// <summary>
        /// Coroutine that passes each ActorSpawnSettings that should spawn on this spawn event to another coroutine that filters them.
        /// </summary>
        /// <param name="settings">The list of everything this actor can spawn.</param>
        /// <param name="spawnEvent">The type of even that just occurred.</param>
        /// <param name="safeToSpawn">Allows spawning to coordinate so that no spawn from the same source can occur on the same frame.</param>
        /// <returns></returns>
        private static IEnumerator _spawnActors(Vector2 parentVelocity, List<ActorSpawnSettings> settings, SpawnEvent spawnEvent, SpawnPool poolToSpawnIn, PersistentFlag safeToSpawn)
        {
            int numberOfSettings = settings.Count;
            for (int i = 0; i < numberOfSettings; i++)
            {
                // Early out - wrong event for this type of spawn.
                if (spawnEvent != settings[i].SpawnEvent)
                    continue;

                SpawnZoneManager.Singleton.StartCoroutine(_spawnActors(parentVelocity, settings[i], poolToSpawnIn, safeToSpawn));
                yield return new WaitForFixedUpdate();
            }
        }

        /// <summary>
        /// Coroutine that starts the spawn coroutine for a specific ActorSpawnSettings that should start spawning.
        /// </summary>
        /// <param name="settings">The ActorSpawnSettings that should spawn now.</param>
        /// <param name="safeToSpawn">Allows spawning to coordinate so that no spawn from the same source can occur on the same frame.</param>
        /// <returns></returns>
        private static IEnumerator _spawnActors(Vector2 parentVelocity, ActorSpawnSettings settings, SpawnPool poolToSpawnIn, PersistentFlag safeToSpawn)
        {
            // Nothing to spawn.
            if (settings.Actors.Count == 0)
                yield break;

            // Wait for any start delay.
            if (settings.StartDelay > 0)
                yield return new WaitForSeconds(settings.StartDelay);

            // Always execute once. Allow for iterations.
            int iterations = Mathf.Max(settings.Iterations, 1);
            for (int i = 0; i < iterations; i++)
            {
                int numberOfSpawnSets = Mathf.Min(settings.NumberToSpawn.Count, settings.Actors.Count);
                SpawnZoneManager.Singleton.StartCoroutine(spawn(parentVelocity, settings, i, poolToSpawnIn, safeToSpawn));
                yield return new WaitForFixedUpdate();
            }
        }

        /// <summary>
        /// Coroutine that spawns a specific number of one type of Actor.
        /// </summary>
        /// <param name="settings">The ActorSpawnSettings that are being spawned from.</param>
        /// <param name="index">The index of the list of Actor and NumberToSpawn lists.</param>
        /// <param name="safeToSpawn">Allows spawning to coordinate so that no spawn from the same source can occur on the same frame.</param>
        /// <returns></returns>
        private static IEnumerator spawn(Vector2 parentVelocity, ActorSpawnSettings settings, int index, SpawnPool poolToSpawnIn, PersistentFlag safeToSpawn)
        {
            int numberToSpawn = settings.NumberToSpawn[index];
            Actor actorPrefab = settings.Actors[index];
            bool isSpawning = false;
            for (int i = 0; i < numberToSpawn; i++)
            {
                // Holding Loop - waiting for a chance to spawn.
                while (!isSpawning && safeToSpawn.Flag == false)
                    yield return new WaitForFixedUpdate();

                isSpawning = true;
                safeToSpawn.SetFlag(false);

                Actor actor = ActorManager.Instantiate(parentVelocity, actorPrefab, settings.SpawnAtTransform, settings.Momentum, poolToSpawnIn);
                if (actor)
                    actor.SetSpawnPool(poolToSpawnIn);

                yield return new WaitForSeconds(settings.DelayAfterSpawn);
            }

            isSpawning = false;
            safeToSpawn.SetFlag(true);
        }
        // ========== END STATIC MANAGER ==========

        // ---------- COMPONENT ----------
        private SpawnPool poolToSpawnIn = SpawnPool.PRIMARY;

        public SpawnPool PoolToSpawnIn { get { return poolToSpawnIn; } }
        public void SetPoolToSpawnIn(SpawnPool poolToSpawnIn) { this.poolToSpawnIn = poolToSpawnIn; }

        [SerializeField] private List<ActorSpawnSettings> actorSpawnSettings = new List<ActorSpawnSettings>();

        public List<ActorSpawnSettings> ActorSpawnSettings { get { return actorSpawnSettings; } }

        private void Start()
        {
            int numberOfSettings = actorSpawnSettings.Count;
            for (int i = 0; i < numberOfSettings; i++)
            {
                actorSpawnSettings[i].SetTransform(transform);
                Actor actor = GetComponent<Actor>();
                if (actor)
                    poolToSpawnIn = actor.SpawnPool.Next();
            }
        }
        // ---------- END COMPONENT ----------
    }
}