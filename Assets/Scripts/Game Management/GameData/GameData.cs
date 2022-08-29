using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Missions;

namespace SpaceMage.Data
{
    public class GameData : MonoBehaviour
    {
        // Singleton
        private static GameData _;
        public static GameData Singleton { get { return _; } }
        private void Awake() { _ = this; }

        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject missionContentPrefab;
        [SerializeField] private GameObject missionPrefab;
        [SerializeField] private GameObject mapEdgePrefab;
        [SerializeField] private int defaultObstacleThickness;
        [SerializeField] private int defaultSplitMapGapThickness;
        [SerializeField] private GameObject spawnZonePrefab;

        public static GameObject PlayerPrefab { get { return _.playerPrefab; } }
        public static GameObject MissionContentPrefab { get { return _.missionContentPrefab; } }
        public static GameObject MissionPrefab { get { return _.missionPrefab; } }
        public static GameObject SpawnZonePrefab { get { return _.spawnZonePrefab; } }
        public static int DefaultObstacleThickness { get { return _.defaultObstacleThickness; } }
        public static int DefaultSplitMapGapThickness { get { return _.defaultSplitMapGapThickness; } }
        public static GameObject MapEdgePrefab { get { return _.mapEdgePrefab; } }
    }
}