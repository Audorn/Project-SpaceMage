using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    public class ProjectileManager : MonoBehaviour
    {
        // Singleton.
        private static ProjectileManager _;
        public static ProjectileManager Singleton => _;
        private void Awake() => _ = this;


        [SerializeField] private ProjectilePool pool;                 // Editor configurable.

        public static Projectile Instantiate(Vector2 parentVelocity, Projectile prefab, Transform transform, Transform target, HandleMomentum momentum = HandleMomentum.OVERRIDE)
        {
            return Instantiate(parentVelocity, prefab, transform.position, transform.rotation, target, momentum);
        }

        public static Projectile Instantiate(Vector2 parentVelocity, Projectile prefab, Vector3 position, Quaternion quaternion, Transform target, HandleMomentum momentum)
        {
            Projectile projectileInPool = _.pool.GetWaitingProjectileByPrefabId(prefab.PrefabId);

            // None found - instantiate one.
            if (!projectileInPool)
            {
                Projectile projectile = GameObject.Instantiate(prefab, position, quaternion);
                //projectile.Fire(target, parentVelocity, momentum);

                _.pool.Add(projectile);

                return projectile;
            }

            projectileInPool.ActivateInPool(position, quaternion, target, parentVelocity, momentum);
            return projectileInPool;
        }
    }
}