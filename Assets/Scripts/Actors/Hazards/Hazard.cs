using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(IDealImpactDamage))]
    public class Hazard : MonoBehaviour
    {
        // =================================================
        // ==================== SORTING ====================
        // =================================================
        [SerializeField] private Faction faction;
        [SerializeField] private ThreatLevel threatLevel;
        [SerializeField] private Rarity rarity;

        public Faction Faction { get { return faction; } }
        public ThreatLevel ThreatLevel { get { return threatLevel; } }
        public Rarity Rarity { get { return rarity; } }
    }
}