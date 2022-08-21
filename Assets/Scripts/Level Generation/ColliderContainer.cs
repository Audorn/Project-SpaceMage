using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    /// <summary>
    /// Allow collider triggers to track contained colliders.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class ColliderContainer : MonoBehaviour
    {
        [SerializeField] private HashSet<Collider2D> colliders = new HashSet<Collider2D>();
        public HashSet<Collider2D> Colliders { get { return colliders; } }

        public bool Contains(Collider2D collider)
        {
            if (colliders.Contains(collider))
                return true;

            return false;
        }

        public void Add(Collider2D collider)
        {
            if (colliders.Contains(collider))
                return;

            colliders.Add(collider);
        }

        public void Remove(Collider2D collider)
        {
            if (colliders.Contains(collider))
                colliders.Remove(collider);
        }
    }
}