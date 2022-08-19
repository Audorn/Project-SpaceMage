using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using SpaceMage.Catalogs;
using SpaceMage.Entities;

namespace SpaceMage
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        public bool IsRecoveringFromInertia { get { return rb.angularVelocity > 0; } }

        // Specs.
        [SerializeField] private Ship ship;
        public Ship Ship { get { return ship; } }
        public bool IsInShip { get { return ship != null; } }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            SetShipToDefault();
        }

        private void FixedUpdate()
        {
            // Reduce inertia based on modified maneuverability in case of collisions.
            if (rb.angularVelocity > 0)
                rb.angularVelocity = Mathf.Clamp(rb.angularVelocity - ship.InertiaRecovery, 0, rb.angularVelocity);
            else if (rb.angularVelocity < 0)
                rb.angularVelocity = Mathf.Clamp(rb.angularVelocity + ship.InertiaRecovery, rb.angularVelocity, 0);
        }

        private void SetShipToDefault()
        {
            ship = ShipCatalog.GetDefaultShip(transform.position, transform.rotation);
            ship.transform.parent = transform;
        }

        // Determine whether ship is accelerating or decelerating.
        public void AccelerateShip(Vector2 inputAmount)
        {
            float acceleration = inputAmount.y;

            if (acceleration > 0)
                accelerateeShip(acceleration);
            else if (acceleration < 0)
                decelerateShip(acceleration);
        }

        // Perform acceleration or deceleration of ship.
        public void accelerateeShip(float acceleration)
        {
            acceleration *= ship.Engine.BaseAcceleration;
            rb.AddForce(transform.up * acceleration * Time.fixedDeltaTime);
        }
        public void decelerateShip(float deceleration)
        {
            deceleration *= ship.Engine.BaseDeceleration;
            rb.AddForce(transform.up * deceleration * Time.fixedDeltaTime);
        }

        // Perform turning of ship.
        public void TurnShip(Vector2 inputAmount)
        {
            // Execute player input rotation.
            float turnAmount = inputAmount.x * ship.Maneuverability;
            rb.MoveRotation(rb.rotation + turnAmount * Time.fixedDeltaTime);
        }
    }
}