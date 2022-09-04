using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MotionLimiter : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField] private float maxVelocity;
        [SerializeField] private float maxRotation;

        private float sqrMaxVelocity;

        private void FixedUpdate()
        {
            if (rb.velocity.sqrMagnitude > sqrMaxVelocity)
                rb.velocity = rb.velocity.normalized * maxVelocity;

            if (rb.angularVelocity > maxRotation)
                rb.angularVelocity = maxRotation;
            else if (rb.angularVelocity < -maxRotation)
                rb.angularVelocity = -maxRotation;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            sqrMaxVelocity = maxVelocity * maxVelocity; // Store for performance.
        }
    }
}