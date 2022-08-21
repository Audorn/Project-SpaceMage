using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    public enum Catalog
    {
        ANY,
        HAZARD,
        SETPIECE,
        SHIP,
        ENGINE
    }

    public enum Faction
    {
        ANY,
        HUMAN,
        ALIEN,
        AI,
        NEUTRAL
    }

    public enum ThreatLevel
    {
        ANY,
        VERY_LOW,
        LOW,
        MEDIUM,
        HIGH,
        VERY_HIGH,
        INSANE
    }

    public enum Rarity
    {
        ANY,
        VERY_COMMON,
        COMMON,
        UNCOMMON,
        RARE,
        VERY_RARE,
        UNIQUE
    }
}