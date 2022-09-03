using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    /// <summary>
    /// Allows passing a boolean around by reference.
    /// </summary>
    public class PersistentFlag
    {
        private bool flag;
        public bool Flag { get { return flag; } }

        public void SetFlag(bool flag) { this.flag = flag; }

        public PersistentFlag(bool flag) { this.flag = flag; }
    }
}