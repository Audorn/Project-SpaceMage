using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(IDealImpactDamage))]
    [RequireComponent(typeof(DieFromHealthLoss))]
    public class Hazard : Actor {}
}