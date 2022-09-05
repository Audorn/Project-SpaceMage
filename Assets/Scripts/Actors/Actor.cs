using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceMage.Actors
{
    /// <summary>
    /// An entity that can be interacted with via a rigidbody and waits in a pool when dead.
    /// </summary>
    public class Actor : MonoBehaviour
    {
        protected Rigidbody2D rb;                                   // Assigned in Awake().

        [SerializeField] protected CatalogFilterData filterData;    // Editor configurable.
        [SerializeField] protected SpawnPool spawnPool;             // Editor viewable.
        [SerializeField] protected bool isWaitingInPool = true;     // Editor viewable.

        public UnityEvent ActivateInPoolEvent = new UnityEvent();   
        public UnityEvent WaitInPoolEvent = new UnityEvent();

        public CatalogFilterData FilterData => filterData;
        public SpawnPool SpawnPool => spawnPool;
        public bool IsWaitingInPool => isWaitingInPool;

        public void SetSpawnPool(SpawnPool spawnPool) => this.spawnPool = spawnPool;

        /// <summary>
        /// Moves actor to new position and rotation, resets rigidbody velocities, and wakes the actor up, invoking the ActivateInPoolEvent.
        /// </summary>
        /// <param name="position">Where to activate.</param>
        /// <param name="quaternion">What rotation to activate with.</param>
        /// <returns>Freshly activated actor.</returns>
        public Actor ActivateInPool(Vector3 position, Quaternion quaternion)
        {
            resetRigidBodyVelocities();
            transform.position = position;
            transform.rotation = quaternion;

            gameObject.SetActive(true);
            ActivateInPoolEvent.Invoke(); // Invoke while GameObject is enabled.
            isWaitingInPool = false;

            return this;
        }

        /// <summary>
        /// Deactivates the actor, invoking the WaitInPoolEvent.
        /// </summary>
        public void WaitInPool()
        {
            WaitInPoolEvent.Invoke(); // Invoke while GameObject is enabled.
            gameObject.SetActive(false);
            isWaitingInPool = true;
        }

        protected virtual void resetRigidBodyVelocities()
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }

        protected virtual void Awake() => rb = GetComponent<Rigidbody2D>();
    }
}