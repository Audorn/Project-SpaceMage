using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Extensions;

namespace SpaceMage.Utilities
{
    /// <summary>
    /// Keep this object from rotating at all times.
    /// </summary>
    public class DoNotRotate : MonoBehaviour
    {
        private void Update() => transform.eulerAngles = Vector3.zero;
    }
}