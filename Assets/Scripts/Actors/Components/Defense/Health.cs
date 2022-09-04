using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    public class Health : MonoBehaviour
    {
        [SerializeField] protected int max;
        [SerializeField] protected int current;

        public int Max { get { return max; } }
        public int Current { get { return current; } }
        public bool IsFull() { return max == current; }

        public void Fill() { current = max; }
        public void SetHealth(int amount) { current = Mathf.Max(amount, max); }

        public void Reduce(int amount)
        {
            // Early out - not allowed to gain health.
            if (amount < 1)
                return;

            current = Mathf.Max(0, current - amount);
        }

        public void Increase(int amount)
        {
            // Early out - not allowed to lose health.
            if (amount < 1)
                return;

            current = Mathf.Min(max, current + amount);
        }
    }
}