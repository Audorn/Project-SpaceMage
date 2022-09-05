using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage
{
    public enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }
    public enum SpawnPool
    {
        PRIMARY,
        SECONDARY,
        TERTIARY,
        QUATERNARY
    }

    public enum SpawnEvent
    {
        CREATION,
        SPOT_TARGET,
        ATTACK,
        FLEE,
        TAKE_DAME,
        DEATH,
        INTERVAL,
    }

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
        TRANCERS,
        CREATURES,
        LOST,
        MEME,
        REVENANT,
        ELDRITCH,
        ETHEREALS,
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

    public enum Tag
    {
        NONE,
        SMALL,
        LARGE,
        SPAWNS_CHILDREN,
        STATIONARY
    }

    public enum HandleMomentum
    {
        OVERRIDE,
        RETAIN,
        AVERAGE
    }

    public enum MissionType
    {
        SURVIVAL,
        DEFENSE,
        EXPLORATION,
        EXPERIMENT,
        RECOVERY,
        REPAIR
    }

    public enum MapSize
    {
        TINY,
        SMALL,
        MEDIUM,
        LARGE,
        MASSIVE
    }

    public enum PrimaryModifier
    {
        WAVES,
        WAYPOINTS,
        ALIEN_HIVE,
        MAELSTROM,
        DROUGHT,
        ONE_WAY,
        SPIRAL
    }

    public enum SecondaryModifier
    {
        BIG_MAP,
        SMALL_MAP,
        CORRIDOR,
        BOSS,
        HORDE,
        CLUTTER,
        ALLY_PRESENCE,
        FACTION_PRESENCE,
        WRAP,
        SPLIT_MAP,
        WARP,
        NO_WEAPONS,
        NO_EQUIPMENT,
        NO_DEFENSES
    }

    public enum Objective
    {
        SURVIVE,
        ESCORT,
        DEFEND,
        RESCUE,
        EXPLORE,
        SCAN,
        DEAL_DAMAGE,
        DESTROY_TARGET,
    }

    public enum HardpointRating
    {
        LIGHT,
        MEDIUM,
        HEAVY
    }
    
    public enum HullStrength
    {
        POOR,
        STANDARD,
        REINFORCED,
        ARMORED
    }
}