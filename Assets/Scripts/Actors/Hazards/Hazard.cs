using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(IDealImpactDamage))]
    [RequireComponent(typeof(WaitsInQueue))]
    public class Hazard : Actor
    {
        private WaitsInQueue waitsInQueue;
        [SerializeField] private FilterData filterData;

        public FilterData FilterData { get { return filterData; } }
        public bool IsWaitingInQueue { get { return waitsInQueue.IsWaitingInQueue; } }

        public Hazard ActivateInQueue(Vector3 position, Quaternion quaternion)
        {
            GetComponent<Health>().Fill();
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            transform.position = position;
            transform.rotation = quaternion;
            gameObject.SetActive(true);
            waitsInQueue.ActivateInQueue();

            return this;
        }

        public void WaitInQueue()
        {
            gameObject.SetActive(false);
            waitsInQueue.WaitInQueue();
        }

        private void Awake()
        {
            waitsInQueue = GetComponent<WaitsInQueue>();
        }
    }
}