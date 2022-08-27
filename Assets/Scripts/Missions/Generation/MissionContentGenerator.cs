using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Data;

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
        [SerializeField] private MissionContent missionContent;
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
            Debug.Log("Generator working...");
            missionContent = MissionManager.CurrentMissionContent;
            mission = MissionManager.CurrentMission;

            int baseMapSize = GameData.MapSizeInUnits(missionContent.MapSize);
            int x = baseMapSize, y = baseMapSize;

            // Flags that modify map x,y size and basic form.
            bool hasBigMapModifier = missionContent.SecondaryModifiers.Contains(SecondaryModifier.BIG_MAP);         // x1.5 size.
            bool hasSmallMapModifier = missionContent.SecondaryModifiers.Contains(SecondaryModifier.SMALL_MAP);     // x0.75 size.
            bool hasMoreThanOneSetpiece = missionContent.Setpieces.Count > 1;                                       // x1.25 size.
            bool hasWarpZonesModifier = missionContent.SecondaryModifiers.Contains(SecondaryModifier.WARP_ZONES);   // x4 size. Map is broken into smaller submaps.
            bool hasMultiWarpModifier = missionContent.SecondaryModifiers.Contains(SecondaryModifier.MULTI_WARP);   // x4 size. Map is broken into smaller submaps.
            bool hasCorridorModifier = missionContent.SecondaryModifiers.Contains(SecondaryModifier.CORRIDOR);      // One direction is max small, the other is 5-10x longer.

            if (hasBigMapModifier) { x = (int)(x * 1.5); y = (int)(y * 1.5); }
            if (hasSmallMapModifier) { x = (int)(x * 0.75); y = (int)(y * 0.75); }
            if (hasMoreThanOneSetpiece) { x = (int)(x * 1.25); y = (int)(y * 1.25); }
            if (hasWarpZonesModifier || hasMultiWarpModifier) { x *= 4; y *= 4; }
            if (hasCorridorModifier)
            {
                int shortSideLength = Random.Range(GameData.MapSizeInUnits(MapSize.TINY), GameData.MapSizeInUnits(MapSize.SMALL));
                int longSideLength = Mathf.Max((x * y) / shortSideLength, shortSideLength * 5);

                if (Random.Range(0, 1) < 0.5f)
                {
                    x = shortSideLength;
                    y = longSideLength;
                }
                else
                {
                    x = longSideLength;
                    y = shortSideLength;
                }
            }

            mission.SetMapSize(new Vector2Int(x, y));

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void setBounds()
        {
            MissionContentGeneratorStateHandler.RegisterActiveGenerator(this);
            StartCoroutine(_setBounds());
        }
        private IEnumerator _setBounds()
        {
            // Do work here.
            Debug.Log("Generator working...");

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

        private void placeSpawnZones()
        {
            MissionContentGeneratorStateHandler.RegisterActiveGenerator(this);
            StartCoroutine(_placeSpawnZones());
        }
        private IEnumerator _placeSpawnZones()
        {
            // Do work here.
            Debug.Log("Generator working...");

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
            MissionContentGeneratorStateHandler.Singleton.SettingBounds.AddListener(setBounds);
            MissionContentGeneratorStateHandler.Singleton.PlacingObjectives.AddListener(placeObjectives);
            MissionContentGeneratorStateHandler.Singleton.PlacingSetpieces.AddListener(placeSetpieces);
            MissionContentGeneratorStateHandler.Singleton.PlacingObstacles.AddListener(placeObstacles);
            MissionContentGeneratorStateHandler.Singleton.PlacingSpawnZones.AddListener(placeSpawnZones);
            MissionContentGeneratorStateHandler.Singleton.PlacingExistingHazards.AddListener(placeExistingHazards);
            MissionContentGeneratorStateHandler.Singleton.PlacingExistingEnemies.AddListener(placeExistingEnemies);
        }

    }
}