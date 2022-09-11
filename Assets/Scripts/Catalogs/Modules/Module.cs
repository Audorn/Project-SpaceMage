using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    /// <summary>
    /// A module that can be activated and deactivated.
    /// </summary>
    public class Module : MonoBehaviour
    {
        [SerializeField] protected string id;                   // Editor configurable.
        [SerializeField] protected string uiName;               // Editor configurable.
        [SerializeField] protected Sprite sprite;               // Editor configurable.

        public string Id => id;
        public string UIName => uiName;
        public Sprite Sprite => sprite;

        public virtual void Activate() { }
        public virtual void Deactivate() { }
    }
}