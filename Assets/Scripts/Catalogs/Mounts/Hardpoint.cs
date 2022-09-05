using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    /// <summary>
    /// A location on the ship that is designed to carry a load.
    /// </summary>
    public class Hardpoint : MonoBehaviour
    {
        [SerializeField] private bool baseIsExternal = false;
        [SerializeField] private HardpointRating baseHardpointRating;
        [SerializeField] private int baseModuleSlotCount;
        [SerializeField] private int baseSpellSlotCount;
        [SerializeField] private float baseAmmoCountModifier = 1;

        [SerializeField] private List<ModSlot> moduleSlots = new List<ModSlot>();
        [SerializeField] private List<SpellSlot> spellSlots = new List<SpellSlot>();

        private bool isExternal;
        private HardpointRating hardpointRating;
        private int moduleSlotCount;
        private int spellSlotCount;
        private float ammoCountModifier;

        public bool BaseIsExternal => baseIsExternal;
        public HardpointRating BaseHardpointRating => baseHardpointRating;
        public int BaseModuleSlotCount => baseModuleSlotCount;
        public int BaseSpellSlotCount => baseSpellSlotCount;
        public float BaseAmmoCountModifier => baseAmmoCountModifier;

        public List<ModSlot> ModuleSlots => moduleSlots;
        public List<SpellSlot> SpellSlots => spellSlots;
        public int NumberOfModuleSlots => moduleSlots.Count;
        public int NumberOfSpellSlots => spellSlots.Count;

        public bool IsExternal => isExternal;
        public HardpointRating HardpointRating => hardpointRating;
        public int ModuleSlotCount => moduleSlotCount;
        public int SpellSlotCount => spellSlotCount;
        public float AmmoCountModifier => ammoCountModifier;

        public void AddModule(Module module)
        {
            int numberOfModules = NumberOfModuleSlots;
            for (int i = 0; i < numberOfModules; i++)
            {
                if (moduleSlots[i].IsEmpty)
                    moduleSlots[i].InstallModule(module);
            }
        }
        public void RemoveModule(Module module)
        {

        }
        public void AddSpell(Spell spell)
        {
            int numberOfSpells = NumberOfSpellSlots;
            for (int i = 0; i < numberOfSpells; i++)
            {
                if (spellSlots[i].IsEmpty)
                    spellSlots[i].InstallSpell(spell);
            }
        }
        public void RemoveSpell(Spell spell)
        {

        }
    }
}