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


        [SerializeField] private int maxHazards;
        [SerializeField] private List<Hazard> existingHazards;

        public static int MaxHazards { get { return _.maxHazards; } }
        public static Hazard InstantiateHazard(Hazard hazard, Vector3 position, Quaternion quaternion)
        {
            int numberOfHazards = _.existingHazards.Count;
            for (int i = 0; i < numberOfHazards; i++)
            {
                Hazard existingHazard = _.existingHazards[i];

                // Found one to reset.
                if (existingHazard.IsWaitingInQueue && hazard.FilterData.PrefabId == existingHazard.FilterData.PrefabId)
                    return existingHazard.ActivateInQueue(position, quaternion);
            }

            // None found and no room for more.
            if (_.existingHazards.Count >= _.maxHazards)
                return null;

            // None found and room for more.
            Hazard h = Instantiate(hazard, position, quaternion);
            _.existingHazards.Add(h);
            return hazard;
        }
    }
}