using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [System.Serializable]
    public class ActorSpawnSettings
    {
        private Transform transform;

        [SerializeField] private SpawnEvent spawnEvent;
        [SerializeField] private Momentum momentum;
        [SerializeField] private float startDelay;
        [SerializeField] private float delayAfterSpawn;
        [SerializeField] private int iterations = 1;
        [SerializeField] private List<int> numberToSpawn;
        [SerializeField] private List<Actor> actors;

        public Transform SpawnAtTransform => transform;
        public Momentum Momentum => momentum;
        public SpawnEvent SpawnEvent => spawnEvent;
        public float StartDelay => startDelay;
        public float DelayAfterSpawn => delayAfterSpawn;
        public int Iterations => iterations;
        public List<int> NumberToSpawn => numberToSpawn;
        public List<Actor> Actors => actors;

        public void SetTransform(Transform transform) => this.transform = transform;
        public void SetMomentum(Momentum momentum) => this.momentum = momentum;
    }
}