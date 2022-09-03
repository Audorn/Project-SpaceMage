using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class FactionSelector : MissionContentSelectorCog
    {
        List<Faction> selectedFactions = new List<Faction>();
        public List<Faction> SelectedFactions { get { return selectedFactions; } }

        public void Execute()
        {
            int quantity = calculateQuantityToSelect(_possibleContent.PercentFactionsTable, _possibleContent.QuantityFactionsTable);

            var guaranteed = _possibleContent.GuaranteedFactions.ConvertAll(value => (int)value);
            var likely = _possibleContent.LikelyFactions.ConvertAll(value => (int)value);
            var blocked = _possibleContent.BlockedFactions.ConvertAll(value => (int)value);
            var all = ((Faction[])Enum.GetValues(typeof(Faction))).ToList().ConvertAll(value => (int)value);

            selectedFactions = selectOptionsFromEnumAsInts(quantity, guaranteed, likely, blocked, all).ConvertAll(value => (Faction)value);
        }
    }
}