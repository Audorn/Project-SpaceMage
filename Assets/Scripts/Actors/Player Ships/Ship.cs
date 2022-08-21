using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SpaceMage.Entities
{
    /// <summary>
    /// A ship controlled by a player pilot. It is capable of taking damage and dying, and can deal damage on contact.
    /// </summary>
    public class Ship : MonoBehaviour
    {
        [SerializeField] private FilterData filterData;
        public FilterData FilterData { get { return filterData; } }

        // =================================================
        // ==================== CONTROL ====================
        // =================================================
        [SerializeField] private float baseManeuverability = 1.0f;
        [SerializeField] private float baseStopSpinningRate = 0.1f;
        public float BaseManeuverability { get { return baseManeuverability; } }
        public float BaseStopSpinningRate { get { return baseStopSpinningRate; } }
        public float Maneuverability { get { return (engine) ? baseManeuverability * engine.Power : 0f; } }
        public float StopSpinningRate { get { return (engine) ? baseStopSpinningRate * engine.Power : 0f; } }

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