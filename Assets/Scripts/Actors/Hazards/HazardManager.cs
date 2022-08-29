using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    public class HazardManager : MonoBehaviour
    {
        // Singleton allowing access to the game state handler through the static class.
        private static HazardManager _;
        public static HazardManager Singleton { get { return _; } }
        private void Awake() { _ = this; }


        [SerializeField] private int maxPrimaryHazards;
        [SerializeField] private List<Hazard> existingPrimaryHazards;       // Spawned from SpawnZones.
        [SerializeField] private List<Hazard> existingSecondaryHazards;  // Spawned from main Hazards.
        [SerializeField] private List<Hazard> existingTertiaryHazards;   // Spawned from secondary Hazards.
        [SerializeField] private List<Hazard> existingQuaternaryHazards; // Spawned from tertiary Hazards.

        public static int MaxHazards { get { return _.maxPrimaryHazards; } }
        public static Hazard InstantiateHazard(Hazard hazard, Vector3 position, Quaternion quaternion, Order order = Order.PRIMARY)
        {
            int numberOfHazards = (order == Order.PRIMARY) ? _.existingPrimaryHazards.Count :
                                  (order == Order.SECONDARY) ? _.existingSecondaryHazards.Count :
                                  (order == Order.TERTIARY) ? _.existingTertiaryHazards.Count :
                                  (order == Order.QUATERNARY) ? _.existingQuaternaryHazards.Count : 0;

            for (int i = 0; i < numberOfHazards; i++)
            {
                Hazard existingHazard = (order == Order.PRIMARY) ? _.existingPrimaryHazards[i] :
                                        (order == Order.SECONDARY) ? _.existingSecondaryHazards[i] :
                                        (order == Order.TERTIARY) ? _.existingTertiaryHazards[i] :
                                        (order == Order.QUATERNARY) ? _.existingQuaternaryHazards[i] : null;

                // Found one to reset.
                if (existingHazard && existingHazard.IsWaitingInQueue && hazard.FilterData.PrefabId == existingHazard.FilterData.PrefabId)
                    return existingHazard.ActivateInQueue(position, quaternion);
            }

            // None found in the primary list and no room for more.
            if (order == Order.PRIMARY && _.existingPrimaryHazards.Count >= _.maxPrimaryHazards)
                return null;

            // None found and room for more.
            Hazard h = Instantiate(hazard, position, quaternion);
            SpawnsChildren spawnsChildren = h.GetComponent<SpawnsChildren>();
            if (spawnsChildren)
                spawnsChildren.SetOrder(order);

            if (order == Order.PRIMARY) _.existingPrimaryHazards.Add(h);
            else if (order == Order.SECONDARY) _.existingSecondaryHazards.Add(h);
            else if (order == Order.TERTIARY) _.existingTertiaryHazards.Add(h);
            else if (order == Order.QUATERNARY) _.existingQuaternaryHazards.Add(h);

            return hazard;
        }
    }
}