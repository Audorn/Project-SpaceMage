using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class SetpiecesSelector : MissionContentSelectorCog
    {
        List<Faction> selectedSetpieceFactions = new List<Faction>();
        public List<Faction> SelectedSetpieceFactions { get { return selectedSetpieceFactions; } }

        public void Execute()
        {
            int quantity = calculateQuantityToSelect(_possibleContent.PercentSetpiecesTable, _possibleContent.QuantitySetpiecesTable);

            var guaranteed = _possibleContent.GuaranteedSetpieceFactions.ConvertAll(value => (int)value);
            var likely = _possibleContent.LikelySetpieceFactions.ConvertAll(value => (int)value);
            var blocked = _possibleContent.BlockedSetpieceFactions.ConvertAll(value => (int)value);
            var all = ((Faction[])Enum.GetValues(typeof(Faction))).ToList().ConvertAll(value => (int)value);

            selectedSetpieceFactions = selectOptionsFromEnumAsInts(quantity, guaranteed, likely, blocked, all).ConvertAll(value => (Faction)value);
        }
    }
}