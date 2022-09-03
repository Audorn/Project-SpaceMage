using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Entities;

namespace SpaceMage.Missions
{
    public class MissionContent : MonoBehaviour
    {
        [SerializeField] private MissionType missionType;
        [SerializeField] private MapSize mapSize;
        [SerializeField] private List<Faction> factions;
        [SerializeField] private ThreatLevel maxThreatLevel;
        [SerializeField] private ThreatLevel expectedThreatLevel;
        [SerializeField] private ThreatLevel minThreatLevel;
        [SerializeField] private Rarity maxRarity;
        [SerializeField] private Rarity expectedRarity;
        [SerializeField] private Rarity minRarity;
        [SerializeField] private List<PrimaryModifier> primaryModifiers;
        [SerializeField] private List<SecondaryModifier> secondaryModifiers;
        [SerializeField] private List<Objective> objectives;
        [SerializeField] private List<Faction> setpieceFactions;
        [SerializeField] private List<Faction> hazardFactions;
        
        public MissionType MissionType { get { return missionType; } }
        public MapSize MapSize { get { return mapSize; } }
        public List<Faction> Factions { get { return factions; } }
        public ThreatLevel MaxThreatLevel { get { return maxThreatLevel; } }
        public ThreatLevel ExpectedThreatLevel { get { return expectedThreatLevel; } }
        public ThreatLevel MinThreatLevel { get { return minThreatLevel; } }
        public Rarity MaxRarity { get { return maxRarity; } }
        public Rarity ExpectedRarity { get { return expectedRarity; } }
        public Rarity MinRarity { get { return minRarity; } }
        public List<PrimaryModifier> PrimaryModifiers { get { return primaryModifiers; } }
        public List<SecondaryModifier> SecondaryModifiers { get { return secondaryModifiers; } }
        public List<Objective> Objectives { get { return objectives; } }
        public List<Faction> SetpieceFactions { get { return setpieceFactions; } }
        public List<Faction> HazardFactions { get { return hazardFactions; } }

        public void SetMissionType(MissionType missionType) { this.missionType = missionType; }
        public void SetMapSize(MapSize mapSize) { this.mapSize = mapSize; }
        public void SetFactions(List<Faction> factions) { this.factions = factions; }
        public void SetMaxThreatLevel(ThreatLevel threatLevel) { maxThreatLevel = threatLevel; }
        public void SetExpectedThreatLevel(ThreatLevel threatLevel) { expectedThreatLevel = threatLevel; }
        public void SetMinThreatLevel(ThreatLevel threatLevel) { minThreatLevel = threatLevel; }
        public void SetMaxRarity(Rarity rarity) { maxRarity = rarity; }
        public void SetExpectedRarity(Rarity rarity) { expectedRarity = rarity; }
        public void SetMinRarity(Rarity rarity) { minRarity = rarity; }
        public void SetPrimaryModifiers(List<PrimaryModifier> primaryModifiers) { this.primaryModifiers = primaryModifiers; }
        public void SetSecondaryModifiers(List<SecondaryModifier> secondaryModifiers) { this.secondaryModifiers = secondaryModifiers; }
        public void SetObjectives(List<Objective> objectives) { this.objectives = objectives; }
        public void SetSetpieces(List<Faction> setpieceFactions) { this.setpieceFactions = setpieceFactions; }
        public void SetHazardFactions(List<Faction> hazardFactions) { this.hazardFactions = hazardFactions; }
    }
}