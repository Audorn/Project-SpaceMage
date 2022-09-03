using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Entities;

namespace SpaceMage
{
    /// <summary>
    /// The Overseer is responsible for providing information on request. A utility class.
    /// </summary>
    public class RaycastHelper : MonoBehaviour
    {
        public static Vector2 GetRandomDirection(Vector2 min, Vector2 max) { return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y)); }
        public static float GetRandomRotation(float min, float max) { return Random.Range(min, max); }

        public static RaycastHit2D RaycastWithSelfMask(GameObject gameObject, Vector2 direction)
        {
            int oldLayer = gameObject.layer;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            int layerToIgnore = 1 << gameObject.layer;
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, direction, layerToIgnore);
            gameObject.layer = oldLayer;

            return hit;
        }
    }
}