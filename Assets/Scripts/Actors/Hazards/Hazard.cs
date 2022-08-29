using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(IDealImpactDamage))]
    public class Hazard : Actor
    {
        [SerializeField] private FilterData filterData;
        public FilterData FilterData { get { return filterData; } }

        public Hazard ActivateInQueue(Vector3 position, Quaternion quaternion)
        {
            GetComponent<Health>().Fill();
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            transform.position = position;
            transform.rotation = quaternion;
            gameObject.SetActive(true);
            base.ActivateInQueue();

            return this;
        }

        public override void WaitInQueue()
        {
            gameObject.SetActive(false);
            base.WaitInQueue();
        }
    }
}