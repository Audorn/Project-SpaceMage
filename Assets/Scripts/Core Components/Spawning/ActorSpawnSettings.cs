using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Actors
{
    /// <summary>
    /// Allows configuring how, when, and which actors this object spawns.
    /// </summary>
    [System.Serializable]
    public class ActorSpawnSettings
    {
        private Transform transform;                        // Set in ActorSpawner Start().

        [SerializeField] private SpawnEvent spawnEvent;     // Editor configurable. When does this spawn occur?
        [SerializeField] private HandleMomentum momentum;   // Editor configurable. How does this spawn handle parent velocity/rotation?
        [SerializeField] private float startDelay;          // Editor configurable.
        [SerializeField] private float delayAfterSpawn;     // Editor configurable. How long between individual actors?
        [SerializeField] private int iterations = 1;        // Editor configurable. How many times does this fire?
        [SerializeField] private List<int> numberToSpawn;   // Editor configurable. How many actors get spawned each time?
        [SerializeField] private List<Actor> actors;        // Editor configurable. Which actors get spawned?

        public Transform SpawnAtTransform => transform;
        public HandleMomentum Momentum => momentum;
        public SpawnEvent SpawnEvent => spawnEvent;
        public float StartDelay => startDelay;
        public float DelayAfterSpawn => delayAfterSpawn;
        public int Iterations => iterations;
        public List<int> NumberToSpawn => numberToSpawn;
        public List<Actor> Actors => actors;

        public void SetTransform(Transform transform) => this.transform = transform;
        public void SetMomentum(HandleMomentum momentum) => this.momentum = momentum;
    }
}