using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Missions;

namespace SpaceMage.Actors
{
    /// <summary>
    /// Handles unique collision effects when colliding with Obstacles.
    /// </summary>
    public class CollidesWithObstacles : MonoBehaviour
    {
        private Rigidbody2D rb;                             // Assigned in Start(). Gets from parent if none found.

        private Vector2 velocityLastFrame;                  // Tracked and set internally.
        private float angularVelocityLastFrame;             // Tracked and set internally.
        private Direction obstacleFaceImpacted;             // Tracked and set internally.

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
            if (obstacle == null)
                return;

            calculateObstacleFaceImpacted();

            Magical magical = GetComponentInParent<Magical>();

            // Obstacle is permeable to this actor - warp through it.
            if ((obstacle.IsPermeable && magical == null) || (magical && obstacle.IsPermeableToMagic))
                passThrough(obstacle);

            // Obstacle is a warper and will send this actor to a different obstacle to be warped in.
            if ((obstacle.Warps && magical == null) || (magical && obstacle.WarpsMagic))
                warpThrough(obstacle);

            // Get rid of any unwanted spinning.
            if (rb)
                rb.angularVelocity = angularVelocityLastFrame;
        }

        private void passThrough(Obstacle obstacle)
        {
            Vector2 distance = getDistanceToWarpAcrossObstacle(obstacle);
            rb.transform.position += (Vector3)distance;
            rb.velocity = velocityLastFrame;
        }

        private void warpThrough(Obstacle obstacle)
        {
            Obstacle warpDestinationObstacle = obstacle.GetWarpDestination();
            Vector2 position = new Vector2();
            Direction warpInDirection = warpDestinationObstacle.RequestWarpIn(this, out position);

            rb.transform.position = position;

            // Warp in going the same direction - just change location, but keep velocity.
            if ((obstacleFaceImpacted == Direction.NORTH && warpInDirection == Direction.SOUTH) ||
                (obstacleFaceImpacted == Direction.SOUTH && warpInDirection == Direction.NORTH) ||
                (obstacleFaceImpacted == Direction.WEST && warpInDirection == Direction.EAST) ||
                (obstacleFaceImpacted == Direction.EAST && warpInDirection == Direction.WEST))
            {
                rb.velocity = velocityLastFrame;
            }

            // Warp in going the opposite direction - reverse it.
            else if (obstacleFaceImpacted == warpInDirection)
            {
                Vector2 cardinalImpactNormal = (warpInDirection == Direction.NORTH || obstacleFaceImpacted == Direction.SOUTH) ? Vector2.up : Vector2.right;
                rb.velocity = Vector2.Reflect(velocityLastFrame, cardinalImpactNormal);
            }

            // Warp in going 90 degrees off - make sure it's moving in the correct direction for the warp in.
            else if (warpInDirection == Direction.NORTH)
            {
                rb.velocity = new Vector2(velocityLastFrame.y, Mathf.Abs(velocityLastFrame.x * -1));
                //rb.rotation = (obstacleFaceImpacted == Direction.EAST) ? rb.rotation - 90f : rb.rotation + 90f;
            }
            else if (warpInDirection == Direction.EAST)
            {
                rb.velocity = new Vector2(Mathf.Abs(velocityLastFrame.y * -1), velocityLastFrame.x);
                //rb.rotation = (obstacleFaceImpacted == Direction.NORTH) ? rb.rotation + 90f : rb.rotation - 90f;
            }
            else if (warpInDirection == Direction.SOUTH)
            {
                rb.velocity = new Vector2(velocityLastFrame.y, -Mathf.Abs(velocityLastFrame.x * -1));
                //rb.rotation = (obstacleFaceImpacted == Direction.EAST) ? rb.rotation + 90f : rb.rotation - 90f;
            }
            else if (warpInDirection == Direction.WEST)
            {
                rb.velocity = new Vector2(-Mathf.Abs(velocityLastFrame.y * -1), velocityLastFrame.x);
                //rb.rotation = (obstacleFaceImpacted == Direction.NORTH) ? rb.rotation - 90f : rb.rotation + 90f;
            }

            //  Rotate to match velocity when warping.
            rb.rotation = (Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg) - 90; // -90 because of sprite pointing up instead of right.
        }

        private Vector2 getDistanceToWarpAcrossObstacle(Obstacle obstacle)
        {
            Vector2 distance = Vector2.zero;

            // Warp to the other side.
            if (obstacleFaceImpacted == Direction.NORTH)
                distance = new Vector2(0, (obstacle.transform.position.y - transform.position.y) * 2);
            else if (obstacleFaceImpacted == Direction.EAST)
                distance = new Vector2((obstacle.transform.position.x - transform.position.x) * 2, 0);
            else if (obstacleFaceImpacted == Direction.SOUTH)
                distance = new Vector2(0, (obstacle.transform.position.y - transform.position.y) * 2);
            else if (obstacleFaceImpacted == Direction.WEST)
                distance = new Vector2((obstacle.transform.position.x - transform.position.x) * 2, 0);

            return distance;
        }

        private void calculateObstacleFaceImpacted()
        {
            RaycastHit2D hit = RaycastHelper.RaycastWithSelfMask(gameObject, velocityLastFrame);

            // Catch failed raycast hits.
            if (!hit)
                return;

            Obstacle obstacle = hit.collider.gameObject.GetComponent<Obstacle>();
            if (!obstacle)
                return;

            if (obstacle.tag == "Boundary Obstacle" || obstacle.tag == "Map Edge Obstacle")
            {
                if (obstacle.WarpInDirection == Direction.SOUTH && hit.point.y < hit.collider.transform.position.y)
                    obstacleFaceImpacted = Direction.SOUTH;
                else if (obstacle.WarpInDirection == Direction.NORTH && hit.point.y > hit.collider.transform.position.y)
                    obstacleFaceImpacted = Direction.NORTH;
                else if (obstacle.WarpInDirection == Direction.EAST && hit.point.x > hit.collider.transform.position.x)
                    obstacleFaceImpacted = Direction.EAST;
                else if (obstacle.WarpInDirection == Direction.WEST && hit.point.x < hit.collider.transform.position.x)
                    obstacleFaceImpacted = Direction.WEST;
            }
        }

        private void FixedUpdate()
        {
            velocityLastFrame = rb.velocity;
            angularVelocityLastFrame = rb.angularVelocity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (!rb)
                rb = GetComponentInParent<Rigidbody2D>();

            Vector3 direction = rb.velocity;
            Gizmos.DrawRay(transform.position, direction);
        }

        private void Start() => rb = GetComponentInParent<Rigidbody2D>();
    }
}