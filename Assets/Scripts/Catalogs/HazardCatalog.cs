using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Entities;

namespace SpaceMage.Catalogs
{
    public class HazardCatalog : MonoBehaviour
    {
        private static HazardCatalog _;
        public static HazardCatalog Singleton { get { return _; } }
        private void Awake() { _ = this; }

        [SerializeField] private List<Hazard> hazards = new List<Hazard>();
        public List<Hazard> Hazards { get { return hazards; } }

        // =================================================
        // ==================== SORTING ====================
        // =================================================
        /// <summary>
        /// Returns a list of hazards selected based on the filters.
        /// They still need to be instantiated.
        /// </summary>
        /// <param name="sortingData">Faction, ThreatLevel, Rarity, NumberToGet, and IsEachUnique.</param>
        /// <returns>A list of hazards.</returns>
        public static List<Hazard> GetHazard(SortingData sortingData)
        {
            List<Hazard> validHazards = new List<Hazard>();
            foreach (Hazard hazard in _.hazards)
            {
                if (hazard.Faction != Faction.NONE && hazard.Faction == sortingData.Faction ||
                    hazard.ThreatLevel != ThreatLevel.NONE && hazard.ThreatLevel == sortingData.ThreatLevel ||
                    hazard.Rarity != Rarity.NONE && hazard.Rarity == sortingData.Rarity ||
                    hazard.Faction == Faction.NONE && hazard.ThreatLevel == ThreatLevel.NONE && hazard.Rarity == Rarity.NONE)
                        validHazards.Add(hazard);
            }

            // If no hazards were found that match the sorting data, only return a default hazard.
            if (validHazards.Count == 0)
                validHazards.Add(_.hazards[0]);

            List<Hazard> selectedHazards = new List<Hazard>();
            for (int i = 0; i < sortingData.NumberToGet; i++)
            {
                // Early out if there are no valid hazards.
                if (validHazards.Count == 0)
                    break;

                int index = Random.Range(0, validHazards.Count - 1);
                selectedHazards.Add(validHazards[index]);

                if (sortingData.IsEachUnique)
                    validHazards.RemoveAt(index);
            }

            return selectedHazards;
        }

        /// <summary>
        /// Returns default hazard. (Index 0) It still needs to be instantiated.
        /// </summary>
        /// <returns>Default hazard.</returns>
        public static Hazard GetHazard() { return _.hazards[0]; }
    }
}