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
        // =================================================
        // ==================== SORTING ====================
        // =================================================
        [SerializeField] private Faction faction;
        [SerializeField] private ThreatLevel threatLevel;
        [SerializeField] private Rarity rarity;

        public Faction Faction { get { return faction; } }
        public ThreatLevel ThreatLevel { get { return threatLevel; } }
        public Rarity Rarity { get { return rarity; } }

        // =================================================
        // ==================== CONTROL ====================
        // =================================================
        [SerializeField] private Player pilot;
        public Player Pilot { get { return pilot; } }

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

        protected void Awake()
        {
            pilot = GetComponentInParent<Player>();
        }
    }
}