using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Entities;

namespace SpaceMage.Missions
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private bool isWarper;                     // True: actors warp to another Obstacle on collision. False: actors collide as normal.
        [SerializeField] private bool isWarperToMagic;              // True: magical actors warp to another Obstacle on collision. False: magical actors collide as normal.
        [SerializeField] private bool isPermeable;                  // True: actors are warped to the other side. False: they behave as normal. Overrides isSolid.
        [SerializeField] private bool isPermeableToMagic;           // True: magical actors are warped to the other side. False: they behave as normal. Overrides isSolidToMagic.
        [SerializeField] private List<Obstacle> warpObstacles = new List<Obstacle>();    // The obstacles that actors that collide with this obstacle will be warped to.
        [SerializeField] private Direction warpInDirection;         // The direction that an actor will be warped out of this obstacle.
        [SerializeField] private float validWarpPercentage;         // How much of this object (along the face, from the center) is valid spawning area.

        public bool IsWarper { get { return isWarper; } }
        public void SetIsWarper(bool value) { isWarper = value; }
        public bool IsWarperToMagic { get { return isWarperToMagic; } }
        public void SetIsWarperToMagic(bool value) { isWarperToMagic = value; }
        public bool IsPermeable { get { return isPermeable; } }
        public void SetIsPermeable(bool value) { isPermeable = value; }
        public bool IsPermeableToMagic { get { return isPermeableToMagic; } }
        public void SetIsPermeableToMagic(bool value) { isPermeableToMagic = value; }
        public List<Obstacle> WarpObstacles { get { return warpObstacles; } }
        public void SetWarpObstacles(List<Obstacle> obstacles) { warpObstacles = obstacles; }
        public Direction WarpInDirection { get { return warpInDirection; } }
        public float ValidWarpPercentage { get { return validWarpPercentage; } }
        public void SetValidWarpPercentage(float amount) { validWarpPercentage = Mathf.Min(1f, amount); }

        public Obstacle GetWarpDestination()
        {
            if (warpObstacles.Count == 0)
                return this;

            int index = Random.Range(0, warpObstacles.Count);
            return warpObstacles[index];
        }

        public Direction RequestWarpIn(CollidesWithObstacles collidesWithObstacles, out Vector2 position)
        {
            SpriteRenderer spriteRenderer = collidesWithObstacles.GetComponent<SpriteRenderer>();
            position = getWarpInLocation(spriteRenderer.bounds.size);

            return warpInDirection;
        }

        public void SetWarpInDirection(Direction direction) { warpInDirection = direction; }
        private Vector2 getWarpInLocation(Vector2 spriteSize)
        {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            float distanceFromWarpInFace = (warpInDirection == Direction.NORTH || warpInDirection == Direction.SOUTH) ? (collider.size.y / 2) + (spriteSize.y / 2) : (collider.size.x / 2) + (spriteSize.x / 2);
            float distanceAlongWarpInFace = (warpInDirection == Direction.NORTH || warpInDirection == Direction.SOUTH) ? collider.size.x * (validWarpPercentage / 2) : collider.size.y * (validWarpPercentage / 2);

            Vector2 position = Vector2.zero;

            if (warpInDirection == Direction.NORTH || warpInDirection == Direction.SOUTH)
            {
                float x = Random.Range(transform.position.x - distanceAlongWarpInFace, transform.position.x + distanceAlongWarpInFace);
                float y = (warpInDirection == Direction.NORTH) ? transform.position.y + distanceFromWarpInFace : transform.position.y - distanceFromWarpInFace;
                position = new Vector2(x, y);
            }
            else
            {
                float x = (warpInDirection == Direction.EAST) ? transform.position.x + distanceFromWarpInFace : transform.position.x - distanceFromWarpInFace;
                float y = Random.Range(transform.position.y - distanceAlongWarpInFace, transform.position.y + distanceAlongWarpInFace);
                position = new Vector2(x, y);
            }

            return position;
        }
    }
}