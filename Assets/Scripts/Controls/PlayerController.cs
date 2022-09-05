using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using SpaceMage.Actors;

namespace SpaceMage.Controls
{
    /// <summary>
    /// Handles player action routing. Action comes in from the Player Input and sent to whatever component performs it.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Pilot pilot;
        public Pilot Pilot { get { return pilot; } }
        private void Start() { pilot = GetComponent<Pilot>(); }

        // Track acceleration and turning.
        private bool isAccelerationControlHeld = false;
        private bool isTurnControlHeld = false;
        private Vector2 accelerationAmount = Vector2.zero;
        private Vector2 turnAmount = Vector2.zero;

        // Because the Input system doesn't have an equivalent to .getkey(''),
        // the state of the action has to be tracked to handle holding a key down.
        public void TrackAccelerationInput(InputAction.CallbackContext context)
        {
            Vector2 moveValue = context.ReadValue<Vector2>();
            if (context.phase == InputActionPhase.Started)
                isAccelerationControlHeld = true;
            else if (context.phase == InputActionPhase.Canceled)
                isAccelerationControlHeld = false;

            accelerationAmount = moveValue;
        }

        // Because the Input system doesn't have an equivalent to .getkey(''),
        // the state of the action has to be tracked to handle holding a key down.
        public void TrackTurnInput(InputAction.CallbackContext context)
        {
            Vector2 moveValue = context.ReadValue<Vector2>();
            if (context.phase == InputActionPhase.Started)
                isTurnControlHeld = true;
            else if (context.phase == InputActionPhase.Canceled)
                isTurnControlHeld = false;

            turnAmount = moveValue * -1;
        }

        // See if the tracked input is reporting acceleration or turning.
        private void FixedUpdate()
        {
            if (pilot.IsInShip && isAccelerationControlHeld)
                AccelerateShip();
            // TODO: allow movement outside of ship.

            if (pilot.IsInShip && isTurnControlHeld)
                TurnShip();
            // TODO: allow selection of interactibles outside of ship.
        }

        // Tell the player to accelerate or turn the ship.
        private void AccelerateShip() { pilot.AccelerateShip(accelerationAmount); } /* Could be acceleration or deceleration. */
        private void TurnShip() { pilot.TurnShip(turnAmount); }
    }
}