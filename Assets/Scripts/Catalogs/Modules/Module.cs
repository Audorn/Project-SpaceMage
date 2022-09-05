using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    /// <summary>
    /// A module that can be activated and deactivated.
    /// </summary>
    [System.Serializable]
    public abstract class Module
    {
        [SerializeField] protected string moduleId;           // Editor configurable.
        [SerializeField] protected string name;               // Editor configurable.

        public abstract void Activate();
        public abstract void Deactivate();
    }
}