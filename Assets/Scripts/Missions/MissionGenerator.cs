using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Data;

namespace SpaceMage.Missions
{
    public class MissionGenerator : MonoBehaviour
    {
        // Singleton.
        private static MissionGenerator _;
        public static MissionGenerator Singleton { get { return _; } }
        private void Awake() { _ = this; }

        // ==================================================
        // ==================== SETTINGS ====================
        // ==================================================
        private Mission mission;
        public Mission Mission { get { return mission; } }


        // ====================================================
        // ==================== EXTERNALS =====================
        // ====================================================
        public static void Initialize() { MissionGeneratorStateHandler.Initialize(); }


        // ====================================================
        // ==================== INTERNALS =====================
        // ====================================================
        private void initialize()
        {
            MissionGeneratorStateHandler.RegisterActiveMissionGenerator(this);
            StartCoroutine(_initialize());
        }
        private IEnumerator _initialize()
        {
            // Create new blank mission object from prefab.
            Debug.Log("Mission generator working...");

            GameObject go = Instantiate(GameData.MissionPrefab, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(transform);
            go.name = "Current Mission";

            Mission mission = go.GetComponent<Mission>();
            this.mission = mission;
            MissionManager.MissionList.RegisterCurrentMission(mission);

            yield return new WaitForFixedUpdate();
            MissionGeneratorStateHandler.DeRegisterActiveMissionGenerator(this);
        }

        private void selectMissionContent()
        {
            MissionGeneratorStateHandler.RegisterActiveMissionGenerator(this);
            StartCoroutine(_selectMissionContent());
        }
        private IEnumerator _selectMissionContent()
        {
            // Do work here.
            Debug.Log("Mission generator initializing mission content selector...");
            MissionContentSelector.Initialize();

            yield return new WaitForFixedUpdate();
            MissionGeneratorStateHandler.DeRegisterActiveMissionGenerator(this);
        }

        private void generateMissionContent()
        {
            MissionGeneratorStateHandler.RegisterActiveMissionGenerator(this);
            StartCoroutine(_generateMissionContent());
        }
        private IEnumerator _generateMissionContent()
        {
            // Do work here.
            Debug.Log("Mission generator initializing mission content generator...");
            MissionContentGenerator.Initialize();

            yield return new WaitForFixedUpdate();
            MissionGeneratorStateHandler.DeRegisterActiveMissionGenerator(this);
        }

        private void createPlayer()
        {
            MissionGeneratorStateHandler.RegisterActiveMissionGenerator(this);
            StartCoroutine(_createPlayer());
        }
        private IEnumerator _createPlayer()
        {
            // Do work here.
            Debug.Log("Mission generator working...");

            // Move spawn point if split.
            bool hasSplitMapModifier = MissionManager.CurrentMissionContent.SecondaryModifiers.Contains(SecondaryModifier.SPLIT_MAP);
            if (hasSplitMapModifier)
            {
                Vector2 spawnPoint = MissionManager.CurrentMission.PlayerSpawnPoint;
                spawnPoint.x -= GameData.DefaultObstacleThickness + GameData.DefaultSplitMapGapThickness / 2 + MissionManager.CurrentMission.MapSize.x / 2;
                MissionManager.CurrentMission.SetPlayerSpawnPoint(spawnPoint);
            }

            yield return new WaitForFixedUpdate();
            MissionGeneratorStateHandler.DeRegisterActiveMissionGenerator(this);
        }

        private void play()
        {
            MissionGeneratorStateHandler.RegisterActiveMissionGenerator(this);
            StartCoroutine(_play());
        }
        private IEnumerator _play()
        {
            // Do work here.
            Debug.Log("Mission generator working...");

            yield return new WaitForFixedUpdate();
            MissionGeneratorStateHandler.DeRegisterActiveMissionGenerator(this);
        }

        private void exitingMission()
        {
            MissionGeneratorStateHandler.RegisterActiveMissionGenerator(this);
            StartCoroutine(_exitingMission());
        }
        private IEnumerator _exitingMission()
        {
            // Do work here.
            Debug.Log("Mission generator working...");

            yield return new WaitForFixedUpdate();
            MissionGeneratorStateHandler.DeRegisterActiveMissionGenerator(this);
        }

        private void Start()
        {
            MissionGeneratorStateHandler.Singleton.Initializing.AddListener(initialize);
            MissionGeneratorStateHandler.Singleton.SelectingMissionContent.AddListener(selectMissionContent);
            MissionGeneratorStateHandler.Singleton.GeneratingMissionContent.AddListener(generateMissionContent);
            MissionGeneratorStateHandler.Singleton.CreatingPlayer.AddListener(createPlayer);
            MissionGeneratorStateHandler.Singleton.Playing.AddListener(play);
            MissionGeneratorStateHandler.Singleton.ExitingMission.AddListener(exitingMission);
        }
    }
}