using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Actors
{
    /// <summary>
    /// Deals damage to any Health it collides with. Gets rb from parent if none found on self.
    /// </summary>
    public class DamageOnCollision : MonoBehaviour
    {
        private Rigidbody2D rb;                             // Assigned in Start(). Gets from parent if none found.

        private int framesUntilCanDealDamage = 0;           // Tracked and set internally.

        [SerializeField] protected int damage;              // Editor configurable.
        [SerializeField] protected int intervalInFrames;    // Editor configurable.

        private bool dealtDamageRecently => framesUntilCanDealDamage > 0;
        public int Damage => damage;
        public int IntervalInFrames => intervalInFrames;

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            // Early out if waiting for the damageInterval to expire.
            if (dealtDamageRecently)
                return;

            Health health = collision.collider.GetComponent<Health>();

            // Early out - other object can't take damage.
            if (!health)
                return;

            //float collisionSpeed = collision.relativeVelocity.magnitude;
            health.Reduce(damage);
            framesUntilCanDealDamage = intervalInFrames;
        }

        protected void FixedUpdate()
        {
            if (dealtDamageRecently)
                framesUntilCanDealDamage--;
        }

        protected void Start() => rb = GetComponentInParent<Rigidbody2D>();
    }
}