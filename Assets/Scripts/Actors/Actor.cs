using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Actor : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected bool isWaitingInQueue = true;

        public bool IsWaitingInQueue { get { return isWaitingInQueue; } }
        public virtual void ActivateInQueue() { isWaitingInQueue = false; }
        public virtual void WaitInQueue() { isWaitingInQueue = true; }

        protected virtual void Start() { rb = GetComponent<Rigidbody2D>(); }
    }
}