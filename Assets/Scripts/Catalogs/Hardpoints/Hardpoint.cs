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
        [SerializeField] private string uniqueName;
        [SerializeField] private string description;
        [SerializeField] private HardpointRating baseHardpointRating;
        [SerializeField] private bool baseIsExternal = false;
        [SerializeField] private bool isDestroyed = false;
        [SerializeField] private bool baseIsTurret = false;
        [SerializeField] private float baseTurretConeRadius;
        [SerializeField] private float baseStability;
        [SerializeField] private int baseModuleSlotCount;
        [SerializeField] private int baseSpellSlotCount;
        [SerializeField] private float baseEnchantmentStrength;
        [SerializeField] private float baseAmmoCountModifier;
        [SerializeField] private Gear gear;
        [SerializeField] private bool equipmentOnly = false;
        [SerializeField] private bool spellGearOnly = false;

        [SerializeField] private List<Module> modules = new List<Module>();
        [SerializeField] private List<Spell> spells = new List<Spell>();

        private HardpointRating hardpointRating;
        private bool isExternal;
        private bool isTurret;
        private float turretConeRadius;
        private float stability;
        private int moduleSlotCount;
        private int spellSlotCount;
        private float enchantmentStrength;
        private float ammoCountModifier;
        private int currentAmmo;

        public string Id => id;
        public string Type => StateOfRepair(true) + Location(true) + "Hardpoint";
        public string UniqueName => uniqueName;
        public string Description => description;
        public HardpointRating BaseHardpointRating => baseHardpointRating;
        public bool BaseIsExternal => baseIsExternal;
        public bool BaseIsTurret => baseIsTurret;
        public float BaseTurretConeRadius => baseTurretConeRadius;
        public float BaseStability => baseStability;
        public int BaseModuleSlotCount => baseModuleSlotCount;
        public int BaseSpellSlotCount => baseSpellSlotCount;
        public float BaseEnchantmentStrength => baseEnchantmentStrength;
        public float BaseAmmoCountModifier => baseAmmoCountModifier;

        public Gear Gear => gear;
        public bool EquipmentOnly => equipmentOnly;
        public bool SpellGearOnly => spellGearOnly;
        public List<Module> Modules => modules;
        public List<Spell> Spells => spells;
        public int NumberOfModules => modules.Count;
        public int NumberOfSpells => spells.Count;

        public HardpointRating HardpointRating => hardpointRating;
        public string Rating => hardpointRating.ToString();
        public bool IsDestroyed => isDestroyed;
        public string StateOfRepair(bool includeTrailingSpace) => (isDestroyed) ? "Destroyed" + ((includeTrailingSpace) ? " " : "") : "";
        public bool IsExternal => isExternal;
        public string Location(bool includeTrailingSpace) => ((isExternal) ? "External" : "Internal") + ((includeTrailingSpace) ? " " : "");
        public bool IsTurret => isTurret;
        public float TurretConeRadius => turretConeRadius;
        public float Stability => stability;
        public int ModuleSlotCount => moduleSlotCount;
        public int SpellSlotCount => spellSlotCount;
        public float EnchantmentStrength => enchantmentStrength;
        public float AmmoCountModifier => ammoCountModifier;

        public int MaxAmmo => (int)(gear.NumberOfRounds * ammoCountModifier);
        public bool IsOutOfAmmo => currentAmmo <= 0;
        public void RefillAmmo() => currentAmmo = ((gear) ? MaxAmmo : 0);


        private bool canActivate(Transform target)
        {
            if (IsOutOfAmmo)
                return false;

            float distance = Vector2.Distance(transform.position, target.position);
            if (gear.OnlyFireWithinRange && distance > gear.Ammo.Range)
                return false;

            // TODO: Only fire within turret radius.

            return true;
        }
        public void ActivateGear(Transform target)
        {
            if (!canActivate(target))
                return;

            gear.Activate(target);
            currentAmmo--;
        }

        public bool InstallGear(Gear gear)
        {
            if (!gear.canMountTo(this))
                return false;

            // TODO: Uninstall existing gear if necessary.
            RemoveGear(0);

            this.gear = gear;
            gear.RegisterHardpoint(this);
            currentAmmo = MaxAmmo;
            return true;
        }
        public void RemoveGear(int index, bool destroyMountable = false)
        {
            if (gear is EmptyGear)
                return;

            // TODO: Swap gear reference with inventory location gear reference.
        }

        public bool InstallModule(Module module)
        {
            int numberOfModules = NumberOfModules;
            for (int i = 0; i < numberOfModules; i++)
            {
                if (modules[i] is EmptyModule)
                {
                    modules[i] = module;
                    return true;
                }
            }

            return false;
        }
        public void RemoveModule(Module module, bool destroyModule = false)
        {

        }
        public bool Enchant(Spell spell)
        {
            int numberOfSpells = NumberOfSpells;
            for (int i = 0; i < numberOfSpells; i++)
            {
                if (spells[i] is EmptySpell)
                {
                    spells[i] = spell;
                    return true;
                }
            }

            return false;
        }
        public void Dispel(Spell spell, bool destroySpell = false)
        {

        }
    }
}