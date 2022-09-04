using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    [System.Serializable]
    public class RotationRange
    {
        [SerializeField] private int max;
        [SerializeField] private int min;

        public int Max => max;
        public int Min => min;

        public int GetRotationWithinRange()
        {
            int velocity = Random.Range(min, max);
            return velocity;
        }
    }
}