using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    /// <summary>
    /// This gameobject dies. If it is an actor, it will wait in its ActorPool instead.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class DieFromHealthLoss : MonoBehaviour
    {
        private Actor actor;
        private Health health;

        [SerializeField] private int diesAt = 0;
        public int DiesAt { get { return diesAt; } }

        public virtual void Die()
        {
            if (actor)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                Vector2 parentVelocity = new Vector2(rb.velocity.x, rb.velocity.y);

                actor.WaitInPool();
                ActorSpawner spawner = GetComponent<ActorSpawner>();
                if (spawner)
                    ActorSpawner.SpawnActors(parentVelocity, spawner.ActorSpawnSettings, SpawnEvent.DEATH, spawner.PoolToSpawnIn);

                return;
            }

            Debug.Log($"Destroying {gameObject.name}");
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if (health.Current <= diesAt)
                Die();
        }

        private void Awake() 
        { 
            actor = GetComponent<Actor>();
            health = GetComponent<Health>();
        }
    }
}