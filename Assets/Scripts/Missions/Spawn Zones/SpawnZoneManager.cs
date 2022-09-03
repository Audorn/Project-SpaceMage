using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class SpawnZoneManager : MonoBehaviour
    {
        // Singleton allowing access to the game state handler through the static class.
        private static SpawnZoneManager _;
        public static SpawnZoneManager Singleton { get { return _; } }
        private void Awake() { _ = this; }


        [SerializeField] private List<SpawnZone> spawnZones;

        public static List<SpawnZone> SpawnZones { get { return SpawnZones; } }
        public static void AddSpawnZone(SpawnZone spawnZone) { _.spawnZones.Add(spawnZone); }

        // ====================================================
        // ==================== GENERATION ====================
        // ====================================================

        public static bool IsValidSpawnZoneLocation(Rect rect)
        {
            int numberOfSpawnZones = _.spawnZones.Count;
            for (int i = 0; i < numberOfSpawnZones; i++)
            {
                BoxCollider2D collider = _.spawnZones[i].GetComponent<BoxCollider2D>();
                Rect existingRect = new Rect(collider.transform.position.x, collider.transform.position.y, collider.size.x, collider.size.y);
                if (rect.Overlaps(existingRect))
                    return false;
            }

            return true;
        }

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