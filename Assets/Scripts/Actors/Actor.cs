using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Actor : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb;

        protected virtual void Start() { rb = GetComponent<Rigidbody2D>(); }
    }
}