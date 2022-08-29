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

            float aspect = (float)Screen.width / Screen.height;
            float screenHeightInUnits = Camera.main.orthographicSize * 2;
            float screenWidthInUnits = screenHeightInUnits * aspect;
            Vector2 baseMapSize = new Vector2(Mathf.Min(screenWidthInUnits, screenHeightInUnits), Mathf.Min(screenWidthInUnits, screenHeightInUnits));
            float x = baseMapSize.x, y = baseMapSize.y;

            // Flags that modify map x,y size and basic form.
            bool hasBigMapModifier = missionContent.SecondaryModifiers.Contains(SecondaryModifier.BIG_MAP);         // Full-screen.
            bool hasSmallMapModifier = missionContent.SecondaryModifiers.Contains(SecondaryModifier.SMALL_MAP);     // Mini-screen.
            bool hasSplitMapModifier = missionContent.SecondaryModifiers.Contains(SecondaryModifier.SPLIT_MAP);     // Two half-screen maps.
            bool hasCorridorModifier = missionContent.SecondaryModifiers.Contains(SecondaryModifier.CORRIDOR);      // Full width, but narrow.


            if (hasBigMapModifier) { x = screenWidthInUnits; y = screenHeightInUnits; }
            else if (hasSmallMapModifier) { x = baseMapSize.x * 0.65f; y = baseMapSize.y * 0.65f; }
            else if (hasSplitMapModifier) { x = baseMapSize.x * 0.5f; y = baseMapSize.y * 0.7f; } // Two maps.
            else if (hasCorridorModifier) { x = screenWidthInUnits; y = baseMapSize.y * 0.5f; }

            mission.SetMapSize(new Vector2(x, y));

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }

        private void setMapEdges()
        {
            MissionContentGeneratorStateHandler.RegisterActiveGenerator(this);
            StartCoroutine(_setMapEdges());
        }
        private IEnumerator _setMapEdges()
        {
            // Do work here.
            Debug.Log("Generator working...");
            Vector2 mapSize = mission.MapSize;

            int obstacleThickness = GameData.DefaultObstacleThickness;
            GameObject missionObstacleParent = GameObject.FindGameObjectsWithTag("Mission Obstacle Parent")[0]; // There will only ever be one.

            // The screen center will always be 0,0,0.
            GameObject mapEdgePrefab = GameData.MapEdgePrefab;
            bool hasWrapModifier = missionContent.SecondaryModifiers.Contains(SecondaryModifier.WRAP);
            bool hasSplitMapModifier = missionContent.SecondaryModifiers.Contains(SecondaryModifier.SPLIT_MAP);

            // Create map edges.
            Rect northDimensions = new Rect(0, (mapSize.y / 2 + obstacleThickness / 1.5f), mapSize.x + obstacleThickness * 2, obstacleThickness);
            Rect southDimensions = new Rect(0, -(mapSize.y / 2 + obstacleThickness / 1.5f), mapSize.x + obstacleThickness * 2, obstacleThickness);
            Rect westDimensions = new Rect(-(mapSize.x / 2 + obstacleThickness / 1.5f), 0, obstacleThickness, mapSize.y);
            Rect eastDimensions = new Rect((mapSize.x / 2 + obstacleThickness / 1.5f), 0, obstacleThickness, mapSize.y);

            Obstacle northObstacle = createMapEdge(northDimensions, mapEdgePrefab, missionObstacleParent, Direction.NORTH);
            Obstacle southObstacle = createMapEdge(southDimensions, mapEdgePrefab, missionObstacleParent, Direction.SOUTH);
            Obstacle westObstacle = createMapEdge(westDimensions, mapEdgePrefab, missionObstacleParent, Direction.WEST);
            Obstacle eastObstacle = createMapEdge(eastDimensions, mapEdgePrefab, missionObstacleParent, Direction.EAST);

            applyMapEdgeModifiers(northObstacle, southObstacle, westObstacle, eastObstacle, hasWrapModifier, hasSplitMapModifier, missionObstacleParent);

            yield return new WaitForFixedUpdate();
            MissionContentGeneratorStateHandler.DeRegisterActiveGenerator(this);
        }
        private Obstacle createMapEdge(Rect dimensions, GameObject mapEdgePrefab, GameObject missionObstacleParent, Direction direction)
        {
            GameObject mapEdge = Instantiate(mapEdgePrefab, new Vector3(dimensions.x, dimensions.y, 0), Quaternion.identity);
            mapEdge.transform.SetParent(missionObstacleParent.transform);
            mapEdge.name = (direction == Direction.NORTH) ? "North Map Edge" :
                           (direction == Direction.SOUTH) ? "South Map Edge" :
                           (direction == Direction.WEST) ? "West Map Edge" :
                           (direction == Direction.EAST) ? "East Map Edge" : "NAMING_ERROR";
            mapEdge.GetComponent<BoxCollider2D>().size = new Vector2(dimensions.width, dimensions.height);
            Obstacle obstacle = mapEdge.GetComponent<Obstacle>();
            obstacle.SetWarpInDirection((direction == Direction.NORTH) ? Direction.SOUTH :
                                        (direction == Direction.SOUTH) ? Direction.NORTH :
                                        (direction == Direction.WEST) ? Direction.EAST :
                                        (direction == Direction.EAST) ? Direction.WEST : Direction.NORTH);

            return obstacle;
        }
        private void applyMapEdgeModifiers(Obstacle northObstacle, Obstacle southObstacle, Obstacle westObstacle, Obstacle eastObstacle, bool hasWrapModifier, bool hasSplitMapModifier, GameObject missionObstacleParent)
        {
            if (hasWrapModifier)
            {
                applyMapEdgeWrapTo(northObstacle, southObstacle);
                applyMapEdgeWrapTo(southObstacle, northObstacle);
                applyMapEdgeWrapTo(westObstacle, eastObstacle);
                applyMapEdgeWrapTo(eastObstacle, westObstacle);
            }

            if (hasSplitMapModifier)
            {
                // Duplicate each side, move the first set to the left and the second site to the right.
                float horizontalDistanceToMove = mission.MapSize.x / 2 + GameData.DefaultObstacleThickness + GameData.DefaultSplitMapGapThickness / 2;
                Obstacle northObstacle2 = splitObstacle(northObstacle, horizontalDistanceToMove, missionObstacleParent);
                Obstacle southObstacle2 = splitObstacle(southObstacle, horizontalDistanceToMove, missionObstacleParent);
                splitObstacle(westObstacle, horizontalDistanceToMove, missionObstacleParent);
                splitObstacle(eastObstacle, horizontalDistanceToMove, missionObstacleParent);

                // Wrap top left to bottom right and top right to bottom left.
                applyMapEdgeWrapTo(northObstacle, southObstacle2);
                applyMapEdgeWrapTo(southObstacle, northObstacle2);
                applyMapEdgeWrapTo(northObstacle2, southObstacle);
                applyMapEdgeWrapTo(southObstacle2, northObstacle);
            }
        }
        private void applyMapEdgeWrapTo(Obstacle obstacleFrom, Obstacle obstacleTo) 
        {
            obstacleFrom.SetIsWarper(true);
            obstacleFrom.SetWarpObstacles(new List<Obstacle>() { obstacleTo });
        }
        private Obstacle splitObstacle(Obstacle obstacle, float horizontalDistanceToMove, GameObject missionObstacleParent)
        {
            Obstacle obstacle2 = Instantiate(obstacle);
            obstacle2.transform.SetParent(missionObstacleParent.transform);
            obstacle.transform.position = new Vector3(obstacle.transform.position.x - horizontalDistanceToMove, obstacle.transform.position.y, 0);
            obstacle2.transform.position = new Vector3(obstacle2.transform.position.x + horizontalDistanceToMove, obstacle2.transform.position.y, 0);

            return obstacle2;
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
            int numberOfSpawnZones = 50;
            List<Rect> validSpawnZoneAreas = new List<Rect>() {
                new Rect(/*-(mission.MapSize.x / 2)*/ tempMaxSpawnZoneSize * 0.25f, -(mission.MapSize.y / 2) + tempMaxSpawnZoneSize * 0.9f, mission.MapSize.x - tempMaxSpawnZoneSize, mission.MapSize.y - tempMaxSpawnZoneSize * 1.5f),
            };

            if (MissionManager.CurrentMissionContent.SecondaryModifiers.Contains(SecondaryModifier.SPLIT_MAP)) 
            {
                float horizontalDistanceToMove = mission.MapSize.x / 2 + GameData.DefaultObstacleThickness + GameData.DefaultSplitMapGapThickness / 2;
                Rect leftSpawnZoneArea = new Rect(validSpawnZoneAreas[0].x - horizontalDistanceToMove, validSpawnZoneAreas[0].y, validSpawnZoneAreas[0].width, validSpawnZoneAreas[0].height);
                Rect rightSpawnZoneArea = new Rect(validSpawnZoneAreas[0].x + horizontalDistanceToMove, validSpawnZoneAreas[0].y, validSpawnZoneAreas[0].width, validSpawnZoneAreas[0].height);
                validSpawnZoneAreas[0] = leftSpawnZoneArea;
                validSpawnZoneAreas.Add(rightSpawnZoneArea);
            }

            Rect possibleSpawnZoneLocation = new Rect(0,0,0,0);
            for (int i = 0; i < numberOfSpawnZones; i++)
            {
                // Generate a valid spawn location. Right now, it is an obstacle thickness inside the map edges and not overlapping any other spawn points.
                possibleSpawnZoneLocation = generatePotentialSpawnZoneRects(validSpawnZoneAreas);
                int count = 0;
                while (count < 20 && !SpawnZoneManager.IsValidSpawnZoneLocation(possibleSpawnZoneLocation))
                {
                    possibleSpawnZoneLocation = generatePotentialSpawnZoneRects(validSpawnZoneAreas);
                    count++;
                }

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
        private Rect generatePotentialSpawnZoneRects(List<Rect> validSpawnZoneAreas)
        {
            int index = Random.Range(0, validSpawnZoneAreas.Count);
            Rect validSpawnZoneArea = validSpawnZoneAreas[index];

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
            MissionContentGeneratorStateHandler.Singleton.SettingBounds.AddListener(setMapEdges);
            MissionContentGeneratorStateHandler.Singleton.PlacingObjectives.AddListener(placeObjectives);
            MissionContentGeneratorStateHandler.Singleton.PlacingSetpieces.AddListener(placeSetpieces);
            MissionContentGeneratorStateHandler.Singleton.PlacingObstacles.AddListener(placeObstacles);
            MissionContentGeneratorStateHandler.Singleton.PlacingSpawnZones.AddListener(placeSpawnZones);
            MissionContentGeneratorStateHandler.Singleton.PlacingExistingHazards.AddListener(placeExistingHazards);
            MissionContentGeneratorStateHandler.Singleton.PlacingExistingEnemies.AddListener(placeExistingEnemies);
        }

    }
}