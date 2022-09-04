using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Missions
{
    public class Setpiece : MonoBehaviour
    {
        [SerializeField] private CatalogFilterData filterData;
        public CatalogFilterData FilterData { get { return filterData; } }
    }
}