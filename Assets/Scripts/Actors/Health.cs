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
        public void Change(int amount) { current += amount; }
    }
}