using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class HazardsSelector : MissionContentSelectorCog
    {
        private List<Faction> selectedHazardFactions = new List<Faction>();
        public List<Faction> SelectedHazardFactions { get { return selectedHazardFactions; } }

        public void Execute()
        {
            int quantity = calculateQuantityToSelect(_possibleContent.PercentHazardsTable, _possibleContent.QuantityHazardsTable);

            var guaranteed = _possibleContent.GuaranteedHazardFactions.ConvertAll(value => (int)value);
            var likely = _possibleContent.LikelyHazardFactions.ConvertAll(value => (int)value);
            var blocked = _possibleContent.BlockedHazardFactions.ConvertAll(value => (int)value);
            var all = ((Faction[])Enum.GetValues(typeof(Faction))).ToList().ConvertAll(value => (int)value);

            selectedHazardFactions = selectOptionsFromEnumAsInts(quantity, guaranteed, likely, blocked, all).ConvertAll(value => (Faction)value);
        }
    }
}
