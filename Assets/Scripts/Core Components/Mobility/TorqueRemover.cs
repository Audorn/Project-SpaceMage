using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Ships;

namespace SpaceMage.Actors
{
    /// <summary>
    /// Reduce rigidbody angular velocity to zero over time. Rate is overridden by Ship objects.
    /// </summary>
    public class TorqueRemover : MonoBehaviour
    {
        private Rigidbody2D rb;                             // Assigned in Start().

        [SerializeField] private float rate;                // Editor configurable.

        public float Rate => rate;
        public bool IsRecovering => rb.angularVelocity != 0;
        public void setRate(float rate) => this.rate = rate;

        private void FixedUpdate()
        {
            float angularVelocity = rb.angularVelocity;

            // Reduce inertia based on modified maneuverability in case of collisions.
            if (angularVelocity > 0)
                rb.angularVelocity = Mathf.Clamp(angularVelocity - rate, 0, angularVelocity);
            else if (angularVelocity < 0)
                rb.angularVelocity = Mathf.Clamp(angularVelocity + rate, angularVelocity, 0);
        }

        private void Start() => rb = GetComponentInParent<Rigidbody2D>();
    }
}