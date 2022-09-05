using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    /// <summary>
    /// This is a dummy module for when a module is not installed.
    /// </summary>
    public class EmptyModule : Module
    {
        public override void Activate() { }

        public override void Deactivate() { }
    }
}