using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    [System.Serializable]
    public class VelocityRange
    {
        [SerializeField] private float max;
        [SerializeField] private float min;

        public float Max => max;
        public float Min => min;

        public float GetVelocityWithinRange()
        {
            float velocity = Random.Range(min, max);
            return velocity;
        }
    }
}