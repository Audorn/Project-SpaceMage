using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SpaceMage.Catalogs;
using SpaceMage.Entities;

namespace SpaceMage.Missions
{
    [RequireComponent(typeof(Collider2D))]
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

            trackedColliders = GetComponent<ColliderContainer>();
        }

        // ==================================================
        // ==================== SETTINGS ====================
        // ==================================================
        [SerializeField] private bool isActive = false;
        [SerializeField] private Vector2 area;
        [SerializeField] private int maxTries = 5;
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

            StartCoroutine(spawnHazardsFromSpawnSettings());
        }

        private IEnumerator spawnHazardsFromSpawnSettings()
        {
            for (int i = 0; i < numberOfSpawnSettings; i++)
            {
                if (spawnSettings[i].IsSpawning)
                    continue;

                if (spawnSettings[i].Catalog == Catalog.HAZARD)
                    StartCoroutine(spawnHazards(spawnSettings[i].SpawnDelay, spawnSettings[i]));

                // TODO: Spawn from other catalogs.

                yield return new WaitForFixedUpdate();
            }
        }

        private ColliderContainer trackedColliders;
        private IEnumerator spawnHazards(float spawnDelay, SpawnSettings spawnSettings)
        {
            List<Hazard> hazards = HazardCatalog.GetHazards(spawnSettings);

            spawnSettings.ToggleSpawning(true);
            foreach (Hazard hazard in hazards)
            {
                // Respect sprite width and height within spawn zone if possible.
                SpriteRenderer spriteRenderer = hazard.GetComponent<SpriteRenderer>();
                Vector2 spriteDimensionsHalved = new Vector2(spriteRenderer.bounds.size.x / 2, spriteRenderer.bounds.size.y / 2);

                // Try to find a place for the hazard to instantiate without overlapping other objects.
                Vector2 position = Vector2.zero;
                for (int i = 0; i < maxTries; i++)
                {
                    float x = Random.Range(transform.position.x + spriteDimensionsHalved.x, maxX - spriteDimensionsHalved.x);
                    float y = Random.Range(transform.position.y + spriteDimensionsHalved.y, maxY - spriteDimensionsHalved.y);
                    Vector2 possiblePosition = new Vector2(x, y);

                    if (trackedColliders.Colliders.Count == 0)
                    {
                        position = possiblePosition;
                        break;
                    }

                    // Other objects are present. Find the center of this one and then see if it overlaps any of them.
                    Vector2 center = new Vector2(x + spriteDimensionsHalved.x, y + spriteDimensionsHalved.y);

                    // Make sure that hazards do not spawn in overlapping other objects.
                    bool isValidPosition = true;
                    foreach (Collider2D collider in trackedColliders.Colliders)
                    {
                        SpriteRenderer trackedSpriteRenderer = collider.GetComponent<SpriteRenderer>();

                        Vector2 trackedDimensionsHalved = new Vector2(trackedSpriteRenderer.bounds.size.x / 2, trackedSpriteRenderer.bounds.size.y / 2);
                        Vector2 trackedPosition = collider.transform.position;
                        Vector2 trackedCenter = new Vector2(trackedPosition.x + trackedDimensionsHalved.x, trackedPosition.y + trackedDimensionsHalved.y);

                        Vector2 distance = new Vector2(Mathf.Abs(center.x - trackedCenter.x), Mathf.Abs(center.y - trackedCenter.y));
                        Vector2 requiredDistance = new Vector2(spriteDimensionsHalved.x + trackedDimensionsHalved.x, spriteDimensionsHalved.y + trackedDimensionsHalved.y);

                        if (distance.x < requiredDistance.x || distance.y < requiredDistance.y)
                            isValidPosition = false;
                    }

                    if (isValidPosition)
                        position = possiblePosition;
                }

                // A valid position was found, create the new object.
                if (position != Vector2.zero)
                {
                    // TODO: Allow to override starting rotation (not torque, but how the object is rotated when instantiated).
                    Hazard h = (Hazard)ActorManager.Instantiate(hazard, position, Quaternion.identity);
                    if (h && overridePrefabSpawnMotion)
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