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
        [SerializeField] private Vector2 mapSize;
        [SerializeField] private Vector2 playerSpawnPoint;

        public MissionContent MissionContent { get { return missionContent; } }
        public Vector2 MapSize { get { return mapSize; } }
        public Vector2 PlayerSpawnPoint { get { return playerSpawnPoint; } }

        public void SetMissionContent(MissionContent missionContent) { this.missionContent = missionContent; }
        public void SetMapSize(Vector2 mapSize) { this.mapSize = mapSize; }
        public void SetPlayerSpawnPoint(Vector2 playerSpawnPoint) { this.playerSpawnPoint = playerSpawnPoint; }

        public bool HasPrimaryModifier(PrimaryModifier modifier) { return missionContent.PrimaryModifiers.Contains(modifier); }
        public bool HasSecondaryModifier(SecondaryModifier modifier) { return missionContent.SecondaryModifiers.Contains(modifier); }
    }
}