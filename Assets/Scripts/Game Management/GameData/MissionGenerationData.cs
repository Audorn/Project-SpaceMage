using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.LevelGeneration;
using SpaceMage.Entities;

namespace SpaceMage.Data
{
    public class MissionGenerationData : MonoBehaviour
    {
        // =================================================
        // ==================== GETTERS ====================
        // =================================================
        [SerializeField] private MissionType missionType;

        [SerializeField] private List<Faction> guaranteedFactions = new List<Faction>();
        [SerializeField] private List<Faction> possibleFactions = new List<Faction>();
        [SerializeField] private List<Faction> blockedFactions = new List<Faction>();

        [SerializeField] private ThreatLevel maxThreatLevel;
        [SerializeField] private ThreatLevel expectedThreatLevel;
        [SerializeField] private ThreatLevel minThreatLevel;

        [SerializeField] private Rarity maxRarity;
        [SerializeField] private Rarity expectedRarity;
        [SerializeField] private Rarity minRarity;

        [SerializeField] private List<PrimaryModifier> guaranteedPrimaryModifiers = new List<PrimaryModifier>();
        [SerializeField] private List<PrimaryModifier> possiblePrimaryModifiers = new List<PrimaryModifier>();
        [SerializeField] private List<PrimaryModifier> blockedPrimaryModifiers = new List<PrimaryModifier>();

        [SerializeField] private List<SecondaryModifier> guaranteedSecondaryModifiers = new List<SecondaryModifier>();
        [SerializeField] private List<SecondaryModifier> possibleSecondaryModifiers = new List<SecondaryModifier>();
        [SerializeField] private List<SecondaryModifier> blockedSecondaryModifiers = new List<SecondaryModifier>();

        [SerializeField] private List<Objective> guaranteedObjectives = new List<Objective>();
        [SerializeField] private List<Objective> possibleObjectives = new List<Objective>();
        [SerializeField] private List<Objective> blockedObjectives = new List<Objective>();

        [SerializeField] private List<Setpiece> guaranteedSetpieces = new List<Setpiece>();
        [SerializeField] private List<Setpiece> possibleSetpieces = new List<Setpiece>();
        [SerializeField] private List<Setpiece> blockedSetpieces = new List<Setpiece>();

        [SerializeField] private List<Hazard> guaranteedHazards = new List<Hazard>();
        [SerializeField] private List<Hazard> possibleHazards = new List<Hazard>();
        [SerializeField] private List<Hazard> blockedHazards = new List<Hazard>();

        // ===================================================
        // ==================== ACCESSORS ====================
        // ===================================================
        public MissionType MissionType { get { return missionType; } set { missionType = value; } }

        public List<Faction> GuaranteedFactions { get { return guaranteedFactions; } set { guaranteedFactions = value; } }
        public List<Faction> PossibleFactions { get { return possibleFactions; } set { possibleFactions = value; } }
        public List<Faction> BlockedFactions { get { return blockedFactions; } set { blockedFactions = value; } }

        public ThreatLevel MaxThreatLevel { get { return maxThreatLevel; } set { maxThreatLevel = value; } }
        public ThreatLevel ExpectedThreatLevel { get { return ExpectedThreatLevel; } set { ExpectedThreatLevel = value; } }
        public ThreatLevel MinThreatLevel { get { return minThreatLevel; } set { minThreatLevel = value; } }

        public Rarity MaxRarity { get { return maxRarity; } set { maxRarity = value; } }
        public Rarity ExpectedRarity { get { return expectedRarity; } set { expectedRarity = value; } }
        public Rarity MinRarity { get { return minRarity; } set { minRarity = value; } }

        public List<PrimaryModifier> GuaranteedPrimaryModifiers { get { return guaranteedPrimaryModifiers; } set { guaranteedPrimaryModifiers = value; } }
        public List<PrimaryModifier> PossiblePrimaryModifiers { get { return possiblePrimaryModifiers; } set { possiblePrimaryModifiers = value; } }
        public List<PrimaryModifier> BlockedPrimaryModifiers { get { return blockedPrimaryModifiers; } set { blockedPrimaryModifiers = value; } }

        public List<SecondaryModifier> GuaranteedSecondaryModifiers { get { return guaranteedSecondaryModifiers; } set { guaranteedSecondaryModifiers = value; } }
        public List<SecondaryModifier> PossibleSecondaryModifiers { get { return possibleSecondaryModifiers; } set { possibleSecondaryModifiers = value; } }
        public List<SecondaryModifier> BlockedSecondaryModifiers { get { return blockedSecondaryModifiers; } set { blockedSecondaryModifiers = value; } }

        public List<Objective> GuaranteedObjectives { get { return guaranteedObjectives; } set { guaranteedObjectives = value; } }
        public List<Objective> PossibleObjectives { get { return possibleObjectives; } set { possibleObjectives = value; } }
        public List<Objective> BlockedObjectives { get { return blockedObjectives; } set { blockedObjectives = value; } }

        public List<Setpiece> GuaranteedSetpieces { get { return guaranteedSetpieces; } set { guaranteedSetpieces = value; } }
        public List<Setpiece> PossibleSetpieces { get { return possibleSetpieces; } set { possibleSetpieces = value; } }
        public List<Setpiece> BlockedSetpieces { get { return blockedSetpieces; } set { blockedSetpieces = value; } }

        public List<Hazard> GuaranteedHazards { get { return guaranteedHazards; } set { guaranteedHazards = value; } }
        public List<Hazard> PossibleHazards { get { return possibleHazards; } set { possibleHazards = value; } }
        public List<Hazard> BlockedHazards { get { return blockedHazards; } set { blockedHazards = value; } }
    }
}