using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Data;

namespace SpaceMage.Missions
{
    public class MapEdgeGenerator : MissionContentGeneratorCog
    {
        public void Execute()
        {
            Vector2 mapSize = _mission.MapSize;

            int obstacleThickness = PrefabManager.DefaultObstacleThickness;
            GameObject missionObstacleParent = GameObject.FindGameObjectsWithTag("Mission Obstacle Parent")[0]; // There will only ever be one.

            // The screen center will always be 0,0,0.
            GameObject mapEdgePrefab = PrefabManager.MapEdgePrefab;
            bool hasWrapModifier = _content.SecondaryModifiers.Contains(SecondaryModifier.WRAP);
            bool hasSplitMapModifier = _content.SecondaryModifiers.Contains(SecondaryModifier.SPLIT_MAP);

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
        }

        private Obstacle createMapEdge(Rect dimensions, GameObject mapEdgePrefab, GameObject missionObstacleParent, Direction direction)
        {
            GameObject mapEdge = GameObject.Instantiate(mapEdgePrefab, new Vector3(dimensions.x, dimensions.y, 0), Quaternion.identity);
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
                float horizontalDistanceToMove = _mission.MapSize.x / 2 + PrefabManager.DefaultObstacleThickness + PrefabManager.DefaultSplitMapGapThickness / 2;
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
            Obstacle obstacle2 = GameObject.Instantiate(obstacle);
            obstacle2.transform.SetParent(missionObstacleParent.transform);
            obstacle.transform.position = new Vector3(obstacle.transform.position.x - horizontalDistanceToMove, obstacle.transform.position.y, 0);
            obstacle2.transform.position = new Vector3(obstacle2.transform.position.x + horizontalDistanceToMove, obstacle2.transform.position.y, 0);

            return obstacle2;
        }
    }
}