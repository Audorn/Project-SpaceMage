using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class SecondaryModifiersSelector : MissionContentSelectorCog
    {
        private List<SecondaryModifier> selectedSecondaryModifiers = new List<SecondaryModifier>();
        public List<SecondaryModifier> SelectedSecondaryModifiers { get { return selectedSecondaryModifiers; } }

        public void Execute()
        {
            int quantity = calculateQuantityToSelect(_possibleContent.PercentSecondaryModifiersTable, _possibleContent.QuantitySecondaryModifiersTable);

            var guaranteed = _possibleContent.GuaranteedSecondaryModifiers.ConvertAll(value => (int)value);
            var likely = _possibleContent.LikelySecondaryModifiers.ConvertAll(value => (int)value);
            var blocked = _possibleContent.BlockedSecondaryModifiers.ConvertAll(value => (int)value);
            var all = ((SecondaryModifier[])Enum.GetValues(typeof(PrimaryModifier))).ToList().ConvertAll(value => (int)value);

            selectedSecondaryModifiers = selectOptionsFromEnumAsInts(quantity, guaranteed, likely, blocked, all).ConvertAll(value => (SecondaryModifier)value);
        }
    }
}