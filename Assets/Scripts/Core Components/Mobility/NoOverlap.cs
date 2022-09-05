using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Actors
{
    /// <summary>
    /// Force overlapping colliders that can collide to push away from each other.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class NoOverlap : MonoBehaviour
    {
        private Rigidbody2D rb;                             // Assigned in Awake().

        private void OnCollisionStay2D(Collision2D collision)
        {
            Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb && otherRb)
            {
                float degree = Vector2.Angle(rb.position, otherRb.position);
                Vector2 direction = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad)).normalized;
                rb.AddForce(direction);
                otherRb.AddForce(direction * -1);
            }
        }

        private void Awake() => rb = GetComponent<Rigidbody2D>();
    }
}