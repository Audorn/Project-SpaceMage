using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    [System.Serializable]
    public class FilterData
    {
        [SerializeField] private string prefabId;
        [SerializeField] private Faction faction;
        [SerializeField] private ThreatLevel threatLevel;
        [SerializeField] private Rarity rarity;
        [SerializeField] private List<Tag> tags = new List<Tag>() { Tag.NONE };
        public string PrefabId { get { return prefabId; } }
        public Faction Faction { get { return faction; } }
        public ThreatLevel ThreatLevel { get { return threatLevel; } }
        public Rarity Rarity { get { return rarity; } }
        public List<Tag> Tags { get { return tags; } }

        public FilterData(string prefabId)
        {
            this.prefabId = prefabId;
            faction = Faction.ANY;
            threatLevel = ThreatLevel.ANY;
            rarity = Rarity.ANY;
            tags = new List<Tag>() { Tag.NONE };
        }

        public FilterData(Faction faction, ThreatLevel threatLevel, Rarity rarity, List<Tag> tags)
        {
            prefabId = "";
            this.faction = faction;
            this.threatLevel = threatLevel;
            this.rarity = rarity;
            this.tags = tags;
        }
    }
}