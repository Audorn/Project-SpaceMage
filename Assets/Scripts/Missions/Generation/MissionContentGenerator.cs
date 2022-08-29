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
            bool hasMoreThanOneSetpiece = missionContent.SetpieceFactions.Count > 1;                                       // x1.25 size.
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
            Vector2Int mapSize = mission.MapSize;

            int obstacleThickness = GameData.DefaultObstacleThickness;
            int boundaryRegionThickness = GameData.BoundaryRegionThickness;
            RectInt boundaryRegion = new RectInt(-boundaryRegionThickness, -boundaryRegionThickness, mapSize.x + boundaryRegionThickness * 2, mapSize.y + boundaryRegionThickness * 2);
            GameObject missionObstacleParent = GameObject.FindGameObjectsWithTag("Mission Obstacle Parent")[0]; // There will only ever be one.
            mission.SetBoundary(boundaryRegion);

            // Create map boundaries.
            GameObject boundaryPrefab = GameData.BoundaryPrefab;

            Rect northDimensions = new Rect(0, boundaryRegion.y + boundaryRegion.height, boundaryRegion.width, obstacleThickness);
            Rect southDimensions = new Rect(0, boundaryRegion.y, boundaryRegion.width, obstacleThickness);
            Rect westDimensions = new Rect(-(boundaryRegion.width / 2), boundaryRegion.y + boundaryRegion.height / 2, obstacleThickness, boundaryRegion.height - obstacleThickness);
            Rect eastDimensions = new Rect((boundaryRegion.width / 2), boundaryRegion.y + boundaryRegion.height / 2, obstacleThickness, boundaryRegion.height - obstacleThickness);

            GameObject northBoundary = Instantiate(boundaryPrefab, new Vector3(northDimensions.x, northDimensions.y, 0), Quaternion.identity);
            GameObject southBoundary = Instantiate(boundaryPrefab, new Vector3(southDimensions.x, southDimensions.y, 0), Quaternion.identity);
            GameObject westBoundary = Instantiate(boundaryPrefab, new Vector3(westDimensions.x, westDimensions.y, 0), Quaternion.identity);
            GameObject eastBoundary = Instantiate(boundaryPrefab, new Vector3(eastDimensions.x, eastDimensions.y, 0), Quaternion.identity);

            northBoundary.transform.SetParent(missionObstacleParent.transform);
            southBoundary.transform.SetParent(missionObstacleParent.transform);
            westBoundary.transform.SetParent(missionObstacleParent.transform);
            eastBoundary.transform.SetParent(missionObstacleParent.transform);

            northBoundary.name = "North Boundary";
            southBoundary.name = "South Boundary";
            westBoundary.name = "West Boundary";
            eastBoundary.name = "East Boundary";

            northBoundary.GetComponent<BoxCollider2D>().size = new Vector2(northDimensions.width, northDimensions.height);
            southBoundary.GetComponent<BoxCollider2D>().size = new Vector2(southDimensions.width, southDimensions.height);
            westBoundary.GetComponent<BoxCollider2D>().size = new Vector2(westDimensions.width, westDimensions.height);
            eastBoundary.GetComponent<BoxCollider2D>().size = new Vector2(eastDimensions.width, eastDimensions.height);

            northBoundary.GetComponent<Obstacle>().SetWarpInDirection(Direction.SOUTH);
            southBoundary.GetComponent<Obstacle>().SetWarpInDirection(Direction.NORTH);
            westBoundary.GetComponent<Obstacle>().SetWarpInDirection(Direction.EAST);
            eastBoundary.GetComponent<Obstacle>().SetWarpInDirection(Direction.WEST);

            // Create map edges.
            GameObject mapEdgePrefab = GameData.MapEdgePrefab;

            northDimensions = new Rect(0, mapSize.y, mapSize.x, obstacleThickness);
            southDimensions = new Rect(0, 0, mapSize.x, obstacleThickness);
            westDimensions = new Rect(-(mapSize.x / 2), mapSize.y / 2, obstacleThickness, mapSize.y - obstacleThickness);
            eastDimensions = new Rect((mapSize.x / 2), mapSize.y / 2, obstacleThickness, mapSize.y - obstacleThickness);

            GameObject northMapEdge = Instantiate(mapEdgePrefab, new Vector3(northDimensions.x, northDimensions.y, 0), Quaternion.identity);
            GameObject southMapEdge = Instantiate(mapEdgePrefab, new Vector3(southDimensions.x, southDimensions.y, 0), Quaternion.identity);
            GameObject westMapEdge = Instantiate(mapEdgePrefab, new Vector3(westDimensions.x, westDimensions.y, 0), Quaternion.identity);
            GameObject eastMapEdge = Instantiate(mapEdgePrefab, new Vector3(eastDimensions.x, eastDimensions.y, 0), Quaternion.identity);

            northMapEdge.transform.SetParent(missionObstacleParent.transform);
            southMapEdge.transform.SetParent(missionObstacleParent.transform);
            westMapEdge.transform.SetParent(missionObstacleParent.transform);
            eastMapEdge.transform.SetParent(missionObstacleParent.transform);

            northMapEdge.name = "North Map Edge";
            southMapEdge.name = "South Map Edge";
            westMapEdge.name = "West Map Edge";
            eastMapEdge.name = "East Map Edge";

            northMapEdge.GetComponent<BoxCollider2D>().size = new Vector2(northDimensions.width, northDimensions.height);
            southMapEdge.GetComponent<BoxCollider2D>().size = new Vector2(southDimensions.width, southDimensions.height);
            westMapEdge.GetComponent<BoxCollider2D>().size = new Vector2(westDimensions.width, westDimensions.height);
            eastMapEdge.GetComponent<BoxCollider2D>().size = new Vector2(eastDimensions.width, eastDimensions.height);

            Obstacle northObstacle = northMapEdge.GetComponent<Obstacle>();
            Obstacle southObstacle = southMapEdge.GetComponent<Obstacle>();
            Obstacle westObstacle = westMapEdge.GetComponent<Obstacle>();
            Obstacle eastObstacle = eastMapEdge.GetComponent<Obstacle>();

            northObstacle.SetWarpInDirection(Direction.SOUTH);
            southObstacle.SetWarpInDirection(Direction.NORTH);
            westObstacle.SetWarpInDirection(Direction.EAST);
            eastObstacle.SetWarpInDirection(Direction.WEST);

            northObstacle.SetIsPermeableToMagic(true);
            southObstacle.SetIsPermeableToMagic(true);
            westObstacle.SetIsPermeableToMagic(true);
            eastObstacle.SetIsPermeableToMagic(true);

            // Apply relevant flags.
            bool hasWrapModifier = mission.HasSecondaryModifier(SecondaryModifier.WRAP);
            bool hasWarpModifier = mission.HasSecondaryModifier(SecondaryModifier.WARP_ZONES);
            bool hasMultiWarpModifier = mission.HasSecondaryModifier(SecondaryModifier.MULTI_WARP);

            if (hasWrapModifier || hasWarpModifier || hasMultiWarpModifier)
            {
                northObstacle.SetWarper(true);
                southObstacle.SetWarper(true);
                westObstacle.SetWarper(true);
                eastObstacle.SetWarper(true);
            }

            if (hasWrapModifier || hasWarpModifier)
            {
                northObstacle.SetWarpObstacles(new List<Obstacle>() { southObstacle });
                southObstacle.SetWarpObstacles(new List<Obstacle>() { northObstacle });
                westObstacle.SetWarpObstacles(new List<Obstacle>() { eastObstacle });
                eastObstacle.SetWarpObstacles(new List<Obstacle>() { westObstacle });
            }

            if (hasMultiWarpModifier)
            {
                northObstacle.SetWarpObstacles(new List<Obstacle>() { southObstacle, westObstacle, eastObstacle });
                southObstacle.SetWarpObstacles(new List<Obstacle>() { northObstacle, westObstacle, eastObstacle });
                westObstacle.SetWarpObstacles(new List<Obstacle>() { eastObstacle, northObstacle, southObstacle });
                eastObstacle.SetWarpObstacles(new List<Obstacle>() { westObstacle, northObstacle, southObstacle });
            }

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

            // TODO: Find some way to differentiate what hazards spawn where. Maybe based on setpieces or objectives.
            GameObject spawnZonePrefab = GameData.SpawnZonePrefab;
            int squareUnits = mission.MapSize.x * mission.MapSize.y;
            int numberOfSpawnZones = squareUnits / GameData.SquareUnitsPerSpawnZone;
            var validSpawnZoneArea = new Rect(GameData.DefaultObstacleThickness / 2 + tempMaxSpawnZoneSize / 2, GameData.DefaultObstacleThickness / 2 + tempMaxSpawnZoneSize / 2, mission.MapSize.x - GameData.DefaultObstacleThickness - tempMaxSpawnZoneSize, mission.MapSize.y - GameData.DefaultObstacleThickness - tempMaxSpawnZoneSize);

            Rect possibleSpawnZoneLocation = new Rect(0,0,0,0);
            for (int i = 0; i < numberOfSpawnZones; i++)
            {
                // Generate a valid spawn location. Right now, it is an obstacle thickness inside the map edges and not overlapping any other spawn points.
                possibleSpawnZoneLocation = generatePotentialSpawnZoneRect(validSpawnZoneArea);
                while (!SpawnZoneManager.IsValidSpawnZoneLocation(possibleSpawnZoneLocation))
                    possibleSpawnZoneLocation = generatePotentialSpawnZoneRect(validSpawnZoneArea);

                // Create the spawn zone and place it.
                GameObject spawnZoneObject = Instantiate(spawnZonePrefab, new Vector3(possibleSpawnZoneLocation.x, possibleSpawnZoneLocation.y, 0), Quaternion.identity);
                spawnZoneObject.transform.SetParent(GameObject.FindGameObjectWithTag("Spawn Zones Parent").transform);
                spawnZoneObject.name = "Spawn Zone";
                spawnZoneObject.GetComponent<BoxCollider2D>().size = new Vector2(possibleSpawnZoneLocation.width, possibleSpawnZoneLocation.height);
                
                List<Faction> factions = missionContent.HazardFactions;
                foreach (Faction faction in factions)
                {
                    Debug.Log("Adding faction data...");
                }

                SpawnZoneManager.AddSpawnZone(spawnZoneObject.GetComponent<SpawnZone>());
            }

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }
        private float tempMinSpawnZoneSize = 5;
        private float tempMaxSpawnZoneSize = 10;
        private Rect generatePotentialSpawnZoneRect(Rect validSpawnZoneArea)
        {
            float x = Random.Range(validSpawnZoneArea.x - validSpawnZoneArea.width / 2, validSpawnZoneArea.x + validSpawnZoneArea.width / 2);
            float y = Random.Range(validSpawnZoneArea.y, validSpawnZoneArea.y + validSpawnZoneArea.height);
            float width = Random.Range(tempMinSpawnZoneSize, tempMaxSpawnZoneSize);
            float height = Random.Range(tempMinSpawnZoneSize, tempMaxSpawnZoneSize);
            return new Rect(x - width / 2, y - height / 2, width, height);
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