using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TorqueRemover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float rate;
        public float Rate { get { return rate; } }
        public bool IsRecovering { get { return rb.angularVelocity != 0; } }
        public void setRate(float amount) { rate = amount; }

        private void FixedUpdate()
        {
            // Reduce inertia based on modified maneuverability in case of collisions.
            if (rb.angularVelocity > 0)
                rb.angularVelocity = Mathf.Clamp(rb.angularVelocity - rate, 0, rb.angularVelocity);
            else if (rb.angularVelocity < 0)
                rb.angularVelocity = Mathf.Clamp(rb.angularVelocity + rate, rb.angularVelocity, 0);
        }
        private void Start() { rb = GetComponent<Rigidbody2D>(); }
    }
}