using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))] // TODO: Get the health to fill itself when activating from queue.
    public class Actor : MonoBehaviour
    {
        protected Rigidbody2D rb;
        [SerializeField] protected CatalogFilterData filterData;
        [SerializeField] protected SpawnPool spawnPool;
        [SerializeField] protected bool isWaitingInQueue = true;

        public CatalogFilterData FilterData { get { return filterData; } }
        public SpawnPool SpawnPool { get { return spawnPool; } }
        public bool IsWaitingInQueue { get { return isWaitingInQueue; } }

        public void SetSpawnPool(SpawnPool spawnPool) { this.spawnPool = spawnPool; }

        public Actor ActivateInQueue(Vector3 position, Quaternion quaternion)
        {
            GetComponent<Health>().Fill();
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            transform.position = position;
            transform.rotation = quaternion;
            gameObject.SetActive(true);
            isWaitingInQueue = false;

            return this;
        }

        public void WaitInQueue()
        {
            gameObject.SetActive(false);
            isWaitingInQueue = true;
        }

        protected virtual void Start() { rb = GetComponent<Rigidbody2D>(); }
    }
}