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
        // Get the pilot. TODO: Create parent class to get instead.
        [SerializeField] private Player pilot;
        public Player Pilot { get { return pilot; } }

        // Specs.
        [SerializeField] private float baseManeuverability = 1.0f;
        public float BaseManeuverability { get { return baseManeuverability; } }
        public float Maneuverability { get { return baseManeuverability * engine.Power; } }
        public float InertiaRecovery { get { return Mathf.Sqrt(Mathf.Sqrt(Maneuverability)); } }
        private bool isRecoveringFromInertia { get { return pilot.IsRecoveringFromInertia; } }

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