using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Entities;

namespace SpaceMage
{
    /// <summary>
    /// The Overseer is responsible for providing information on request. A utility class.
    /// </summary>
    public class Overseer : MonoBehaviour
    {
        public static Vector2 GetRandomDirection(Vector2 min, Vector2 max) { return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y)); }
        public static float GetRandomRotation(float min, float max) { return Random.Range(min, max); }
    }
}