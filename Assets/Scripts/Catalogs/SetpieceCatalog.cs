using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.LevelGeneration;

namespace SpaceMage.Catalogs
{
    public class SetpieceCatalog : MonoBehaviour
    {
        private static SetpieceCatalog _;
        public static SetpieceCatalog Singleton { get { return _; } }
        private void Awake() { _ = this; }

        [SerializeField] private List<Setpiece> setpieces = new List<Setpiece>();
        public List<Setpiece> Setpieces { get { return setpieces; } }

        // =================================================
        // ==================== SORTING ====================
        // =================================================
        /// <summary>
        /// Returns a list of setpieces selected based on the filters.
        /// They still need to be instantiated.
        /// </summary>
        /// <param name="spawnSettings">Faction, ThreatLevel, Rarity, NumberToGet, and IsEachUnique.</param>
        /// <returns>A list of setpieces.</returns>
        public static List<Setpiece> GetSetpieces(SpawnSettings spawnSettings)
        {
            List<Setpiece> validSetpieces = new List<Setpiece>();
            foreach (Setpiece setpiece in _.setpieces)
            {
                if (setpiece.FilterData.Faction != Faction.ANY && setpiece.FilterData.Faction == spawnSettings.FilterData.Faction ||
                    setpiece.FilterData.ThreatLevel != ThreatLevel.ANY && setpiece.FilterData.ThreatLevel == spawnSettings.FilterData.ThreatLevel ||
                    setpiece.FilterData.Rarity != Rarity.ANY && setpiece.FilterData.Rarity == spawnSettings.FilterData.Rarity ||
                    setpiece.FilterData.Faction == Faction.ANY && setpiece.FilterData.ThreatLevel == ThreatLevel.ANY && setpiece.FilterData.Rarity == Rarity.ANY)
                    validSetpieces.Add(setpiece);
            }

            // If no hazards were found that match the sorting data, only return a default hazard.
            if (validSetpieces.Count == 0)
                validSetpieces.Add(_.setpieces[0]);

            List<Setpiece> selectedSetpieces = new List<Setpiece>();
            for (int i = 0; i < spawnSettings.NumberToGet; i++)
            {
                // Early out if there are no valid hazards.
                if (validSetpieces.Count == 0)
                    break;

                int index = Random.Range(0, validSetpieces.Count - 1);
                selectedSetpieces.Add(validSetpieces[index]);

                if (spawnSettings.IsEachUnique)
                    validSetpieces.RemoveAt(index);
            }

            return selectedSetpieces;
        }

        /// <summary>
        /// Returns default hazard. (Index 0) It still needs to be instantiated.
        /// </summary>
        /// <returns>Default hazard.</returns>
        public static Setpiece GetSetpiece() { return _.setpieces[0]; }
    }
}