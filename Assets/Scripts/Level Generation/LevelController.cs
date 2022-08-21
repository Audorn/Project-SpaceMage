using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.LevelGeneration
{
    public class LevelController : MonoBehaviour
    {
        // Singleton allowing access to the game state handler through the static class.
        private static LevelController _;
        public static LevelController Singleton { get { return _; } }
        private void Awake() 
        { 
            _ = this;
            spawnZones.AddRange(GetComponentsInChildren<SpawnZone>());
        }

        // ==================================================
        // ==================== SETTINGS ====================
        // ==================================================
        [SerializeField] private Rect levelSize;
        [SerializeField] private List<SpawnZone> spawnZones;
        public Rect LevelSize { get { return levelSize; } }
        public List<SpawnZone> SpawnZones { get { return spawnZones; } }

        // ====================================================
        // ==================== GENERATION ====================
        // ====================================================
        public static void ToggleSpawnZones(bool state, List<string> ids = null)
        {
            if (ids == null)
            {
                toggleAllSpawnZones(state);
                return;
            }

            int numberOfSpawnZones = _.spawnZones.Count;
            int numberOfIds = ids.Count;
            for (int i = 0; i < numberOfSpawnZones; i++)
            {
                for (int j = 0; j < numberOfIds; j++)
                {
                    if (_.spawnZones[i].UniqueId == ids[j])
                        _.spawnZones[i].Toggle(state);
                }
            }
        }

        private static void toggleAllSpawnZones(bool state)
        {
            int numberOfSpawnZones = _.spawnZones.Count;
            for (int i = 0; i < numberOfSpawnZones; i++)
                _.spawnZones[i].Toggle(state);
        }

        public static void SetSpawnDelayMultipliers(float amount, List<string> spawnZoneIds = null, List<string> spawnSettingIds = null)
        {
            if (spawnZoneIds == null)
            {
                setAllSpawnZoneSpawnDelayMultipliers(amount, spawnSettingIds);
                return;
            }

            int numberOfSpawnZones = _.spawnZones.Count;
            int numberOfIds = spawnZoneIds.Count;
            for (int i = 0; i < numberOfSpawnZones; i++)
            {
                for (int j = 0; j < numberOfIds; j++)
                {
                    if (_.spawnZones[i].UniqueId == spawnZoneIds[j])
                        _.spawnZones[i].SetSpawnSettingsSpawnDelayMultipliers(amount, spawnSettingIds);
                }
            }

        }

        private static void setAllSpawnZoneSpawnDelayMultipliers(float amount, List<string> spawnSettingIds = null)
        {
            if (spawnSettingIds == null)
            {
                int numberOfSpawnZones = _.spawnZones.Count;
                for (int i = 0; i < numberOfSpawnZones; i++)
                    _.spawnZones[i].SetAllSpawnSettingsSpawnDelayMultipliers(amount);
            }
        }
    }
}