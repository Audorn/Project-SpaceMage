using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.LevelGeneration
{
    public class Setpiece : MonoBehaviour
    {
        [SerializeField] private FilterData filterData;
        public FilterData FilterData { get { return filterData; } }
    }
}