using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Entities;
using SpaceMage.LevelGeneration;

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
        /// <param name="spawnSettings">Faction, ThreatLevel, Rarity, NumberToGet, and IsEachUnique.</param>
        /// <returns>A list of hazards.</returns>
        public static List<Hazard> GetHazards(SpawnSettings spawnSettings)
        {
            List<Hazard> validHazards = new List<Hazard>();
            if (spawnSettings.FilterData.PrefabId != "") // Specific uniqueId.
            {
                foreach (Hazard hazard in _.hazards)
                {
                    if (spawnSettings.FilterData.PrefabId == hazard.FilterData.PrefabId)
                    {
                        validHazards.Add(hazard);
                        break;
                    }
                }
            }
            else // Find by tags.
            {
                foreach (Hazard hazard in _.hazards)
                {
                    if (hazard.FilterData.Faction != Faction.ANY && hazard.FilterData.Faction == spawnSettings.FilterData.Faction ||
                        hazard.FilterData.ThreatLevel != ThreatLevel.ANY && hazard.FilterData.ThreatLevel == spawnSettings.FilterData.ThreatLevel ||
                        hazard.FilterData.Rarity != Rarity.ANY && hazard.FilterData.Rarity == spawnSettings.FilterData.Rarity ||
                        hazard.FilterData.Faction == Faction.ANY && hazard.FilterData.ThreatLevel == ThreatLevel.ANY && hazard.FilterData.Rarity == Rarity.ANY)
                            validHazards.Add(hazard);
                }
            }

            // If no hazards were found that match the sorting data, only return a default hazard.
            if (validHazards.Count == 0)
                validHazards.Add(_.hazards[0]);

            List<Hazard> selectedHazards = new List<Hazard>();
            for (int i = 0; i < spawnSettings.NumberToGet; i++)
            {
                // Early out if there are no valid hazards.
                if (validHazards.Count == 0)
                    break;

                int index = Random.Range(0, validHazards.Count);
                selectedHazards.Add(validHazards[index]);

                if (spawnSettings.IsEachUnique)
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