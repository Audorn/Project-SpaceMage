using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    public class SpellSlot
    {
        private Spell spell;

        public bool IsEmpty => spell == null || spell is EmptySpell;

        public void InstallSpell(Spell spell)
        {
            this.spell = spell;
            spell.Enchant();
        }
    }
}