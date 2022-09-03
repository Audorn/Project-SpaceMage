using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Data;

namespace SpaceMage.Missions
{
    public class SpawnZoneGenerator : MissionContentGeneratorCog
    {
        private float tempMinSpawnZoneSize = 5;
        private float tempMaxSpawnZoneSize = 10;

        public void Execute()
        {
            // TODO: Find some way to differentiate what hazards spawn where. Maybe based on setpieces or objectives.
            GameObject spawnZonePrefab = PrefabManager.SpawnZonePrefab;
            int numberOfSpawnZones = 50;
            List<Rect> validSpawnZoneAreas = new List<Rect>() {
                new Rect(
                    tempMaxSpawnZoneSize * 0.25f, 
                    -(_mission.MapSize.y / 2) + tempMaxSpawnZoneSize * 0.9f, 
                    _mission.MapSize.x - tempMaxSpawnZoneSize, 
                    _mission.MapSize.y - tempMaxSpawnZoneSize * 1.5f
                    )
            };

            if (MissionManager.CurrentMissionContent.SecondaryModifiers.Contains(SecondaryModifier.SPLIT_MAP))
            {
                float horizontalDistanceToMove = _mission.MapSize.x / 2 + PrefabManager.DefaultObstacleThickness + PrefabManager.DefaultSplitMapGapThickness / 2;
                Rect leftSpawnZoneArea = new Rect(validSpawnZoneAreas[0].x - horizontalDistanceToMove, validSpawnZoneAreas[0].y, validSpawnZoneAreas[0].width, validSpawnZoneAreas[0].height);
                Rect rightSpawnZoneArea = new Rect(validSpawnZoneAreas[0].x + horizontalDistanceToMove, validSpawnZoneAreas[0].y, validSpawnZoneAreas[0].width, validSpawnZoneAreas[0].height);
                validSpawnZoneAreas[0] = leftSpawnZoneArea;
                validSpawnZoneAreas.Add(rightSpawnZoneArea);
            }

            Rect possibleSpawnZoneLocation = new Rect(0, 0, 0, 0);
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
                GameObject spawnZoneObject = GameObject.Instantiate(spawnZonePrefab, new Vector3(possibleSpawnZoneLocation.x, possibleSpawnZoneLocation.y, 0), Quaternion.identity);
                spawnZoneObject.transform.SetParent(GameObject.FindGameObjectWithTag("Spawn Zones Parent").transform);
                spawnZoneObject.name = "Spawn Zone";
                spawnZoneObject.GetComponent<BoxCollider2D>().size = new Vector2(possibleSpawnZoneLocation.width, possibleSpawnZoneLocation.height);

                List<Faction> factions = _content.HazardFactions;
                foreach (Faction faction in factions)
                {
                    Debug.Log("Adding faction data...");
                }

                SpawnZoneManager.AddSpawnZone(spawnZoneObject.GetComponent<SpawnZone>());
            }
        }

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
    }
}