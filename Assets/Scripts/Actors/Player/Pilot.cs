using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using SpaceMage.Catalogs;
using SpaceMage.Ships;

namespace SpaceMage.Actors
{
    public class Pilot : Actor
    {
        [SerializeField] private TorqueRemover iStopSpinning;
        [SerializeField] private Ship ship;
        public Ship Ship { get { return ship; } }
        public bool IsInShip { get { return ship != null; } }

        private void SetShipToDefault()
        {
            ship = ShipCatalog.GetDefaultShip(transform.position, transform.rotation);
            ship.transform.parent = transform;
            Player.SetPlayerShip(ship);
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

        protected void Start()
        {
            SetShipToDefault();
            iStopSpinning = GetComponent<TorqueRemover>();
            if (iStopSpinning && ship)
                iStopSpinning.setRate(ship.StopSpinningRate);
        }
    }
}