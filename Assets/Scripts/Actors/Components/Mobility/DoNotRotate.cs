using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Extensions;

namespace SpaceMage.Utilities
{
    public class DoNotRotate : MonoBehaviour
    {
        private void Update() { transform.eulerAngles = Vector3.zero; }
    }
}