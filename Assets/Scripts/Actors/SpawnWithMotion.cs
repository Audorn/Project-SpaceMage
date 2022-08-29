using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpawnWithMotion : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;

        [SerializeField] private Vector2 maxDirection;
        [SerializeField] private Vector2 minDirection;
        [SerializeField] private float maxVelocity;
        [SerializeField] private float minVelocity;
        [SerializeField] private float maxRotation;
        [SerializeField] private float minRotation;

        public void SetMinDirection(Vector2 minDirection) { this.minDirection = minDirection; }
        public void SetMaxDirection(Vector2 maxDirection) { this.maxDirection = maxDirection; }
        public void ModifyVelocity(float modifier) { minVelocity *= modifier; maxVelocity *= modifier; }
        public void ModifyRotation(float modifier) { minRotation *= modifier; maxRotation *= modifier; }

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody2D>();

            Vector2 direction = Overseer.GetRandomDirection(minDirection, maxDirection);
            float velocity = Random.Range(minVelocity, maxVelocity);
            float torque = Overseer.GetRandomRotation(minRotation, maxRotation);
            rb.AddForce(direction * velocity);
            rb.AddTorque(torque);
        }
    }
}