using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Catalogs;

namespace SpaceMage.Missions
{
    [System.Serializable]
    public class SpawnSettings
    {
        private static int nextId = 0;
        public static int GenerateUniqueId()
        {
            int id = nextId++;
            return id;
        }

        [SerializeField] private string uniqueId;
        [SerializeField] private Catalog catalog;
        [SerializeField] private FilterData filterData;
        public FilterData FilterData { get { return filterData; } }

        [SerializeField] private int numberToGet;
        [SerializeField] private float spawnDelay;
        [SerializeField] private float spawnDelayMultiplier;
        [SerializeField] private bool isEachUnique;
        public Catalog Catalog { get { return catalog; } }
        public int NumberToGet { get { return numberToGet; } }
        public float SpawnDelay { get { return spawnDelay; } }
        public float SpawnDelayMultiplier { get { return spawnDelayMultiplier; } }
        public string UniqueId { get { return uniqueId; } }
        public bool IsEachUnique { get { return isEachUnique; } }

        private bool isSpawning;
        public bool IsSpawning { get { return isSpawning; } }
        public void SetUniqueId(string uniqueId) { this.uniqueId = uniqueId; }
        public void ToggleSpawning(bool state) { isSpawning = state; }
        public void SetSpawnDelayMultiplier(float amount) { spawnDelayMultiplier = Mathf.Min(0, amount); }
    }
}