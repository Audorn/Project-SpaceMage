using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [System.Serializable]
    public class ActorSpawnSettings
    {
        private Transform transform;
        private SpawnPool parentSpawnPool;

        [SerializeField] private SpawnEvent spawnEvent;
        [SerializeField] private float startDelay;
        [SerializeField] private float delayAfterSpawn;
        [SerializeField] private int iterations = 1;
        [SerializeField] private List<int> numberToSpawn;
        [SerializeField] private List<Actor> actors;

        public Transform SpawnAtTransform { get { return transform; } }
        public SpawnPool ParentSpawnPool { get { return parentSpawnPool; } }
        public SpawnEvent SpawnEvent { get { return spawnEvent; } }
        public float StartDelay { get { return startDelay; } }
        public float DelayAfterSpawn { get { return delayAfterSpawn; } }
        public int Iterations { get { return iterations; } }
        public List<int> NumberToSpawn { get { return numberToSpawn; } }
        public List<Actor> Actors { get { return actors; } }

        public void SetTransform(Transform transform) { this.transform = transform; }
        public void SetParentSpawnPool(SpawnPool parentSpawnPool) { this.parentSpawnPool = parentSpawnPool; }
    }
}