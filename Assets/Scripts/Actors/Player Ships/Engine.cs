using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    // Handles acceleration and deceleration of an actor.
    public class Engine : MonoBehaviour
    {
        [SerializeField] private float baseAcceleration = 1.0f;
        public float BaseAcceleration { get { return baseAcceleration; } }

        [SerializeField] private float baseDeceleration = 1.0f;
        public float BaseDeceleration { get { return baseDeceleration; } }

        public float Power { get { return Mathf.Sqrt((baseAcceleration + baseDeceleration) / 2);  } }
    }
}