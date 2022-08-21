using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    [System.Serializable]
    public struct FilterData
    {
        [SerializeField] private string prefabId;
        [SerializeField] private Faction faction;
        [SerializeField] private ThreatLevel threatLevel;
        [SerializeField] private Rarity rarity;
        public string PrefabId { get { return prefabId; } }
        public Faction Faction { get { return faction; } }
        public ThreatLevel ThreatLevel { get { return threatLevel; } }
        public Rarity Rarity { get { return rarity; } }

        public FilterData(string prefabId)
        {
            this.prefabId = prefabId;
            faction = Faction.ANY;
            threatLevel = ThreatLevel.ANY;
            rarity = Rarity.ANY;
        }

        public FilterData(Faction faction, ThreatLevel threatLevel, Rarity rarity)
        {
            prefabId = "";
            this.faction = faction;
            this.threatLevel = threatLevel;
            this.rarity = rarity;
        }
    }
}