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
        [SerializeField] private HardpointRating hardpointRating;
        [SerializeField] private bool isExternalOnly = false;
        [SerializeField] private float accuracy;
        [SerializeField] private bool onlyFireWithinRange = false;
        [SerializeField] private float rateOfFire;
        [SerializeField] private Ammo ammo;
        [SerializeField] private float numberOfRounds;
        [SerializeField] private bool instantiatesRound = true;
        [SerializeField] private bool regeneratesAmmo;
        [SerializeField] private float rateOfAmmoRegen;
        [SerializeField] private bool isPassive = false;

        public void RegisterHardpoint(Hardpoint hardpoint) => this.hardpoint = hardpoint;
        public string Id => id;
        public string UniqueName => uniqueName;
        public string Description => description;
        public GearType GearType => gearType;
        public HardpointRating HardpointRating => hardpointRating;
        public bool IsExternalOnly => isExternalOnly;
        public float Accuracy => accuracy;
        public bool OnlyFireWithinRange => onlyFireWithinRange;
        public float RateOfFire => rateOfFire;
        public Ammo Ammo => ammo;
        public float NumberOfRounds => numberOfRounds;
        public bool RegeneratesAmmo => regeneratesAmmo;
        public float RateOfAmmoRegen => rateOfAmmoRegen;
        public bool IsPassive => isPassive;

        public virtual bool canMountTo(Hardpoint hardpoint)
        {
            if (hardpoint.IsDestroyed)
                return false;

            if (hardpoint.HardpointRating != hardpointRating)
                return false;

            if (isExternalOnly && !hardpoint.IsExternal)
                return false;

            if ((gearType != GearType.EQUIPMENT && hardpoint.EquipmentOnly) || (gearType != GearType.SPELLGEAR && hardpoint.SpellGearOnly))
                return false;

            return true;
        }

        public virtual void Activate(Transform target)
        {
            Debug.LogWarning($"Activating {uniqueName}");

            if (instantiatesRound)
                fireRound(target);
            else
                activateEffect(target);
        }

        private void fireRound(Transform target)
        {
            Debug.LogWarning($"Fired round.");
            ammo.Fire(target, GetComponentInParent<Rigidbody2D>().velocity, ammo.HandleMomentum);
        }

        private void activateEffect(Transform target)
        {
            Debug.LogWarning($"Activated effect.");
        }
    }
}