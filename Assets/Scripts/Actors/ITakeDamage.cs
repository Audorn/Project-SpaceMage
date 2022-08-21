using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    /// <summary>
    /// This gameobject can take damage and possibly die.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class ITakeDamage : MonoBehaviour
    {
        [SerializeField] protected Health health;                               // Required component.
        [SerializeField] protected IDie iDie;                                   // Optional component allowing death.

        [SerializeField] protected int minSpeedForImpactDamage;                 // Hard minimum speed for impact to occur.
        [SerializeField] protected float extraSpeedDamageMultiplier;            // Adds damage for speed over minimum.

        public int MinSpeedForImpactDamage { get { return minSpeedForImpactDamage; } }
        public float ExtraSpeedDamageMultiplier { get { return extraSpeedDamageMultiplier; } }

        public virtual void TakeDamage(int amount, float collisionSpeed = 0f) { // collisionSpeed only matters in some cases, like impacts.

            // Early out - no surprise healing.
            if (amount < 0)
                return;

            // Early out - impact velocity wasn't high enough to deal damage.
            if (collisionSpeed > 0 && collisionSpeed < MinSpeedForImpactDamage)
                return;

            // Apply any necessary modifiers if this was impact damage
            if (collisionSpeed >= minSpeedForImpactDamage)
                amount += (int)(extraSpeedDamageMultiplier * (collisionSpeed - minSpeedForImpactDamage));

            health.Change(-amount);
            if (iDie && health.Current <= 0)
                iDie.Die();
        }

        private void Awake() { health = GetComponent<Health>(); }
        private void Start() { iDie = GetComponentInParent<IDie>(); }
    }
}