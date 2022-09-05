using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    /// <summary>
    /// A spell that can enchant or be cast.
    /// </summary>
    [System.Serializable]
    public abstract class Spell
    {
        [SerializeField] protected string id;               // Editor configurable.
        [SerializeField] protected string name;             // Editor configurable.

        public abstract void Enchant();
        public abstract void Cast();
    }
}