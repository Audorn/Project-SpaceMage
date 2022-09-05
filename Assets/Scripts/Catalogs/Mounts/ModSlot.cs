using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Ships
{
    public class ModSlot
    {
        private Module module;

        public bool IsEmpty => module == null || module is EmptyModule;

        public void InstallModule(Module module)
        {
            this.module = module;
            module.Activate();
        }
    }
}