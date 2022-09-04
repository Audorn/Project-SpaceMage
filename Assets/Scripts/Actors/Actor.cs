using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))] // TODO: Get the health to fill itself when activating from queue.
    public class Actor : MonoBehaviour
    {
        protected Rigidbody2D rb;

        [SerializeField] protected CatalogFilterData filterData;
        [SerializeField] protected SpawnPool spawnPool;
        [SerializeField] protected bool isWaitingInPool = true;

        public UnityEvent ActivateInPoolEvent = new UnityEvent();
        public UnityEvent WaitInPoolEvent = new UnityEvent();

        public CatalogFilterData FilterData => filterData;
        public SpawnPool SpawnPool => spawnPool;
        public bool IsWaitingInPool => isWaitingInPool;

        public void SetSpawnPool(SpawnPool spawnPool) => this.spawnPool = spawnPool;

        public Actor ActivateInPool(Vector3 position, Quaternion quaternion)
        {
            GetComponent<Health>().Fill();
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            transform.position = position;
            transform.rotation = quaternion;
            gameObject.SetActive(true);
            isWaitingInPool = false;
            ActivateInPoolEvent.Invoke();

            return this;
        }

        public void WaitInPool()
        {
            isWaitingInPool = true;
            WaitInPoolEvent.Invoke();
            gameObject.SetActive(false);
        }

        protected virtual void Start() { rb = GetComponent<Rigidbody2D>(); }
    }
}