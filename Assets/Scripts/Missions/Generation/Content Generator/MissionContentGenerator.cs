using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Data;
using SpaceMage.Entities;

namespace SpaceMage.Missions
{
    public class MissionContentGenerator : MonoBehaviour
    {
        // Singleton.
        private static MissionContentGenerator _;
        public static MissionContentGenerator Singleton { get { return _; } }
        private void Awake() { _ = this; }
        private void OnDestroy() { _ = null; }

        // ==================================================
        // ==================== SETTINGS ====================
        // ==================================================
        [SerializeField] private Mission mission;


        // ====================================================
        // ==================== EXTERNALS =====================
        // ====================================================
        public static void Initialize() { MissionContentGeneratorStateHandler.Initialize(); }


        // ====================================================
        // ==================== INTERNALS =====================
        // ====================================================
        private void initialize()
        {
            MissionContentGeneratorStateHandler.RegisterActiveGenerator(this);
            StartCoroutine(_initialize());
        }
        private IEnumerator _initialize()
        {
            MissionGeneratorInitializer initializer = new MissionGeneratorInitializer(MissionManager.CurrentMissionContent, MissionManager.CurrentMission);
            initializer.Execute();
            mission = initializer.Mission;

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void generateMapEdges()
        {
            MissionContentGeneratorStateHandler.RegisterActiveGenerator(this);
            StartCoroutine(_generateMapEdges());
        }
        private IEnumerator _generateMapEdges()
        {
            MapEdgeGenerator mapEdgeGenerator = new MapEdgeGenerator();
            mapEdgeGenerator.Execute();

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }


        private void placeObjectives()
        {
            MissionContentGeneratorStateHandler.RegisterActiveGenerator(this);
            StartCoroutine(_placeObjectives());
        }
        private IEnumerator _placeObjectives()
        {
            // Do work here.
            Debug.Log("Generator working...");

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void placeSetpieces()
        {
            MissionContentGeneratorStateHandler.RegisterActiveGenerator(this);
            StartCoroutine(_placeSetpieces());
        }
        private IEnumerator _placeSetpieces()
        {
            // Do work here.
            Debug.Log("Generator working...");

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void placeObstacles()
        {
            MissionContentGeneratorStateHandler.RegisterActiveGenerator(this);
            StartCoroutine(_placeObstacles());
        }
        private IEnumerator _placeObstacles()
        {
            // Do work here.
            Debug.Log("Generator working...");

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void generateSpawnZones()
        {
            MissionContentGeneratorStateHandler.RegisterActiveGenerator(this);
            StartCoroutine(_generateSpawnZones());
        }
        private IEnumerator _generateSpawnZones()
        {
            SpawnZoneGenerator spawnZoneGenerator = new SpawnZoneGenerator();
            spawnZoneGenerator.Execute();

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void placeExistingHazards()
        {
            MissionContentGeneratorStateHandler.RegisterActiveGenerator(this);
            StartCoroutine(_placeExistingHazards());
        }
        private IEnumerator _placeExistingHazards()
        {
            // Do work here.
            Debug.Log("Generator working...");

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void placeExistingEnemies()
        {
            MissionContentGeneratorStateHandler.RegisterActiveGenerator(this);
            StartCoroutine(_placeExistingEnemies());
        }
        private IEnumerator _placeExistingEnemies()
        {
            // Do work here.
            Debug.Log("Generator working...");

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void Start()
        {
            MissionContentGeneratorStateHandler.Singleton.Initializing.AddListener(initialize);
            MissionContentGeneratorStateHandler.Singleton.SettingBounds.AddListener(generateMapEdges);
            MissionContentGeneratorStateHandler.Singleton.PlacingObjectives.AddListener(placeObjectives);
            MissionContentGeneratorStateHandler.Singleton.PlacingSetpieces.AddListener(placeSetpieces);
            MissionContentGeneratorStateHandler.Singleton.PlacingObstacles.AddListener(placeObstacles);
            MissionContentGeneratorStateHandler.Singleton.PlacingSpawnZones.AddListener(generateSpawnZones);
            MissionContentGeneratorStateHandler.Singleton.PlacingExistingHazards.AddListener(placeExistingHazards);
            MissionContentGeneratorStateHandler.Singleton.PlacingExistingEnemies.AddListener(placeExistingEnemies);
        }

    }
}