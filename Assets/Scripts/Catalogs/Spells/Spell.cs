using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    /// <summary>
    /// A spell that can enchant or be cast.
    /// </summary>
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField] protected string id;               // Editor configurable.
        [SerializeField] protected string uiName;             // Editor configurable.
        [SerializeField] protected Sprite sprite;           // Editor configurable.

        public string Id => id;
        public string UIName => uiName;
        public Sprite Sprite => sprite;

        public abstract void Enchant();
        public abstract void Cast();
    }
}