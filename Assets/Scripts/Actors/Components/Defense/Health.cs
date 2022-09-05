using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Actors
{
    /// <summary>
    /// Manages the current and max health of an actor.
    /// </summary>
    [RequireComponent(typeof(Actor))]
    public class Health : MonoBehaviour
    {
        private Actor actor;                                // Assigned in Awake().

        [SerializeField] protected int max;                 // Editor configurable.
        [SerializeField] protected int current;             // Editor configurable.

        public int Max => max;
        public int Current => current;
        public bool IsFull() => current >= max;

        public void Fill() => current = max;
        public void Set(int amount, bool allowOverFill = false) => current = (allowOverFill) ? Mathf.Max(0, amount) : Mathf.Clamp(amount, 0, max) ;

        /// <summary>
        /// Reduce health by an amount. Cannot be reduced below zero.
        /// </summary>
        /// <param name="amount">Cannot be negative.</param>
        public void Reduce(int amount)
        {
            // Early out - not allowed to gain health.
            if (amount < 1)
                return;

            Set(current - amount);
        }

        /// <summary>
        /// Increase health by an amount.
        /// </summary>
        /// <param name="amount">Cannot be negative.</param>
        /// <param name="allowOverFill">Optional: Can current health exceed max?</param>
        public void Increase(int amount, bool allowOverFill = false)
        {
            // Early out - not allowed to lose health.
            if (amount < 1)
                return;

            Set(current + amount, allowOverFill);
        }

        private void Start() => actor.ActivateInPoolEvent.AddListener(Fill);
        private void Awake() => actor = GetComponent<Actor>();
    }
}