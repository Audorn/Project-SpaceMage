using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    /// <summary>
    /// This gameobject can die.
    /// </summary>
    public class IDie : MonoBehaviour
    {
        public virtual void Die() { Debug.Log($"Destroying {gameObject.name}"); Destroy(gameObject); }
    }
}