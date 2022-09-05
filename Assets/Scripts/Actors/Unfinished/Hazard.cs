using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Actors
{
    [RequireComponent(typeof(DamageOnCollision))]
    [RequireComponent(typeof(DieFromHealthLoss))]
    public class Hazard : Actor {}
}