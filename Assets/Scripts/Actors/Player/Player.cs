using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Ships;

namespace SpaceMage
{
    public class Player : MonoBehaviour
    {
        // Singleton.
        private static Player _;
        public static Player Singleton { get { return _; } }
        private void Awake() { _ = this; }


        private Ship ship;

        public static Ship Ship => _.ship;
        public static void SetPlayerShip(Ship playerShip) => _.ship = playerShip;
    }
}