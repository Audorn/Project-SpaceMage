using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    public class AmmoManager : MonoBehaviour
    {
        // Singleton.
        private static AmmoManager _;
        public static AmmoManager Singleton => _;
        private void Awake() => _ = this;


        [SerializeField] private AmmoPool pool;                 // Editor configurable.

        public static Ammo Instantiate(Vector2 parentVelocity, Ammo ammoPrefab, Transform transform, Transform target, HandleMomentum momentum = HandleMomentum.OVERRIDE)
        {
            return Instantiate(parentVelocity, ammoPrefab, transform.position, transform.rotation, target, momentum);
        }

        public static Ammo Instantiate(Vector2 parentVelocity, Ammo ammoPrefab, Vector3 position, Quaternion quaternion, Transform target, HandleMomentum momentum)
        {
            Ammo ammoInPool = _.pool.GetWaitingAmmoObjectByPrefabId(ammoPrefab.PrefabId);

            // None found - instantiate one.
            if (!ammoInPool)
            {
                Ammo ammo = GameObject.Instantiate(ammoPrefab, position, quaternion);
                ammo.Fire(target, parentVelocity, momentum);

                _.pool.Add(ammo);

                return ammo;
            }

            ammoInPool.ActivateInPool(position, quaternion, target, parentVelocity, momentum);
            return ammoInPool;
        }
    }
}