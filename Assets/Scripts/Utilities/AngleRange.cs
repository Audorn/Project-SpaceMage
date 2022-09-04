using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    [System.Serializable]
    public class AngleRange
    {
        [SerializeField] private int max;
        [SerializeField] private int min;

        public int Max => max;
        public int Min => min;

        public Vector2 GetNormalizedVectorWithinRange(Vector2 originVector)
        {
            int degree = Random.Range(min, max);
            Vector2 resultVector = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad)).normalized * originVector;
            return resultVector;
        }
    }
}