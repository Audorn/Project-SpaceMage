using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(Collider2D))]
    public class TrackedInColliderContainer : MonoBehaviour
    {
        private HashSet<ColliderContainer> trackedByContainers = new HashSet<ColliderContainer>();
        private void OnTriggerEnter2D(Collider2D collision)
        {
            ColliderContainer container = collision.gameObject.GetComponent<ColliderContainer>();
            if (container == null)
                return;

            container.Add(GetComponent<Collider2D>());
            trackedByContainers.Add(container);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            ColliderContainer container = collision.gameObject.GetComponent<ColliderContainer>();
            if (container == null)
                return;

            container.Remove(GetComponent<Collider2D>());
            trackedByContainers.Remove(container);
        }

        private void OnDestroy()
        {
            foreach (ColliderContainer container in trackedByContainers)
                container.Remove(GetComponent<Collider2D>());
        }
    }
}