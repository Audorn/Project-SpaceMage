using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    /// <summary>
    /// This gameobject waits in a queue if possible when it dies.
    /// </summary>
    public class WaitsInQueue : MonoBehaviour
    {
        [SerializeField] protected bool isWaitingInQueue = true;

        public bool IsWaitingInQueue { get { return isWaitingInQueue; } }
        public virtual void ActivateInQueue()
        {
            //GetComponent<Collider2D>().isTrigger = false;
            //GetComponent<SpriteRenderer>().enabled = true;
            isWaitingInQueue = false; 
        }
        public virtual void WaitInQueue() { isWaitingInQueue = true; }

        private bool isStillSpawningChildren = true;
        public void RegisterFinishedSpawningChildren() { isStillSpawningChildren = false; }
        public virtual void Die() 
        { 
            Debug.Log($"Destroying {gameObject.name}");

            SpawnsChildren spawnsChildren = GetComponent<SpawnsChildren>();
            if (spawnsChildren)
                spawnsChildren.SpawnChildren();

            Hazard hazard = GetComponent<Hazard>();
            if (hazard)
            {
                //GetComponent<Collider2D>().isTrigger = true;
                //GetComponent<SpriteRenderer>().enabled = false;
                return;
            }

            Destroy(gameObject);
        }

        private IEnumerator die(System.Type type)
        {
            while (isStillSpawningChildren)
            {
                yield return new WaitForFixedUpdate();
            }

            Debug.LogError("Sleeping");
            if (type == typeof(Hazard))
                GetComponent<Hazard>().WaitInQueue();
        }
    }
}