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
        [SerializeField] private List<GameObject> setpiecePrefabs;
        [SerializeField] private List<GameObject> hazardPrefabs;
        [SerializeField] private List<MapSize> missionSizes;
        [SerializeField] private List<int> missionSizeInUnits;

        public static GameObject PlayerPrefab { get { return _.playerPrefab; } }
        public static GameObject MissionContentPrefab { get { return _.missionContentPrefab; } }
        public static GameObject MissionPrefab { get { return _.missionPrefab; } }
        public static List<GameObject> SetpiecePrefabs { get { return _.setpiecePrefabs; } }
        public static List<GameObject> HazardPrefabs { get { return _.hazardPrefabs; } }
        public static int MapSizeInUnits(MapSize missionSize)
        {
            // Early out - Mission sizes or sizes in units not specified.
            if (_.missionSizes.Count == 0 || _.missionSizeInUnits.Count == 0)
                return 1;

            int index = _.missionSizes.FindIndex(size => size == missionSize);

            // Early out - Mission sizes and sizes in units length do not match.
            if (index >= _.missionSizeInUnits.Count)
                return 1;

            return _.missionSizeInUnits[index];
        }
    }
}