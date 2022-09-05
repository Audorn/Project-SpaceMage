using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Actors
{
    /// <summary>
    /// Clamps velocity and rotation to a minimum and maximum.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class MotionLimiter : MonoBehaviour
    {
        private Rigidbody2D rb;                                     // Assigned in Awake().
        private float maxVelocity;                                  // Retrieved in Awake().
        private float minVelocity;                                  // Retrieved in Awake().
        private float sqrMaxVelocity;                               // Retrieved in Awake().
        private float sqrMinVelocity;                               // Retrieved in Awake().
        private float maxRotation;                                  // Retrieved in Awake().
        private float minRotation;                                  // Retrieved in Awake().

        [SerializeField] private VelocityRange velocityRange;       // Editor configurable.
        [SerializeField] private RotationRange rotationRange;       // Editor configurable.

        public VelocityRange VelocityRange => velocityRange;
        public RotationRange RotationRange => rotationRange;

        public void SetMaxVelocity(float maxVelocity) { this.maxVelocity = maxVelocity; sqrMaxVelocity = maxVelocity * maxVelocity; }
        public void SetMinVelocity(float minVelocity) { this.minVelocity = minVelocity; sqrMinVelocity = minVelocity * minVelocity; }
        public void SetMaxRotation(float maxRotation) => this.maxRotation = maxRotation;
        public void SetMinRotation(float minRotation) => this.minRotation = minRotation;

        private void FixedUpdate()
        {
            float sqrVelocity = rb.velocity.sqrMagnitude;
            float angularVelocity = rb.angularVelocity;

            // Velocity greater or lesser.
            if (sqrVelocity > sqrMaxVelocity)
                rb.velocity = rb.velocity.normalized * maxVelocity;
            else if (sqrVelocity != 0 && sqrVelocity < sqrMinVelocity)
                rb.velocity = rb.velocity.normalized * minVelocity;
            else if (sqrVelocity == 0)
                rb.velocity = new Vector2(Random.Range(0, 1), Random.Range(0, 1)).normalized * minVelocity;

            // Rotation greater in either direction.
            if (angularVelocity > maxRotation)
                rb.angularVelocity = maxRotation;
            else if (angularVelocity < -maxRotation)
                rb.angularVelocity = -maxRotation;

            // Rotation lesser in either direction.
            else if (angularVelocity > 0 && angularVelocity < minRotation)
                rb.angularVelocity = minRotation;
            else if (angularVelocity < 0 && angularVelocity > minRotation)
                rb.angularVelocity = -minRotation;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            maxVelocity = velocityRange.Max;
            minVelocity = velocityRange.Min;
            sqrMaxVelocity = velocityRange.MaxSqr;
            sqrMinVelocity = velocityRange.MinSqr;
            maxRotation = rotationRange.Max;
            minRotation = rotationRange.Min;
        }
    }
}