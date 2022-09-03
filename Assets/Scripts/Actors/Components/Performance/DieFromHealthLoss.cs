using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    /// <summary>
    /// This gameobject dies.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class DieFromHealthLoss : MonoBehaviour
    {
        private Actor actor;
        private Health health;

        public virtual void Die()
        {
            Debug.Log($"Destroying {gameObject.name}");

            if (actor)
            {
                actor.WaitInQueue();
                ActorSpawner spawner = GetComponent<ActorSpawner>();

                if (spawner)
                    ActorSpawner.SpawnActors(spawner.ActorSpawnSettings, SpawnEvent.DEATH);

                return;
            }

            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if (health.Current <= 0)
                Die();
        }

        private void Awake() 
        { 
            actor = GetComponent<Actor>();
            health = GetComponent<Health>();
        }
    }
}