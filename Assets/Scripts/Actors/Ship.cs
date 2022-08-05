using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SpaceMage
{
    /// <summary>
    /// All ships in the game.
    /// </summary>
    public class Ship : MonoBehaviour
    {
        [SerializeField] private float baseManeuverability = 1.0f;
        public float BaseManeuverability { get { return baseManeuverability; } }


        [SerializeField] private Engine engine;
        public Engine Engine { get { return engine; } }
        public void RemoveEngine() { engine = null; /* TODO: Create a None Engine. */ }
        public void InstallEngine(Engine engine) { this.engine = engine; }
        public Engine ReplaceEngine(Engine engine)
        {
            Engine oldEngine = this.engine;
            this.engine = engine;

            return oldEngine;
        }
    }
}