using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Entities;

namespace SpaceMage.Catalogs
{
    /// <summary>
    /// All ships available in the game. Default ship is 0.
    /// </summary>
    public class ShipCatalog : MonoBehaviour
    {
        private static ShipCatalog _;
        public static ShipCatalog Singleton { get { return _; } }
        private void Awake() { _ = this; }

        [SerializeField] private List<Ship> ships = new List<Ship>();
        public List<Ship> Ships { get { return ships; } }

        public static Ship GetDefaultShip(Vector3 position, Quaternion rotation) {
            Ship ship = Instantiate(_.ships[0], position, rotation);

            Engine engine = EngineCatalog.GetDefaultEngine(position, rotation);
            engine.transform.parent = ship.transform;
            ship.InstallEngine(engine);

            return ship;
        }
    }
}