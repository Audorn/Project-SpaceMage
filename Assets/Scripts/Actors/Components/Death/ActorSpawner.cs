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
        public static void SpawnActors(List<ActorSpawnSettings> settings, SpawnEvent spawnEvent)
        {
            SpawnZoneManager.Singleton.StartCoroutine(_spawnActors(settings, spawnEvent, new PersistentFlag(true)));
        }

        /// <summary>
        /// Coroutine that passes each ActorSpawnSettings that should spawn on this spawn event to another coroutine that filters them.
        /// </summary>
        /// <param name="settings">The list of everything this actor can spawn.</param>
        /// <param name="spawnEvent">The type of even that just occurred.</param>
        /// <param name="safeToSpawn">Allows spawning to coordinate so that no spawn from the same source can occur on the same frame.</param>
        /// <returns></returns>
        private static IEnumerator _spawnActors(List<ActorSpawnSettings> settings, SpawnEvent spawnEvent, PersistentFlag safeToSpawn)
        {
            int numberOfSettings = settings.Count;
            for (int i = 0; i < numberOfSettings; i++)
            {
                // Early out - wrong event for this type of spawn.
                if (spawnEvent != settings[i].SpawnEvent)
                    continue;

                SpawnZoneManager.Singleton.StartCoroutine(_spawnActors(settings[i], safeToSpawn));
                yield return new WaitForFixedUpdate();
            }
        }

        /// <summary>
        /// Coroutine that starts the spawn coroutine for a specific ActorSpawnSettings that should start spawning.
        /// </summary>
        /// <param name="settings">The ActorSpawnSettings that should spawn now.</param>
        /// <param name="safeToSpawn">Allows spawning to coordinate so that no spawn from the same source can occur on the same frame.</param>
        /// <returns></returns>
        private static IEnumerator _spawnActors(ActorSpawnSettings settings, PersistentFlag safeToSpawn)
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
                SpawnPool spawnPool = settings.ParentSpawnPool.Next(false);
                SpawnZoneManager.Singleton.StartCoroutine(spawn(settings, i, spawnPool, safeToSpawn));
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
        private static IEnumerator spawn(ActorSpawnSettings settings, int index, SpawnPool spawnPool, PersistentFlag safeToSpawn)
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

                Actor actor = ActorManager.Instantiate(actorPrefab, settings.SpawnAtTransform, spawnPool);
                if (actor)
                    actor.SetSpawnPool(spawnPool);
                yield return new WaitForSeconds(settings.DelayAfterSpawn);
            }

            isSpawning = false;
            safeToSpawn.SetFlag(true);
        }
        // ========== END STATIC MANAGER ==========

        // ---------- COMPONENT ----------
        private SpawnPool spawnPool = SpawnPool.PRIMARY;

        public SpawnPool SpawnPool { get { return spawnPool; } }
        public void SetSpawnPool(SpawnPool spawnPool) { this.spawnPool = spawnPool; }

        [SerializeField] private List<ActorSpawnSettings> actorSpawnSettings = new List<ActorSpawnSettings>();

        public List<ActorSpawnSettings> ActorSpawnSettings { get { return actorSpawnSettings; } }

        private void Awake()
        {
            int numberOfSettings = actorSpawnSettings.Count;
            for (int i = 0; i < numberOfSettings; i++)
            {
                actorSpawnSettings[i].SetTransform(transform);
                actorSpawnSettings[i].SetParentSpawnPool(spawnPool);
            }
        }
        // ---------- END COMPONENT ----------
    }
}