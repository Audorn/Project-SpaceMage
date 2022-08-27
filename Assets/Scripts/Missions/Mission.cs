using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class Mission : MonoBehaviour
    {
        // ==================================================
        // ==================== SETTINGS ====================
        // ==================================================
        [SerializeField] private MissionContent missionContent;
        [SerializeField] private Vector2Int mapSize;
        [SerializeField] private Vector2 playerSpawnPoint;
        [SerializeField] private List<SpawnZone> spawnZones;

        public MissionContent MissionContent { get { return missionContent; } }
        public Vector2 MapSize { get { return mapSize; } }
        public Vector2 PlayerSpawnPoint { get { return playerSpawnPoint; } }
        public List<SpawnZone> SpawnZones { get { return spawnZones; } }

        public void SetMissionContent(MissionContent missionContent) { this.missionContent = missionContent; }
        public void SetMapSize(Vector2Int mapSize) { this.mapSize = mapSize; }

        public bool HasPrimaryModifier(PrimaryModifier modifier) { return missionContent.PrimaryModifiers.Contains(modifier); }
        public bool HasSecondaryModifier(SecondaryModifier modifier) { return missionContent.SecondaryModifiers.Contains(modifier); }

        // ====================================================
        // ==================== GENERATION ====================
        // ====================================================
        public void ToggleSpawnZones(bool state, List<string> ids = null)
        {
            if (ids == null)
            {
                toggleAllSpawnZones(state);
                return;
            }

            int numberOfSpawnZones = spawnZones.Count;
            int numberOfIds = ids.Count;
            for (int i = 0; i < numberOfSpawnZones; i++)
            {
                for (int j = 0; j < numberOfIds; j++)
                {
                    if (spawnZones[i].UniqueId == ids[j])
                        spawnZones[i].Toggle(state);
                }
            }
        }

        private void toggleAllSpawnZones(bool state)
        {
            int numberOfSpawnZones = spawnZones.Count;
            for (int i = 0; i < numberOfSpawnZones; i++)
                spawnZones[i].Toggle(state);
        }

        public void SetSpawnDelayMultipliers(float amount, List<string> spawnZoneIds = null, List<string> spawnSettingIds = null)
        {
            if (spawnZoneIds == null)
            {
                setAllSpawnZoneSpawnDelayMultipliers(amount, spawnSettingIds);
                return;
            }

            int numberOfSpawnZones = spawnZones.Count;
            int numberOfIds = spawnZoneIds.Count;
            for (int i = 0; i < numberOfSpawnZones; i++)
            {
                for (int j = 0; j < numberOfIds; j++)
                {
                    if (spawnZones[i].UniqueId == spawnZoneIds[j])
                        spawnZones[i].SetSpawnSettingsSpawnDelayMultipliers(amount, spawnSettingIds);
                }
            }

        }

        private void setAllSpawnZoneSpawnDelayMultipliers(float amount, List<string> spawnSettingIds = null)
        {
            if (spawnSettingIds == null)
            {
                int numberOfSpawnZones = spawnZones.Count;
                for (int i = 0; i < numberOfSpawnZones; i++)
                    spawnZones[i].SetAllSpawnSettingsSpawnDelayMultipliers(amount);
            }
        }
    }
}