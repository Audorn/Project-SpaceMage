using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    /// <summary>
    /// A list of projectiles that can be reused.
    /// </summary>
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private List<Projectile> projectiles = new List<Projectile>();     // Editor viewable.

        public List<Projectile> Projectiles => projectiles;

        public int Count => projectiles.Count;
        public void Add(Projectile projectile) => projectiles.Add(projectile);

        /// <summary>
        /// Find a projectile to activate.
        /// </summary>
        /// <param name="prefabId">The prefab Id to look for.</param>
        /// <returns>A waiting projectile, or null if none found.</returns>
        public Projectile GetWaitingProjectileByPrefabId(string prefabId)
        {
            int numberOfProjectiles = projectiles.Count;
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                if (projectiles[i].PrefabId == prefabId && projectiles[i].IsWaitingInPool)
                    return projectiles[i];
            }

            return null;
        }
    }
}