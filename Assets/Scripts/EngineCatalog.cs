using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Catalogs
{
    public class EngineCatalog : MonoBehaviour
    {
        private static EngineCatalog _;
        public static EngineCatalog Singleton { get { return _; } }
        private void Start() { _ = this; }

        [SerializeField] private List<Engine> engines = new List<Engine>();
        public List<Engine> Engines { get { return engines; } }

        public static Engine GetDefaultEngine(Vector3 position, Quaternion rotation)
        {
            return Instantiate(_.engines[0], position, rotation);
        }
    }
}