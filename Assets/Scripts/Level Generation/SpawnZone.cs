using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SpaceMage.Catalogs;
using SpaceMage.Entities;

namespace SpaceMage.LevelGeneration
{
    public class SpawnZone : MonoBehaviour
    {
        private static int nextId = 0;
        private static int GenerateId() 
        {
            int id = nextId++;
            return id;
        }

        [SerializeField] private string uniqueId;
        public string UniqueId { get { return uniqueId; } }
        private void Awake()
        {
            if (uniqueId == "")
                uniqueId = GenerateId().ToString();

            numberOfSpawnSettings = spawnSettings.Count;
            for (int i = 0; i < numberOfSpawnSettings; i++)
            {
                if (spawnSettings[i].UniqueId == "")
                    spawnSettings[i].SetUniqueId(SpawnSettings.GenerateUniqueId().ToString());
            }
        }

        // ==================================================
        // ==================== SETTINGS ====================
        // ==================================================
        [SerializeField] private bool isActive = false;
        [SerializeField] private Vector2 area;
        [SerializeField] private bool overridePrefabSpawnMotion;
        [SerializeField] private Vector2 maxDirection;
        [SerializeField] private Vector2 minDirection;
        [SerializeField] private float velocityModifier;
        [SerializeField] private float rotationModifier;
        [SerializeField] private List<SpawnSettings> spawnSettings;

        public Vector2 Area { get { return area; } }

        public void Toggle(bool state) { isActive = state; }
        public void SetAllSpawnSettingsSpawnDelayMultipliers(float amount)
        {
            int numberOfSpawnSettings = spawnSettings.Count;
            for (int i = 0; i < numberOfSpawnSettings; i++)
                spawnSettings[i].SetSpawnDelayMultiplier(amount);
        }
        public void SetSpawnSettingsSpawnDelayMultipliers(float amount, List<string> spawnSettingsIds)
        {
            int numberOfSpawnSettingsIds = spawnSettingsIds.Count;
            for (int i = 0; i < numberOfSpawnSettings; i++)
            {
                for (int j = 0; j < numberOfSpawnSettingsIds; j++)
                {
                    if (spawnSettings[i].UniqueId == spawnSettingsIds[j])
                    {
                        spawnSettings[i].SetSpawnDelayMultiplier(amount);
                        break;
                    }
                }
            }
        }

        private int numberOfSpawnSettings;
        private float maxX;
        private float maxY;
        private void FixedUpdate()
        {
            if (!isActive)
                return;

            for (int i = 0; i < numberOfSpawnSettings; i++)
            {
                if (spawnSettings[i].IsSpawning)
                    continue;

                if (spawnSettings[i].Catalog == Catalog.HAZARD)
                    StartCoroutine(spawnHazards(spawnSettings[i].SpawnDelay, spawnSettings[i]));
                // TODO: Spawn from other catalogs.
            }
        }

        private IEnumerator spawnHazards(float spawnDelay, SpawnSettings spawnSettings)
        {
            List<Hazard> hazards = HazardCatalog.GetHazards(spawnSettings);

            spawnSettings.ToggleSpawning(true);
            foreach (Hazard hazard in hazards)
            {
                // TODO: Respect sprite width and height within spawn zone if possible.
                // TODO: Make sure that hazards do not spawn in overlapping other objects.
                float x = Random.Range(transform.position.x, maxX);
                float y = Random.Range(transform.position.y, maxY);
                Vector2 position = new Vector2(x, y);

                Hazard h = Instantiate(hazard, position, Quaternion.identity);
                if (overridePrefabSpawnMotion)
                {
                    SpawnWithMotion spawnWithMotion = h.GetComponent<SpawnWithMotion>();

                    if (spawnWithMotion)
                    {
                        spawnWithMotion.SetMinDirection(minDirection);
                        spawnWithMotion.SetMaxDirection(maxDirection);
                        spawnWithMotion.ModifyVelocity(velocityModifier);
                        spawnWithMotion.ModifyRotation(rotationModifier);
                    }
                }

                yield return new WaitForSeconds(spawnDelay);
            }
            spawnSettings.ToggleSpawning(false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawWireCube(new Vector3(transform.position.x + area.x / 2, transform.position.y + area.y / 2, 0), new Vector3(area.x, area.y, 0));
        }

        private void Start()
        {
            numberOfSpawnSettings = spawnSettings.Count;
            maxX = transform.position.x + area.x;
            maxY = transform.position.y + area.y;
        }
    }
}