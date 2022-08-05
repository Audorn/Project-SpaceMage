using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using SpaceMage.Catalogs;

namespace SpaceMage
{
    public class Player : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>();

        [SerializeField] private Rigidbody2D rb;

        [SerializeField] private Ship ship;
        public Ship Ship { get { return ship; } }
        public bool IsInShip { get { return ship != null; } }


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            ship =  ShipCatalog.GetDefaultShip(transform.position, transform.rotation);
            ship.transform.parent = transform;
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
                Move();
        }

        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
//                var randomPosition = GetRandomPosition();
   //             transform.position = randomPosition;
     //           Position.Value = randomPosition;
            }
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
 //           Position.Value = GetRandomPosition();
        }




        // Acceleration and deceleration netcode.
        private Vector2 accelerateAmount = Vector2.zero;
        [ServerRpc]
        void SubmitAccelerationRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            accelerateShip(accelerateAmount);
        }

        public void AccelerateShip(Vector2 amount)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                accelerateShip(amount);
            }
            else
            {
                accelerateAmount = amount;
                SubmitAccelerationRequestServerRpc();
            }
        }

        private void accelerateShip(Vector2 amount)
        {
            float acceleration = amount.y * ship.Engine.BaseAcceleration;
            rb.AddForce(transform.up * acceleration * Time.fixedDeltaTime);
        }


        [ServerRpc]
        void SubmitDecelerationRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            decelerateShip(accelerateAmount);
        }

        public void DecelerateShip(Vector2 amount)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                decelerateShip(amount);
            }
            else
            {
                accelerateAmount = amount;
                SubmitDecelerationRequestServerRpc();
            }
        }

        private void decelerateShip(Vector2 amount)
        {
            float deceleration = amount.y * ship.Engine.BaseDeceleration;
            rb.AddForce(transform.up * deceleration * Time.fixedDeltaTime);
        }

        // Turn netcode.
        private Vector2 turnAmount = Vector2.zero;
        [ServerRpc]
        void SubmitTurnRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            turnShip(turnAmount);
        }

        public void TurnShip(Vector2 amount)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                turnShip(amount);
            }
            else
            {
                turnAmount = amount;
                SubmitTurnRequestServerRpc();
            }

        }

        private void turnShip(Vector2 amount)
        {
            float enginePower = Mathf.Sqrt((ship.Engine.BaseAcceleration + ship.Engine.BaseDeceleration) / 2);
            
            float turnAmount = amount.x * ship.BaseManeuverability * enginePower;
            rb.MoveRotation(rb.rotation + turnAmount * Time.fixedDeltaTime);
        }
    }
}