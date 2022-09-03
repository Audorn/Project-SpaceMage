using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class ObjectivesSelector : MissionContentSelectorCog
    {
        private List<Objective> selectedObjectives = new List<Objective>();
        public List<Objective> SelectedObjectives { get { return selectedObjectives; } }

        public void Execute()
        {
            int quantity = calculateQuantityToSelect(_possibleContent.PercentObjectivesTable, _possibleContent.QuantityObjectivesTable);

            var guaranteed = _possibleContent.GuaranteedObjectives.ConvertAll(value => (int)value);
            var likely = _possibleContent.LikelyObjectives.ConvertAll(value => (int)value);
            var blocked = _possibleContent.BlockedObjectives.ConvertAll(value => (int)value);
            var all = ((Objective[])Enum.GetValues(typeof(Objective))).ToList().ConvertAll(value => (int)value);

            selectedObjectives = selectOptionsFromEnumAsInts(quantity, guaranteed, likely, blocked, all).ConvertAll(value => (Objective)value);
        }
    }
}