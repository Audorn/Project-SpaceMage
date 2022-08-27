using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Entities;

namespace SpaceMage.Missions
{
    public class MissionPossibleContent : MonoBehaviour
    {
        // =================================================
        // ==================== GETTERS ====================
        // =================================================
        [SerializeField] private MissionType missionType;
        [SerializeField] private MapSize mapSize;

        [SerializeField] private List<int> quantityFactionsTable = new List<int>();
        [SerializeField] private List<float> percentFactionsTable = new List<float>();
        [SerializeField] private List<Faction> guaranteedFactions = new List<Faction>();
        [SerializeField] private List<Faction> likelyFactions = new List<Faction>();
        [SerializeField] private List<Faction> blockedFactions = new List<Faction>();

        [SerializeField] private ThreatLevel maxThreatLevel;
        [SerializeField] private ThreatLevel expectedThreatLevel;
        [SerializeField] private ThreatLevel minThreatLevel;

        [SerializeField] private Rarity maxRarity;
        [SerializeField] private Rarity expectedRarity;
        [SerializeField] private Rarity minRarity;

        [SerializeField] private List<int> quantityPrimaryModifiersTable = new List<int>();
        [SerializeField] private List<float> percentPrimaryModifiersTable = new List<float>();
        [SerializeField] private List<PrimaryModifier> guaranteedPrimaryModifiers = new List<PrimaryModifier>();
        [SerializeField] private List<PrimaryModifier> likelyPrimaryModifiers = new List<PrimaryModifier>();
        [SerializeField] private List<PrimaryModifier> blockedPrimaryModifiers = new List<PrimaryModifier>();

        [SerializeField] private List<int> quantitySecondaryModifiersTable = new List<int>();
        [SerializeField] private List<float> percentSecondaryModifiersTable = new List<float>();
        [SerializeField] private List<SecondaryModifier> guaranteedSecondaryModifiers = new List<SecondaryModifier>();
        [SerializeField] private List<SecondaryModifier> likelySecondaryModifiers = new List<SecondaryModifier>();
        [SerializeField] private List<SecondaryModifier> blockedSecondaryModifiers = new List<SecondaryModifier>();

        [SerializeField] private List<int> quantityObjectivesTable = new List<int>();
        [SerializeField] private List<float> percentObjectivesTable = new List<float>();
        [SerializeField] private List<Objective> guaranteedObjectives = new List<Objective>();
        [SerializeField] private List<Objective> likelyObjectives = new List<Objective>();
        [SerializeField] private List<Objective> blockedObjectives = new List<Objective>();

        [SerializeField] private List<int> quantitySetpiecesTable = new List<int>();
        [SerializeField] private List<float> percentSetpiecesTable = new List<float>();
        [SerializeField] private List<GameObject> guaranteedSetpieces = new List<GameObject>();
        [SerializeField] private List<GameObject> likelySetpieces = new List<GameObject>();
        [SerializeField] private List<GameObject> blockedSetpieces = new List<GameObject>();

        [SerializeField] private List<int> quantityHazardsTable = new List<int>();
        [SerializeField] private List<float> percentHazardsTable = new List<float>();
        [SerializeField] private List<GameObject> guaranteedHazards = new List<GameObject>();
        [SerializeField] private List<GameObject> likelyHazards = new List<GameObject>();
        [SerializeField] private List<GameObject> blockedHazards = new List<GameObject>();

        // ===================================================
        // ==================== ACCESSORS ====================
        // ===================================================
        public MissionType MissionType { get { return missionType; } set { missionType = value; } }
        public MapSize MapSize { get { return mapSize; } set { mapSize = value; } }

        public List<float> PercentFactionsTable { get { return percentFactionsTable; } }
        public List<int> QuantityFactionsTable { get { return quantityFactionsTable; } }
        public List<Faction> GuaranteedFactions { get { return guaranteedFactions; } set { guaranteedFactions = value; } }
        public List<Faction> LikelyFactions { get { return likelyFactions; } set { likelyFactions = value; } }
        public List<Faction> BlockedFactions { get { return blockedFactions; } set { blockedFactions = value; } }

        public ThreatLevel MaxThreatLevel { get { return maxThreatLevel; } set { maxThreatLevel = value; } }
        public ThreatLevel ExpectedThreatLevel { get { return expectedThreatLevel; } set { expectedThreatLevel = value; } }
        public ThreatLevel MinThreatLevel { get { return minThreatLevel; } set { minThreatLevel = value; } }

        public Rarity MaxRarity { get { return maxRarity; } set { maxRarity = value; } }
        public Rarity ExpectedRarity { get { return expectedRarity; } set { expectedRarity = value; } }
        public Rarity MinRarity { get { return minRarity; } set { minRarity = value; } }

        public List<float> PercentPrimaryModifiersTable { get { return percentPrimaryModifiersTable; } }
        public List<int> QuantityPrimaryModifiersTable { get { return quantityPrimaryModifiersTable; } }
        public List<PrimaryModifier> GuaranteedPrimaryModifiers { get { return guaranteedPrimaryModifiers; } set { guaranteedPrimaryModifiers = value; } }
        public List<PrimaryModifier> LikelyPrimaryModifiers { get { return likelyPrimaryModifiers; } set { likelyPrimaryModifiers = value; } }
        public List<PrimaryModifier> BlockedPrimaryModifiers { get { return blockedPrimaryModifiers; } set { blockedPrimaryModifiers = value; } }

        public List<float> PercentSecondaryModifiersTable { get { return percentSecondaryModifiersTable; } }
        public List<int> QuantitySecondaryModifiersTable { get { return quantitySecondaryModifiersTable; } }
        public List<SecondaryModifier> GuaranteedSecondaryModifiers { get { return guaranteedSecondaryModifiers; } set { guaranteedSecondaryModifiers = value; } }
        public List<SecondaryModifier> LikelySecondaryModifiers { get { return likelySecondaryModifiers; } set { likelySecondaryModifiers = value; } }
        public List<SecondaryModifier> BlockedSecondaryModifiers { get { return blockedSecondaryModifiers; } set { blockedSecondaryModifiers = value; } }

        public List<float> PercentObjectivesTable { get { return percentObjectivesTable; } }
        public List<int> QuantityObjectivesTable { get { return quantityObjectivesTable; } }
        public List<Objective> GuaranteedObjectives { get { return guaranteedObjectives; } set { guaranteedObjectives = value; } }
        public List<Objective> LikelyObjectives { get { return likelyObjectives; } set { likelyObjectives = value; } }
        public List<Objective> BlockedObjectives { get { return blockedObjectives; } set { blockedObjectives = value; } }

        public List<float> PercentSetpiecesTable { get { return percentSetpiecesTable; } }
        public List<int> QuantitySetpiecesTable { get { return quantitySetpiecesTable; } }
        public List<GameObject> GuaranteedSetpieces { get { return guaranteedSetpieces; } set { guaranteedSetpieces = value; } }
        public List<GameObject> LikelySetpieces { get { return likelySetpieces; } set { likelySetpieces = value; } }
        public List<GameObject> BlockedSetpieces { get { return blockedSetpieces; } set { blockedSetpieces = value; } }

        public List<float> PercentHazardsTable { get { return percentHazardsTable; } }
        public List<int> QuantityHazardsTable { get { return quantityHazardsTable; } }
        public List<GameObject> GuaranteedHazards { get { return guaranteedHazards; } set { guaranteedHazards = value; } }
        public List<GameObject> LikelyHazards { get { return likelyHazards; } set { likelyHazards = value; } }
        public List<GameObject> BlockedHazards { get { return blockedHazards; } set { blockedHazards = value; } }
    }
}