using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    /// <summary>
    /// This entity deals damage when it collides with something else that can take damage.
    /// </summary>
    public class IDealImpactDamage : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected int impactBaseDamage;                // Base damage used for calculations.
        [SerializeField] protected int damageInterval;                  // Min number of frames between impacts.
        public int ImpactBaseDamage { get { return impactBaseDamage; } }
        public int DamageInterval { get { return damageInterval; } }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            // Early out if waiting for the damageInterval to expire.
            if (dealtDamageRecently)
                return;

            ITakeDamage damageableTarget = collision.collider.GetComponent<ITakeDamage>();
            if (!damageableTarget)
                return;

            float collisionSpeed = collision.relativeVelocity.magnitude;
            dealImpactDamage(damageableTarget, collisionSpeed);
        }
        protected virtual void dealImpactDamage(ITakeDamage damageableTarget, float collisionSpeed)
        {
            damageableTarget.TakeDamage(impactBaseDamage, collisionSpeed);
            framesUntilCanDealDamage = damageInterval;
        }

        private int framesUntilCanDealDamage = 0;
        private bool dealtDamageRecently { get { return framesUntilCanDealDamage > 0; } }
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