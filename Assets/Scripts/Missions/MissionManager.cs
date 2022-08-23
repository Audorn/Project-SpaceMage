using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceMage.LevelGeneration;
using SpaceMage.Entities;

namespace SpaceMage.Data
{
    /// <summary>
    /// Missions are levels while they are being played. Levels are data sets and generation.
    /// </summary>
    public class MissionManager : MonoBehaviour
    {
        // Singleton.
        private static MissionManager _;
        public static MissionManager Singleton { get { return _; } }
        private void Awake() { _ = this; }

        [SerializeField] private List<MissionGenerationData> missionGenerationChoices = new List<MissionGenerationData>();

        public static List<MissionGenerationData> MissionGenerationChoices { get { return _.missionGenerationChoices; } }
        public static int selectedMission = 0;
        public static MissionGenerationData GetSelectedMissionGenerationData() { return _.missionGenerationChoices[selectedMission]; }

        private void activateSpawnZones() { LevelManager.ToggleSpawnZones(true); }
        private void createPlayer() 
        {
            Debug.LogWarning("testing");
            GameObject playerPrefab = GameData.PlayerPrefab;
            GameObject player = Instantiate(playerPrefab, LevelManager.Singleton.PlayerSpawnPoint, Quaternion.identity);
        }

        // Acts as a Start() when preserved between scenes.
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (LevelStateHandler.Singleton)
            {
                Debug.LogWarning("listening");
                LevelStateHandler.Singleton.Playing.AddListener(activateSpawnZones);
                LevelStateHandler.Singleton.CreatingPlayer.AddListener(createPlayer);
            }
        }
    }
}