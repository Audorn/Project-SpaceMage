using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(IDealImpactDamage))]
    public class Hazard : MonoBehaviour
    {
        [SerializeField] private FilterData filterData;
        public FilterData FilterData { get { return filterData; } }
    }
}