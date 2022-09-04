using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    /// <summary>
    /// This entity deals damage when it collides with something with health.
    /// </summary>
    public class DamageOnCollision : MonoBehaviour
    {
        private int framesUntilCanDealDamage = 0;
        private bool dealtDamageRecently { get { return framesUntilCanDealDamage > 0; } }

        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected int damage;                // Base damage used for calculations.
        [SerializeField] protected int interval;                  // Min number of frames between impacts.

        public int Damage { get { return damage; } }
        public int Interval { get { return interval; } }

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
            framesUntilCanDealDamage = interval;
        }

        protected void FixedUpdate()
        {
            if (dealtDamageRecently)
                framesUntilCanDealDamage--;
        }

        protected void Start()
        {
            Rigidbody2D objectRB = GetComponent<Rigidbody2D>();
            if (objectRB)
                rb = objectRB;
            else
                rb = GetComponentInParent<Rigidbody2D>();
        }
    }
}