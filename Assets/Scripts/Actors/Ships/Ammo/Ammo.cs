using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    /// <summary>
    /// The relevant modifiers that a particular projectile may have on the firing behavior of gear.
    /// Contains a reference to any projectile that will be fired.
    /// </summary>
    public class Ammo : MonoBehaviour
    {
        [SerializeField] private string prefabId;
        [SerializeField] private Projectile projectile;
        [SerializeField] private bool instantiatesProjectile;

        public string PrefabId => prefabId;
        public Projectile Projectile => projectile;
        public bool InstantiatesProjectile => instantiatesProjectile;
    }
}