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
        [SerializeField] private GameObject gameObjectThatDies;
        public virtual void Die() { Debug.Log($"Destroying {gameObjectThatDies.name}"); Destroy(gameObjectThatDies); }

        // If there is no pilot, this gameobject will be the one that dies.
        protected void Start()
        {
            IDieTarget dieTarget = GetComponentInParent<IDieTarget>();
            if (dieTarget)
                gameObjectThatDies = dieTarget.gameObject;
            else
                gameObjectThatDies = gameObject;
        }
    }
}