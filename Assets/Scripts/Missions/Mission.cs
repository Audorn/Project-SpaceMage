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
        [SerializeField] private RectInt boundary;
        [SerializeField] private Vector2 playerSpawnPoint;

        public MissionContent MissionContent { get { return missionContent; } }
        public Vector2Int MapSize { get { return mapSize; } }
        public RectInt Boundary { get { return boundary; } }
        public Vector2 PlayerSpawnPoint { get { return playerSpawnPoint; } }

        public void SetMissionContent(MissionContent missionContent) { this.missionContent = missionContent; }
        public void SetMapSize(Vector2Int mapSize) { this.mapSize = mapSize; }
        public void SetBoundary(RectInt boundary) { this.boundary = boundary; }

        public bool HasPrimaryModifier(PrimaryModifier modifier) { return missionContent.PrimaryModifiers.Contains(modifier); }
        public bool HasSecondaryModifier(SecondaryModifier modifier) { return missionContent.SecondaryModifiers.Contains(modifier); }
    }
}