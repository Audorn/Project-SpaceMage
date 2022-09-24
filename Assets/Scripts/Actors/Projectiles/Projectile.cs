using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceMage.Ships
{
    /// <summary>
    /// A single round fired from a weapon or spellgear. May be a bullet, missile, or spell projectile.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        protected Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;

        [SerializeField] private bool isWaitingInPool = true;     // Editor viewable.

        public UnityEvent ActivateInPoolEvent = new UnityEvent();
        public UnityEvent WaitInPoolEvent = new UnityEvent();

        [SerializeField] private Transform target;

        [SerializeField] private string prefabId;
        [SerializeField] private string uniqueName;
        [SerializeField] private string description;
        [SerializeField] private bool baseConsumesFuel = false;
        [SerializeField] private float baseFuel;
        [SerializeField] private bool baseIsHoming = false;
        [SerializeField] private float baseSpeed;
        [SerializeField] private float baseRange;
        [SerializeField] private HandleMomentum handleMomentum = HandleMomentum.OVERRIDE;

        private bool consumesFuel;
        private float fuel;
        private bool isHoming;
        private float speed;
        private float range;
        private Vector2 origin;

        public bool IsWaitingInPool => isWaitingInPool;
        public string PrefabId => prefabId;
        public string UniqueName => uniqueName;
        public string Description => description;
        public bool BaseConsumesFeul => baseConsumesFuel;
        public float BaseFuel => baseFuel;
        public bool BaseIsHoming => baseIsHoming;
        public float BaseSpeed => baseSpeed;
        public float BaseRange => baseRange;
        public HandleMomentum HandleMomentum => handleMomentum;

        public bool ConsumesFuel => consumesFuel;
        public float Fuel => fuel;
        public bool IsHoming => isHoming;
        public float Speed => speed;
        public float Range => range;

        private void FixedUpdate()
        {
            // TODO: decide on kinematic or simulated and move that way.
        }

        //public void Fire(Transform target, Vector2 parentVelocity, HandleMomentum momentum) 
        //{
        //    resetRigidBodyVelocities(parentVelocity, momentum);
        //    this.target = target;
        //    origin = transform.position;
        //}

        /// <summary>
        /// Moves ammo object to new position and rotation, resets rigidbody velocities, and wakes the ammo up, invoking the ActivateInPoolEvent.
        /// </summary>
        /// <param name="position">Where to activate.</param>
        /// <param name="quaternion">What rotation to activate with.</param>
        /// <param name="target">What target to head towards.</param>
        /// <param name="parentVelocity">Velocity that may influence behavior.</param>
        /// <param name="momentum">How to treat parent velocity.</param>
        /// <returns>Freshly activated ammo.</returns>
        public Projectile ActivateInPool(Vector3 position, Quaternion quaternion, Transform target, Vector2 parentVelocity, HandleMomentum momentum)
        {
            transform.position = position;
            transform.rotation = quaternion;
            //Fire(target, parentVelocity, momentum);

            gameObject.SetActive(true);
            ActivateInPoolEvent.Invoke(); // Invoke while GameObject is enabled.
            isWaitingInPool = false;

            return this;
        }

        /// <summary>
        /// Deactivates the ammo object, invoking the WaitInPoolEvent.
        /// </summary>
        public void WaitInPool()
        {
            WaitInPoolEvent.Invoke(); // Invoke while GameObject is enabled.
            gameObject.SetActive(false);
            isWaitingInPool = true;
        }

        protected virtual void resetRigidBodyVelocities(Vector2 parentVelocity, HandleMomentum momentum)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;

            if (momentum == HandleMomentum.RETAIN)
                rb.velocity = parentVelocity;
            else if (momentum == HandleMomentum.AVERAGE)
                rb.velocity = parentVelocity / 2;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}