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
        [SerializeField] private string id;
        [SerializeField] private HardpointRating baseHardpointRating;
        [SerializeField] private bool baseIsExternal = false;
        [SerializeField] private bool isDestroyed = false;
        [SerializeField] private bool baseIsTurret = false;
        [SerializeField] private float baseTurretConeRadius;
        [SerializeField] private float baseSpread;
        [SerializeField] private float baseRateOfFire;
        [SerializeField] private int baseModuleSlotCount;
        [SerializeField] private int baseSpellSlotCount;
        [SerializeField] private float baseSpellPower;
        [SerializeField] private float baseAmmoCountModifier;

        [SerializeField] private List<Module> modules = new List<Module>();
        [SerializeField] private List<Spell> spells = new List<Spell>();

        private HardpointRating hardpointRating;
        private bool isExternal;
        private bool isTurret;
        private float turretConeRadius;
        private float spread;
        private float rateOfFire;
        private int moduleSlotCount;
        private int spellSlotCount;
        private float spellPower;
        private float ammoCountModifier;

        public string Id => id;
        public string Name => (isDestroyed) ? 
            "Destroyed " + hardpointRating.ToString() + ((isExternal) ? " External Hardpoint" : " Internal Hardpoint") : 
            hardpointRating.ToString() + ((isExternal) ? " External Hardpoint" : " Internal Hardpoint");
        public HardpointRating BaseHardpointRating => baseHardpointRating;
        public bool BaseIsExternal => baseIsExternal;
        public bool BaseIsTurret => baseIsTurret;
        public float BaseTurretConeRadius => baseTurretConeRadius;
        public float BaseSpread => baseSpread;
        public float BaseRateOfFire => baseRateOfFire;
        public int BaseModuleSlotCount => baseModuleSlotCount;
        public int BaseSpellSlotCount => baseSpellSlotCount;
        public float BaseSpellPower => baseSpellPower;
        public float BaseAmmoCountModifier => baseAmmoCountModifier;

        public List<Module> Modules => modules;
        public List<Spell> Spells => spells;
        public int NumberOfModules => modules.Count;
        public int NumberOfSpells => spells.Count;

        public HardpointRating HardpointRating => hardpointRating;
        public bool IsDestroyed => isDestroyed;
        public bool IsExternal => isExternal;
        public bool IsTurret => isTurret;
        public float TurretConeRadius => turretConeRadius;
        public float Spread => spread;
        public float RateOfFire => rateOfFire;
        public int ModuleSlotCount => moduleSlotCount;
        public int SpellSlotCount => spellSlotCount;
        public float SpellPower => spellPower;
        public float AmmoCountModifier => ammoCountModifier;

        public void AddModule(Module module)
        {
            int numberOfModules = NumberOfModules;
            for (int i = 0; i < numberOfModules; i++)
            {
                if (modules[i] is EmptyModule)
                    modules[i] = module;
            }
        }
        public void RemoveModule(Module module)
        {

        }
        public void AddSpell(Spell spell)
        {
            int numberOfSpells = NumberOfSpells;
            for (int i = 0; i < numberOfSpells; i++)
            {
                if (spells[i] is EmptySpell)
                    spells[i] = spell;
            }
        }
        public void RemoveSpell(Spell spell)
        {

        }
    }
}