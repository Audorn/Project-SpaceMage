using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    public class Gear : MonoBehaviour 
    {
        private Hardpoint hardpoint;

        [SerializeField] private string id;
        [SerializeField] private string uniqueName;
        [SerializeField] private string description;
        [SerializeField] private GearType gearType;
        [SerializeField] private HardpointRating baseHardpointRating;
        [SerializeField] private bool baseIsExternalOnly = false;
        [SerializeField] private float baseAccuracy;
        [SerializeField] private bool baseOnlyFireWithinRange = false;
        [SerializeField] private float baseRateOfFire;
        [SerializeField] private float baseMaxRounds;
        [SerializeField] private bool baseRequiresAmmo = true;
        [SerializeField] private List<Ammo> validAmmos = new List<Ammo>();
        [SerializeField] private bool baseRegeneratesAmmo;
        [SerializeField] private float baseRateOfAmmoRegen;
        [SerializeField] private bool baseIsPassive = false;

        public void RegisterHardpoint(Hardpoint hardpoint) => this.hardpoint = hardpoint;
        public string Id => id;
        public string UniqueName => uniqueName;
        public string Description => description;
        public GearType GearType => gearType;
        public HardpointRating BaseHardpointRating => baseHardpointRating;
        public bool BaseIsExternalOnly => baseIsExternalOnly;
        public float BaseAccuracy => baseAccuracy;
        public bool BaseOnlyFireWithinRange => baseOnlyFireWithinRange;
        public float BaseRateOfFire => baseRateOfFire;
        public float BaseMaxRounds => baseMaxRounds;
        public bool BaseRequiresAmmo => baseRequiresAmmo;
        public bool BaseRegeneratesAmmo => baseRegeneratesAmmo;
        public float BaseRateOfAmmoRegen => baseRateOfAmmoRegen;
        public bool BaseIsPassive => baseIsPassive;

        private HardpointRating hardpointRating;
        private bool isExternalOnly;
        private float accuracy;
        private bool onlyFireWithinRange;
        private float rateOfFire;
        private float maxRounds;
        private bool requiresAmmo;
        private bool regeneratesAmmo;
        private float rateOfAmmoRegen;
        private bool isPassive;

        public HardpointRating HardpointRating => hardpointRating;
        public bool IsExternalOnly => isExternalOnly;
        public float Accuracy => accuracy;
        public bool OnlyFireWithinRange => onlyFireWithinRange;
        public float MaxRounds => maxRounds;
        public bool RequiresAmmo => requiresAmmo;
        public List<Ammo> ValidAmmos => validAmmos;
        public bool RegeneratesAmmo => regeneratesAmmo;
        public float RateOfAmmoRegen => rateOfAmmoRegen;
        public bool IsPassive => isPassive;

        public virtual bool CanMountTo(Hardpoint hardpoint)
        {
            // Hardpoint is destroyed.
            if (hardpoint.IsDestroyed)
                return false;

            // Hardpoint is for a different rating.
            if (hardpoint.HardpointRating != baseHardpointRating)
                return false;

            // Hardpoint is internal and this gear is external only.
            if (baseIsExternalOnly && !hardpoint.IsExternal)
                return false;

            // Hardpoint does not accept this type of gear.
            if ((gearType != GearType.EQUIPMENT && hardpoint.EquipmentOnly) || (gearType != GearType.SPELLGEAR && hardpoint.SpellGearOnly))
                return false;

            // Hardpoint passed all checks.
            return true;
        }

        public bool IsAmmoValid(Ammo ammo)
        {
            if (validAmmos.Find(validAmmo => ammo.PrefabId == validAmmo.PrefabId) != null)
                return true;

            return false;
        }

        public virtual void Activate(Transform target, List<Module> modules, List<Spell> spells, Ammo ammo)
        {
            Debug.LogWarning($"Activating {uniqueName}");

            if (ammo.InstantiatesProjectile)
                fireRound(target, modules, spells, ammo);
            else
                activateEffect(target, modules, spells, ammo);
        }

        private void fireRound(Transform target, List<Module> modules, List<Spell> spells, Ammo ammo)
        {
            Debug.LogWarning($"Fired round.");
        }

        private void activateEffect(Transform target, List<Module> modules, List<Spell> spells, Ammo ammo)
        {
            Debug.LogWarning($"Activated effect.");
        }
    }
}