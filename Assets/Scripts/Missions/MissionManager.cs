using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceMage.Data;

namespace SpaceMage.Missions
{
    /// <summary>
    /// Missions are levels while they are being played. Levels are data sets and generation.
    /// </summary>
    public class MissionManager : MonoBehaviour
    {
        // Singleton.
        private static MissionManager _;
        public static MissionManager Singleton { get { return _; } }
        private void Awake() 
        { 
            _ = this;
            missionList = GetComponent<MissionList>();
        }

        // =========================================================
        // ==================== STATIC SETTINGS ====================
        // =========================================================
        [SerializeField] private static MissionList missionList;

        public static MissionList MissionList { get { return missionList; } }
        public static List<MissionPossibleContent> PossibleMissionContents { get { return _.possibleMissionContents; } }
        public static int selectedPossibleMissionContent = 0;
        public static MissionPossibleContent GetSelectedMissionPossibleContent() { return _.possibleMissionContents[selectedPossibleMissionContent]; }
        public static Mission CurrentMission { get { return missionList.CurrentMission; } }
        public static MissionContent CurrentMissionContent { get { return _.currentMissionContent; } }
        public static void SetCurrentMissionContent(MissionContent missionContent) { _.currentMissionContent = missionContent; }


        // ==================================================
        // ==================== SETTINGS ====================
        // ==================================================
        [SerializeField] private List<MissionPossibleContent> possibleMissionContents = new List<MissionPossibleContent>();
        [SerializeField] private MissionContent currentMissionContent;

        private void activateSpawnZones() { Debug.Log("Activating spawn zones."); SpawnZoneManager.ToggleSpawnZones(true); }
        private void createPlayer() 
        {
            GameObject playerPrefab = PrefabManager.PlayerPrefab;
            GameObject player = Instantiate(playerPrefab, missionList.CurrentMission.PlayerSpawnPoint, Quaternion.identity);
        }

        // Acts as a Start() when preserved between scenes.
        private static bool shouldStartMission = false;
        public static void PrepareToGenerateMission() { shouldStartMission = true; }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (shouldStartMission)
            {
                MissionGeneratorStateHandler.Singleton.Playing.AddListener(activateSpawnZones);
                MissionGeneratorStateHandler.Singleton.CreatingPlayer.AddListener(createPlayer);
                shouldStartMission = false;
                MissionGenerator.Initialize();
            }
            else
            {
                MissionGeneratorStateHandler.Singleton.CreatingPlayer.RemoveListener(activateSpawnZones);
                MissionGeneratorStateHandler.Singleton.CreatingPlayer.RemoveListener(createPlayer);
            }
        }
    }

    public enum MissionType
    {
        SURVIVAL,
        DEFENSE,
        EXPLORATION,
        EXPERIMENT,
        RECOVERY,
        REPAIR
    }

    public enum MapSize
    {
        TINY,
        SMALL,
        MEDIUM,
        LARGE,
        MASSIVE
    }

    public enum PrimaryModifier
    {
        WAVES,
        WAYPOINTS,
        ALIEN_HIVE,
        MAELSTROM,
        DROUGHT,
    }

    public enum SecondaryModifier
    {
        BIG_MAP,
        SMALL_MAP,
        CORRIDOR,
        BOSS,
        HORDE,
        CLUTTER,
        ALLY_PRESENCE,
        FACTION_PRESENCE,
        WRAP,
        SPLIT_MAP,
        WARP
    }

    public enum Objective
    {
        SURVIVE,
        ESCORT,
        DEFEND,
        RESCUE,
        EXPLORE,
        SCAN,
        DEAL_DAMAGE,
        DESTROY_TARGET,
    }
}