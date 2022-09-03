using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class PrimaryModifiersSelector : MissionContentSelectorCog
    {
        private List<PrimaryModifier> selectedPrimaryModifiers = new List<PrimaryModifier>();
        public List<PrimaryModifier> SelectedPrimaryModifiers { get { return selectedPrimaryModifiers; } }

        public void Execute()
        {
            int quantity = calculateQuantityToSelect(_possibleContent.PercentPrimaryModifiersTable, _possibleContent.QuantityPrimaryModifiersTable);

            var guaranteed = _possibleContent.GuaranteedPrimaryModifiers.ConvertAll(value => (int)value);
            var likely = _possibleContent.LikelyPrimaryModifiers.ConvertAll(value => (int)value);
            var blocked = _possibleContent.BlockedPrimaryModifiers.ConvertAll(value => (int)value);
            var all = ((PrimaryModifier[])Enum.GetValues(typeof(PrimaryModifier))).ToList().ConvertAll(value => (int)value);

            var selectPrimaryModifiers = selectOptionsFromEnumAsInts(quantity, guaranteed, likely, blocked, all).ConvertAll(value => (PrimaryModifier)value);
        }
    }
}