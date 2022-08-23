using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Entities;

namespace SpaceMage.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        // Singleton allowing access to the level generator
        // through the static class.
        private static LevelGenerator _;
        public static LevelGenerator Singleton { get { return _; } }
        private void Awake() { _ = this; }
        private void OnDestroy() { _ = null; }

        // ==================================================
        // ==================== SETTINGS ====================
        // ==================================================
        [SerializeField] private MissionType missionType;
        [SerializeField] private List<PrimaryModifier> primaryModifiers = new List<PrimaryModifier>();
        [SerializeField] private List<SecondaryModifier> secondaryModifiers = new List<SecondaryModifier>();
        [SerializeField] private List<Objective> objectives = new List<Objective>();
        [SerializeField] private Vector2 maxOuterLevelSize = new Vector2();
        [SerializeField] private Vector2 minOuterLevelSize = new Vector2();
        [SerializeField] private Vector2 maxInnerLevelSize = new Vector2();
        [SerializeField] private Vector2 minInnerLevelSize = new Vector2();
        [SerializeField] private List<Setpiece> setpieces = new List<Setpiece>();
        [SerializeField] private List<Hazard> hazards = new List<Hazard>();
        //[SerializeField] private List<Creature> creature = new List<Creature>(); // TODO: Split enemies up among types that do not denote hostility instead.

        public void SetPrimaryModifiers(List<PrimaryModifier> primaryModifiers) { this.primaryModifiers = primaryModifiers; }
        public void SetSecondaryModifiers(List<SecondaryModifier> secondaryModifiers) { this.secondaryModifiers = secondaryModifiers; }
        public void SetObjectives(List<Objective> objectives) { this.objectives = objectives; }
        public void SetSetpieces(List<Setpiece> setpieces) { this.setpieces = setpieces; }
        public void SetHazards(List<Hazard> hazards) { this.hazards = hazards; }

        private void initialize()
        {
            LevelGeneratorStateHandler.RegisterActiveGenerator(this);

            // Do work here.
            Debug.Log("Generator working...");

            LevelGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void setBounds()
        {
            LevelGeneratorStateHandler.RegisterActiveGenerator(this);

            // Do work here.
            Debug.Log("Generator working...");

            LevelGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void placeObjectives()
        {
            LevelGeneratorStateHandler.RegisterActiveGenerator(this);

            // Do work here.
            Debug.Log("Generator working...");

            LevelGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void placeSetpieces()
        {
            LevelGeneratorStateHandler.RegisterActiveGenerator(this);

            // Do work here.
            Debug.Log("Generator working...");

            LevelGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void placeObstacles()
        {
            LevelGeneratorStateHandler.RegisterActiveGenerator(this);

            // Do work here.
            Debug.Log("Generator working...");

            LevelGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void placeSpawnZones()
        {
            LevelGeneratorStateHandler.RegisterActiveGenerator(this);

            // Do work here.
            Debug.Log("Generator working...");

            LevelGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void placeExistingHazards()
        {
            LevelGeneratorStateHandler.RegisterActiveGenerator(this);

            // Do work here.
            Debug.Log("Generator working...");

            LevelGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void placeExistingEnemies()
        {
            LevelGeneratorStateHandler.RegisterActiveGenerator(this);

            // Do work here.
            Debug.Log("Generator working...");

            LevelGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void Start()
        {
            LevelGeneratorStateHandler.Singleton.Initializing.AddListener(initialize);
            LevelGeneratorStateHandler.Singleton.SettingBounds.AddListener(setBounds);
            LevelGeneratorStateHandler.Singleton.PlacingObjectives.AddListener(placeObjectives);
            LevelGeneratorStateHandler.Singleton.PlacingSetpieces.AddListener(placeSetpieces);
            LevelGeneratorStateHandler.Singleton.PlacingObstacles.AddListener(placeObstacles);
            LevelGeneratorStateHandler.Singleton.PlacingSpawnZones.AddListener(placeSpawnZones);
            LevelGeneratorStateHandler.Singleton.PlacingExistingHazards.AddListener(placeExistingHazards);
            LevelGeneratorStateHandler.Singleton.PlacingExistingEnemies.AddListener(placeExistingEnemies);
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
        WARP_ZONES,
        MULTI_WARP
    }

    public enum Objective
    {
        SURVIVE,
        ESCORT,
        DEFEND,
        RESCUE,
        EXPLORE,
        SCAN,
        DESTROY_TARGET,
    }
}