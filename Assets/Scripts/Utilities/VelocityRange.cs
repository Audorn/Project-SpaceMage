using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    /// <summary>
    /// Allows convenient access to random and square velocities.
    /// </summary>
    [System.Serializable]
    public class VelocityRange
    {
        [SerializeField] private float max;                 // Editor configurable.
        [SerializeField] private float min;                 // Editor configurable.

        public float Max => max;
        public float Min => min;
        public float MaxSqr => max * max;
        public float MinSqr => min * min;

        public float GetVelocityWithinRange()
        {
            float velocity = Random.Range(min, max);
            return velocity;
        }
    }
}