using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceMage.LevelGeneration
{
    public class LevelGeneratorStateHandler : MonoBehaviour
    {
        // Singleton allowing access to the level generator state handler through the static class.
        private static LevelGeneratorStateHandler _;
        public static LevelGeneratorStateHandler Singleton { get { return _; } }
        private void Awake() { _ = this; }

        // Singleton allowing access to the level generator state through the static class.
        private static LevelGeneratorState state = LevelGeneratorState.WAITING;
        public static LevelGeneratorState State { get { return state; } }

        public UnityEvent Initializing = new UnityEvent();
        public UnityEvent InitializingComplete = new UnityEvent();
        public UnityEvent SettingBounds = new UnityEvent();
        public UnityEvent SettingBoundsComplete = new UnityEvent();
        public UnityEvent PlacingObjectives = new UnityEvent();
        public UnityEvent PlacingObjectivesComplete = new UnityEvent();
        public UnityEvent PlacingSetpieces = new UnityEvent();
        public UnityEvent PlacingSetpiecesComplete = new UnityEvent();
        public UnityEvent PlacingObstacles = new UnityEvent();
        public UnityEvent PlacingObstaclesComplete = new UnityEvent();
        public UnityEvent PlacingSpawnZones = new UnityEvent();
        public UnityEvent PlacingSpawnZonesComplete = new UnityEvent();
        public UnityEvent PlacingExistingHazards = new UnityEvent();
        public UnityEvent PlacingExistingHazardsComplete = new UnityEvent();
        public UnityEvent PlacingExistingEnemies = new UnityEvent();
        public UnityEvent PlacingExistingEnemiesComplete = new UnityEvent();
        public UnityEvent Finished = new UnityEvent();

        private bool doInitializing = false;
        private bool doInitializingComplete = false;
        private bool doSettingBounds = false;
        private bool doSettingBoundsComplete = false;
        private bool doPlacingObjectives = false;
        private bool doPlacingObjectivesComplete = false;
        private bool doPlacingSetpieces = false;
        private bool doPlacingSetpiecesComplete = false;
        private bool doPlacingObstacles = false;
        private bool doPlacingObstaclesComplete = false;
        private bool doPlacingSpawnZones = false;
        private bool doPlacingSpawnZonesComplete = false;
        private bool doPlacingExistingHazards = false;
        private bool doPlacingExistingHazardsComplete = false;
        private bool doPlacingExistingEnemies = false;
        private bool doPlacingExistingEnemiesComplete = false;
        private bool doFinished = false;

        public static void Initialize() { _.doInitializing = true; }

        private static HashSet<LevelGenerator> activeLevelGenerators = new HashSet<LevelGenerator>();
        public static void RegisterActiveGenerator(LevelGenerator contentPicker) { activeLevelGenerators.Add(contentPicker); }
        public static void DeRegisterActiveGenerator(LevelGenerator contentPicker) { activeLevelGenerators.Remove(contentPicker); }

        /// <summary>
        /// Called by LevelManager to start the level generator state handler.
        /// </summary>
        public void Start()
        {
            // Register all the simple actions below.
            Initializing.AddListener(initializing);
            InitializingComplete.AddListener(initializingComplete);
            SettingBounds.AddListener(settingBounds);
            SettingBoundsComplete.AddListener(settingBoundsComplete);
            PlacingObjectives.AddListener(placingObjectives);
            PlacingObjectivesComplete.AddListener(placingObjectivesComplete);
            PlacingSetpieces.AddListener(placingSetpieces);
            PlacingSetpiecesComplete.AddListener(placingSetpiecesComplete);
            PlacingObstacles.AddListener(placingObstacles);
            PlacingObstaclesComplete.AddListener(placingObstaclesComplete);
            PlacingSpawnZones.AddListener(placingSpawnZones);
            PlacingSpawnZonesComplete.AddListener(placingSpawnZonesComplete);
            PlacingExistingHazards.AddListener(placingExistingHazards);
            PlacingExistingHazardsComplete.AddListener(placingExistingHazardsComplete);
            PlacingExistingEnemies.AddListener(placingExistingEnemies);
            PlacingExistingEnemiesComplete.AddListener(placingExistingEnemiesComplete);
            Finished.AddListener(finished);
        }

        // No two events should fire on the same frame.
        private void Update()
        {
            if (doInitializing)                                                                  Initializing.Invoke();
            else if (doInitializingComplete && activeLevelGenerators.Count == 0)                 InitializingComplete.Invoke();
            else if (doSettingBounds && activeLevelGenerators.Count == 0)                        SettingBounds.Invoke();
            else if (doSettingBoundsComplete && activeLevelGenerators.Count == 0)                SettingBoundsComplete.Invoke();
            else if (doPlacingObjectives && activeLevelGenerators.Count == 0)                    PlacingObjectives.Invoke();
            else if (doPlacingObjectivesComplete && activeLevelGenerators.Count == 0)            PlacingObjectivesComplete.Invoke();
            else if (doPlacingSetpieces && activeLevelGenerators.Count == 0)                     PlacingSetpieces.Invoke();
            else if (doPlacingSetpiecesComplete && activeLevelGenerators.Count == 0)             PlacingSetpiecesComplete.Invoke();
            else if (doPlacingObstacles && activeLevelGenerators.Count == 0)                     PlacingObstacles.Invoke();
            else if (doPlacingObstaclesComplete && activeLevelGenerators.Count == 0)             PlacingObstaclesComplete.Invoke();
            else if (doPlacingSpawnZones && activeLevelGenerators.Count == 0)                    PlacingSpawnZones.Invoke();
            else if (doPlacingSpawnZonesComplete && activeLevelGenerators.Count == 0)            PlacingSpawnZonesComplete.Invoke();
            else if (doPlacingExistingHazards && activeLevelGenerators.Count == 0)               PlacingExistingHazards.Invoke();
            else if (doPlacingExistingHazardsComplete && activeLevelGenerators.Count == 0)       PlacingExistingHazardsComplete.Invoke();
            else if (doPlacingExistingEnemies && activeLevelGenerators.Count == 0)               PlacingExistingEnemies.Invoke();
            else if (doPlacingExistingEnemiesComplete && activeLevelGenerators.Count == 0)       PlacingExistingEnemiesComplete.Invoke();
            else if (doFinished && activeLevelGenerators.Count == 0)                             Finished.Invoke();
        }

        // Simple actions to show the events firing in the console and iterate through them.
        private void initializing()
        {
            Debug.Log("Initializing level generator...");
            state = LevelGeneratorState.INITIALIZING;
            doInitializing = false;
            doInitializingComplete = true;
        }

        private void initializingComplete()
        {
            Debug.Log("Level generator initialization complete.");
            state = LevelGeneratorState.INITIALIZING_COMPLETE;
            doInitializingComplete = false;
            doSettingBounds = true;
        }

        private void settingBounds()
        {
            Debug.Log("Setting level bounds...");
            state = LevelGeneratorState.SETTING_BOUNDS;
            doSettingBounds = false;
            doSettingBoundsComplete = true;
        }

        private void settingBoundsComplete()
        {
            Debug.Log("Finished setting level bounds.");
            state = LevelGeneratorState.SETTING_BOUNDS_COMPLETE;
            doSettingBoundsComplete = false;
            doPlacingObjectives = true;
        }

        private void placingObjectives()
        {
            Debug.Log("Placing objectives...");
            state = LevelGeneratorState.PLACING_OBJECTIVES;
            doPlacingObjectives = false;
            doPlacingObjectivesComplete = true;
        }

        private void placingObjectivesComplete()
        {
            Debug.Log("Objectives placed.");
            state = LevelGeneratorState.PLACING_OBJECTIVES_COMPLETE;
            doPlacingObjectivesComplete = false;
            doPlacingSetpieces = true;
        }

        private void placingSetpieces()
        {
            Debug.Log("Placing setpieces...");
            state = LevelGeneratorState.PLACING_SETPIECES;
            doPlacingSetpieces = false;
            doPlacingSetpiecesComplete = true;
        }

        private void placingSetpiecesComplete()
        {
            Debug.Log("Setpieces placed.");
            state = LevelGeneratorState.PLACING_SETPIECES_COMPLETE;
            doPlacingSetpiecesComplete = false;
            doPlacingObstacles = true;
        }

        private void placingObstacles()
        {
            Debug.Log("Placing obstacles...");
            state = LevelGeneratorState.PLACING_OBSTACLES;
            doPlacingObstacles = false;
            doPlacingObstaclesComplete = true;
        }

        private void placingObstaclesComplete()
        {
            Debug.Log("Obstacles placed.");
            state = LevelGeneratorState.PLACING_OBSTACLES_COMPLETE;
            doPlacingObstaclesComplete = false;
            doPlacingSpawnZones = true;
        }

        private void placingSpawnZones()
        {
            Debug.Log("Placing spawn zones...");
            state = LevelGeneratorState.PLACING_SPAWN_ZONES;
            doPlacingSpawnZones = false;
            doPlacingSpawnZonesComplete = true;
        }
        private void placingSpawnZonesComplete()
        {
            Debug.Log("Spawn zones placed.");
            state = LevelGeneratorState.PLACING_SPAWN_ZONES_COMPLETE;
            doPlacingSpawnZonesComplete = false;
            doPlacingExistingHazards = true;
        }
        private void placingExistingHazards()
        {
            Debug.Log("Placing existing hazards...");
            state = LevelGeneratorState.PLACING_EXISTING_HAZARDS;
            doPlacingExistingHazards = false;
            doPlacingExistingHazardsComplete = true;
        }
        private void placingExistingHazardsComplete()
        {
            Debug.Log("Existing hazards placed.");
            state = LevelGeneratorState.PLACING_EXISTING_HAZARDS_COMPLETE;
            doPlacingExistingHazardsComplete = false;
            doPlacingExistingEnemies = true;
        }
        private void placingExistingEnemies()
        {
            Debug.Log("Placing existing enemies...");
            state = LevelGeneratorState.PLACING_EXISTING_ENEMIES;
            doPlacingExistingEnemies = false;
            doPlacingExistingEnemiesComplete = true;
        }
        private void placingExistingEnemiesComplete()
        {
            Debug.Log("Existing enemies placed.");
            state = LevelGeneratorState.PLACING_EXISTING_ENEMIES_COMPLETE;
            doPlacingExistingEnemiesComplete = false;
            doFinished = true;
        }
        private void finished()
        {
            Debug.Log("Level generator finished.");
            state = LevelGeneratorState.FINISHED;
            doFinished = false;
            LevelStateHandler.RegisterGeneratingLevelComplete();
        }

        // Only one level data generator state handler can exist at a time. Clean up after yourself.
        private void OnDestroy() { _ = null; }
    }

    /// <summary>
    /// The state of a given level.
    /// </summary>
    public enum LevelGeneratorState
    {
        WAITING,
        INITIALIZING,
        INITIALIZING_COMPLETE,
        SETTING_BOUNDS,
        SETTING_BOUNDS_COMPLETE,
        PLACING_OBJECTIVES,
        PLACING_OBJECTIVES_COMPLETE,
        PLACING_SETPIECES,
        PLACING_SETPIECES_COMPLETE,
        PLACING_OBSTACLES,
        PLACING_OBSTACLES_COMPLETE,
        PLACING_SPAWN_ZONES,
        PLACING_SPAWN_ZONES_COMPLETE,
        PLACING_EXISTING_HAZARDS,
        PLACING_EXISTING_HAZARDS_COMPLETE,
        PLACING_EXISTING_ENEMIES,
        PLACING_EXISTING_ENEMIES_COMPLETE,
        FINISHED
    }
}