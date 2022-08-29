using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    /// <summary>
    /// This gameobject waits in a queue if possible when it dies.
    /// </summary>
    public class IWaitInQueue : MonoBehaviour
    {
        public virtual void Die() 
        { 
            Debug.Log($"Destroying {gameObject.name}");

            Hazard hazard = GetComponent<Hazard>();
            if (hazard)
            {
                hazard.WaitInQueue();
                return;
            }

            Destroy(gameObject);
        }
    }
}