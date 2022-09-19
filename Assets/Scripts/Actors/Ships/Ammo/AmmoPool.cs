using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    /// <summary>
    /// A list of ammo objects that can be reused.
    /// </summary>
    public class AmmoPool : MonoBehaviour
    {
        [SerializeField] private List<Ammo> ammoObjects = new List<Ammo>();     // Editor viewable.

        public List<Ammo> AmmoObjects => ammoObjects;

        public int Count => ammoObjects.Count;
        public void Add(Ammo ammoObject) => ammoObjects.Add(ammoObject);

        /// <summary>
        /// Find an ammo object to activate.
        /// </summary>
        /// <param name="prefabId">The prefab Id to look for.</param>
        /// <returns>A waiting ammo object, or null if none found.</returns>
        public Ammo GetWaitingAmmoObjectByPrefabId(string prefabId)
        {
            int numberOfAmmoObjects = ammoObjects.Count;
            for (int i = 0; i < numberOfAmmoObjects; i++)
            {
                if (ammoObjects[i].PrefabId == prefabId && ammoObjects[i].IsWaitingInPool)
                    return ammoObjects[i];
            }

            return null;
        }
    }
}