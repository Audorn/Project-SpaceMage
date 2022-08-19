using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    public struct SortingData
    {
        private Faction faction;
        private ThreatLevel threatLevel;
        private Rarity rarity;
        private int numberToGet;
        private bool isEachUnique;
        public Faction Faction { get { return faction; } }
        public ThreatLevel ThreatLevel { get { return threatLevel; } }
        public Rarity Rarity { get { return rarity; } }
        public int NumberToGet { get { return numberToGet; } }
        public bool IsEachUnique { get { return isEachUnique; } }

        public SortingData(Faction faction, ThreatLevel threatLevel, Rarity rarity, int numberToGet, bool isEachUnique = true)
        {
            this.faction = faction;
            this.threatLevel = threatLevel;
            this.rarity = rarity;
            this.numberToGet = numberToGet;
            this.isEachUnique = isEachUnique;
        }
    }
}