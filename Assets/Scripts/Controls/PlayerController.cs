using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

namespace SpaceMage.Controls
{
    /// <summary>
    /// Handles player action routing. Action comes in from the Player Input and sent to whatever component performs it.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player player;
        public Player Player { get { return player; } }
        private void Start() { player = GetComponent<Player>(); }

        void Update()
        {
            // Move the local player if is server or host.
/*            if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
            {
                foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
                    NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<Player>().Move();
            }

            // Request move of respective client player. (Not quite right)
            else
            {
                var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                var player = playerObject.GetComponent<Player>();
                player.Move();
            }*/
        }

        // Track acceleration.
        private bool isAccelerationControlHeld = false;
        private Vector2 accelerationAmount = Vector2.zero;
        public void ModifyAcceleration(InputAction.CallbackContext context)
        {

            if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
            {
                foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
                    NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<PlayerController>().UpdateAccelerationData(context);
            }
            else
            {
                var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                var playerController = playerObject.GetComponent<PlayerController>();
                playerController.UpdateAccelerationData(context);
            }
        }
        public void UpdateAccelerationData(InputAction.CallbackContext context)
        {
            Vector2 moveValue = context.ReadValue<Vector2>();
            if (context.phase == InputActionPhase.Started)
                isAccelerationControlHeld = true;
            else if (context.phase == InputActionPhase.Canceled)
                isAccelerationControlHeld = false;

            accelerationAmount = moveValue;
        }

        // Track turning.
        private bool isTurnControlHeld = false;
        private Vector2 turnAmount = Vector2.zero;
        public void ModifyTurn(InputAction.CallbackContext context)
        {
            if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
            {
                foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
                    NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<PlayerController>().UpdateTurnData(context);
            }
            else
            {
                var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                var playerController = playerObject.GetComponent<PlayerController>();
                playerController.UpdateTurnData(context);
            }
        }
        public void UpdateTurnData(InputAction.CallbackContext context)
        {
            Vector2 moveValue = context.ReadValue<Vector2>();
            if (context.phase == InputActionPhase.Started)
                isTurnControlHeld = true;
            else if (context.phase == InputActionPhase.Canceled)
                isTurnControlHeld = false;

            turnAmount = moveValue * -1;
        }

        // Handle acceleration and turn here because the input system doesn't allow holding keys.
        private void FixedUpdate()
        {
            if (player.IsInShip && isAccelerationControlHeld)
                AccelerateShip();
            // TODO: allow movement outside of ship.

            if (player.IsInShip && isTurnControlHeld)
                TurnShip();
            // TODO: allow selection of interactibles outside of ship.
        }

        private void AccelerateShip()
        {
            if (accelerationAmount.y > 0)
                player.AccelerateShip(accelerationAmount);
            else if (accelerationAmount.y < 0)
                player.DecelerateShip(accelerationAmount);
        }
        private void TurnShip() { player.TurnShip(turnAmount); }
    }
}