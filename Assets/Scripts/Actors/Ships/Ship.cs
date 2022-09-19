using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Netcode;
using SpaceMage.Actors;

namespace SpaceMage.Ships
{
    /// <summary>
    /// A ship controlled by a pilot.
    /// </summary>
    [RequireComponent(typeof(TorqueRemover))]
    public class Ship : MonoBehaviour
    {
        private TorqueRemover torqueRemover;                                                    // Assigned in Start().

        [SerializeField] private string model;                                                  // Editor configurable.
        [SerializeField] private HullStrength hullStrength;                                     // Editor configurable.
        [SerializeField] private CatalogFilterData filterData;                                  // Editor configurable.
        [SerializeField] private float baseManeuverability;                                     // Editor configurable.
        [SerializeField] private float baseTorqueRemovalRate;                                   // Editor configurable.

        [SerializeField] private Engine engine;                                                 // Editor configurable.

        [SerializeField] private List<Hardpoint> hardpoints = new List<Hardpoint>();            // Editor configurable.
        [SerializeField] private int maxHardpoints;                                             // Editor configurable.

        public string Model => model;
        public HullStrength HullStrength => hullStrength;
        public CatalogFilterData FilterData => filterData;
        public float BaseManeuverability => baseManeuverability;
        public float BaseTorqueRemovalRate => baseTorqueRemovalRate;
        public float Maneuverability => (engine) ? baseManeuverability * engine.Power : 0f;
        public string UIManeuverability => (Maneuverability < 2) ? "Poor" : (Maneuverability < 4) ? "Average" : (Maneuverability < 6) ? "Swift" : "Agile";
        public float TorqueRemovalRate => (engine) ? baseTorqueRemovalRate * engine.Power : 0f;

        public Engine Engine { get { return engine; } }
        public void RemoveEngine() { engine = null; /* TODO: Create a None Engine. */ }
        public void InstallEngine(Engine engine) { this.engine = engine; }
        public Engine ReplaceEngine(Engine engine)
        {
            Engine oldEngine = this.engine;
            this.engine = engine;
            torqueRemover.setRate(TorqueRemovalRate);

            return oldEngine;
        }

        public List<Hardpoint> Hardpoints => hardpoints;
        public int MaxHardpoints => maxHardpoints;
        public void RemoveHardpoint(int index) => hardpoints.RemoveAt(index);
        public void RemoveHardpoint(string id) => hardpoints.Remove(hardpoints.Find(hardpoint => hardpoint.Id == id));
        public void InstallHardpoint(Hardpoint hardpoint)
        {
            // Early Out - no room.
            if (hardpoints.Count >= maxHardpoints)
                return;

            hardpoints.Add(hardpoint);
        }

        private void RefillAllAmmo()
        {
            int numberOfHardpoints = hardpoints.Count;
            for (int i = 0; i < numberOfHardpoints; i++)
                hardpoints[i].RefillAmmo();
        }

        private void Start()
        {
            torqueRemover = GetComponent<TorqueRemover>();
            RefillAllAmmo();
        }
    }
}