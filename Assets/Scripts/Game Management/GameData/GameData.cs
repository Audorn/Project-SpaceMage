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
        [SerializeField] private List<MapSize> mapSizes;
        [SerializeField] private List<int> mapSizeInUnits;
        [SerializeField] private int boundaryRegionThickness;
        [SerializeField] private int defaultObstacleThickness;
        [SerializeField] private GameObject boundaryPrefab;
        [SerializeField] private GameObject mapEdgePrefab;
        [SerializeField] private GameObject spawnZonePrefab;
        [SerializeField] private int squareUnitsPerSpawnZone;

        public static GameObject PlayerPrefab { get { return _.playerPrefab; } }
        public static GameObject MissionContentPrefab { get { return _.missionContentPrefab; } }
        public static GameObject MissionPrefab { get { return _.missionPrefab; } }
        public static GameObject SpawnZonePrefab { get { return _.spawnZonePrefab; } }
        public static int SquareUnitsPerSpawnZone { get { return _.squareUnitsPerSpawnZone; } }
        public static int MapSizeInUnits(MapSize mapSize)
        {
            // Early out - Mission sizes or sizes in units not specified.
            if (_.mapSizes.Count == 0 || _.mapSizeInUnits.Count == 0)
                return 1;

            int index = _.mapSizes.FindIndex(size => size == mapSize);

            // Early out - Mission sizes and sizes in units length do not match.
            if (index >= _.mapSizeInUnits.Count)
                return 1;

            return _.mapSizeInUnits[index];
        }
        public static int BoundaryRegionThickness { get { return _.boundaryRegionThickness; } }
        public static int DefaultObstacleThickness { get { return _.defaultObstacleThickness; } }
        public static GameObject BoundaryPrefab { get { return _.boundaryPrefab; } }
        public static GameObject MapEdgePrefab { get { return _.mapEdgePrefab; } }
    }
}