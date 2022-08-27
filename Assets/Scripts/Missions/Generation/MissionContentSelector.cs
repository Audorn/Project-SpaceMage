using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SpaceMage.Data;

namespace SpaceMage.Missions
{
    [RequireComponent(typeof(MissionContentGenerator))]
    public class MissionContentSelector : MonoBehaviour
    {
        // Singleton.
        private static MissionContentSelector _;
        public static MissionContentSelector Singleton { get { return _; } }
        private void Awake() { _ = this; }

        // ==================================================
        // ==================== SETTINGS ====================
        // ==================================================
        [SerializeField] private MissionPossibleContent missionPossibleContent;
        [SerializeField] private MissionContent missionContent;


        // ====================================================
        // ==================== EXTERNALS =====================
        // ====================================================
        public static void Initialize() { MissionContentSelectorStateHandler.Initialize(); }


        // ====================================================
        // ==================== INTERNALS =====================
        // ====================================================
        private void initialize()
        {
            MissionContentSelectorStateHandler.RegisterActiveContentSelector(this);
            StartCoroutine(_initialize());
        }
        private IEnumerator _initialize()
        {
            // Get missionPossibleContent and create missionContent objects from prefab.
            Debug.Log("Content selector working...");
            missionPossibleContent = MissionManager.GetSelectedMissionPossibleContent();

            GameObject go = Instantiate(GameData.MissionContentPrefab, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(transform);

            MissionContent missionContent = go.GetComponent<MissionContent>();
            MissionManager.SetCurrentMissionContent(missionContent);
            MissionManager.CurrentMission.SetMissionContent(missionContent);
            this.missionContent = missionContent;

            missionContent.SetMissionType(missionPossibleContent.MissionType);
            missionContent.SetMapSize(missionPossibleContent.MapSize);
            missionContent.SetMaxThreatLevel(missionPossibleContent.MaxThreatLevel);
            missionContent.SetExpectedThreatLevel(missionPossibleContent.ExpectedThreatLevel);
            missionContent.SetMinThreatLevel(missionPossibleContent.MinThreatLevel);
            missionContent.SetMaxRarity(missionPossibleContent.MaxRarity);
            missionContent.SetExpectedRarity(missionPossibleContent.ExpectedRarity);
            missionContent.SetMinRarity(missionPossibleContent.MinRarity);
            
            yield return new WaitForFixedUpdate();
            MissionContentSelectorStateHandler.DeRegisterActiveContentSelector(this);
        }

        private void selectFactions()
        {
            MissionContentSelectorStateHandler.RegisterActiveContentSelector(this);
            StartCoroutine(_selectFactions());
        }
        private IEnumerator _selectFactions()
        {
            Debug.Log("Content selector working...");
            int quantity = calculateQuantityToSelect(missionPossibleContent.PercentFactionsTable, missionPossibleContent.QuantityFactionsTable);

            var guaranteed = missionPossibleContent.GuaranteedFactions.ConvertAll(value => (int)value);
            var likely = missionPossibleContent.LikelyFactions.ConvertAll(value => (int)value);
            var blocked = missionPossibleContent.BlockedFactions.ConvertAll(value => (int)value);
            var all = ((Faction[])Enum.GetValues(typeof(Faction))).ToList().ConvertAll(value => (int)value);

            var selected = selectOptionsFromEnumAsInts(quantity, guaranteed, likely, blocked, all).ConvertAll(value => (Faction)value);
            missionContent.SetFactions(selected);

            yield return new WaitForFixedUpdate();
            MissionContentSelectorStateHandler.DeRegisterActiveContentSelector(this);
        }

        private void selectPrimaryModifiers()
        {
            MissionContentSelectorStateHandler.RegisterActiveContentSelector(this);
            StartCoroutine(_selectPrimaryModifier());
        }
        private IEnumerator _selectPrimaryModifier()
        {
            Debug.Log("Content selector working...");
            int quantity = calculateQuantityToSelect(missionPossibleContent.PercentPrimaryModifiersTable, missionPossibleContent.QuantityPrimaryModifiersTable);

            var guaranteed = missionPossibleContent.GuaranteedPrimaryModifiers.ConvertAll(value => (int)value);
            var likely = missionPossibleContent.LikelyPrimaryModifiers.ConvertAll(value => (int)value);
            var blocked = missionPossibleContent.BlockedPrimaryModifiers.ConvertAll(value => (int)value);
            var all = ((PrimaryModifier[])Enum.GetValues(typeof(PrimaryModifier))).ToList().ConvertAll(value => (int)value);

            var selected = selectOptionsFromEnumAsInts(quantity, guaranteed, likely, blocked, all).ConvertAll(value => (PrimaryModifier)value);
            missionContent.SetPrimaryModifiers(selected);

            yield return new WaitForFixedUpdate();
            MissionContentSelectorStateHandler.DeRegisterActiveContentSelector(this);
        }

        private void selectSecondaryModifiers()
        {
            MissionContentSelectorStateHandler.RegisterActiveContentSelector(this);
            StartCoroutine(_selectSecondaryModifiers());
        }
        private IEnumerator _selectSecondaryModifiers()
        {
            Debug.Log("Content selector working...");
            int quantity = calculateQuantityToSelect(missionPossibleContent.PercentSecondaryModifiersTable, missionPossibleContent.QuantitySecondaryModifiersTable);

            var guaranteed = missionPossibleContent.GuaranteedSecondaryModifiers.ConvertAll(value => (int)value);
            var likely = missionPossibleContent.LikelySecondaryModifiers.ConvertAll(value => (int)value);
            var blocked = missionPossibleContent.BlockedSecondaryModifiers.ConvertAll(value => (int)value);
            var all = ((SecondaryModifier[])Enum.GetValues(typeof(PrimaryModifier))).ToList().ConvertAll(value => (int)value);

            var selected = selectOptionsFromEnumAsInts(quantity, guaranteed, likely, blocked, all).ConvertAll(value => (SecondaryModifier)value);
            missionContent.SetSecondaryModifiers(selected);

            yield return new WaitForFixedUpdate();
            MissionContentSelectorStateHandler.DeRegisterActiveContentSelector(this);
        }

        private void selectSetpieces()
        {
            MissionContentSelectorStateHandler.RegisterActiveContentSelector(this);
            StartCoroutine(_selectSetpieces());
        }
        private IEnumerator _selectSetpieces()
        {
            Debug.Log("Content selector working...");
            int quantity = calculateQuantityToSelect(missionPossibleContent.PercentSetpiecesTable, missionPossibleContent.QuantitySetpiecesTable);

            var guaranteed = missionPossibleContent.GuaranteedSetpieces;
            var likely = missionPossibleContent.LikelySetpieces;
            var blocked = missionPossibleContent.BlockedSetpieces;
            var all = GameData.SetpiecePrefabs;

            var selected = selectOptionsFromPrefabs(quantity, guaranteed, likely, blocked, all);
            missionContent.SetSetpieces(selected);

            yield return new WaitForFixedUpdate();
            MissionContentSelectorStateHandler.DeRegisterActiveContentSelector(this);
        }

        private void selectHazards()
        {
            MissionContentSelectorStateHandler.RegisterActiveContentSelector(this);
            StartCoroutine(_selectHazards());
        }
        private IEnumerator _selectHazards()
        {
            // TODO: Select hazard groups instead of individual hazards. (Asteroids come in three phases)
            Debug.Log("Content selector working...");
            int quantity = calculateQuantityToSelect(missionPossibleContent.PercentHazardsTable, missionPossibleContent.QuantityHazardsTable);

            var guaranteed = missionPossibleContent.GuaranteedHazards;
            var likely = missionPossibleContent.LikelyHazards;
            var blocked = missionPossibleContent.BlockedHazards;
            var all = GameData.HazardPrefabs;

            var selected = selectOptionsFromPrefabs(quantity, guaranteed, likely, blocked, all);
            missionContent.SetHazards(selected);

            yield return new WaitForFixedUpdate();
            MissionContentSelectorStateHandler.DeRegisterActiveContentSelector(this);
        }

        private void selectEnemies()
        {
            MissionContentSelectorStateHandler.RegisterActiveContentSelector(this);
            StartCoroutine(_selectEnemies());
        }
        private IEnumerator _selectEnemies()
        {
            // Do work here.
            Debug.Log("Content selector working...");

            yield return new WaitForFixedUpdate();
            MissionContentSelectorStateHandler.DeRegisterActiveContentSelector(this);
        }

        private void selectObjectives()
        {
            MissionContentSelectorStateHandler.RegisterActiveContentSelector(this);
            StartCoroutine(_selectObjectives());
        }
        private IEnumerator _selectObjectives()
        {
            // TODO: Objectives can be known or hidden. Usually only 1 is known at mission start.
            // TODO: 1 = 25%, 2 = 50%, 3 = 25%.
            Debug.Log("Content selector working...");
            int quantity = calculateQuantityToSelect(missionPossibleContent.PercentObjectivesTable, missionPossibleContent.QuantityObjectivesTable);

            var guaranteed = missionPossibleContent.GuaranteedObjectives.ConvertAll(value => (int)value);
            var likely = missionPossibleContent.LikelyObjectives.ConvertAll(value => (int)value);
            var blocked = missionPossibleContent.BlockedObjectives.ConvertAll(value => (int)value);
            var all = ((Objective[])Enum.GetValues(typeof(Objective))).ToList().ConvertAll(value => (int)value);

            var selected = selectOptionsFromEnumAsInts(quantity, guaranteed, likely, blocked, all).ConvertAll(value => (Objective)value);
            missionContent.SetObjectives(selected);

            yield return new WaitForFixedUpdate();
            MissionContentSelectorStateHandler.DeRegisterActiveContentSelector(this);
        }

        private int calculateQuantityToSelect(List<float> percents, List<int> quantities)
        {
            // Early out - Percents or quantities not provided.
            if (percents.Count == 0 || quantities.Count == 0)
                return 1;

            float percent = UnityEngine.Random.Range(0f, 1f);
            int index = 0;
            for (int i = 0; i < percents.Count; i++)
            {
                // Early out - this percent is too high for this row.
                if (percent > percents[i])
                    break;

                // Early out - quantities doesn't have a corresponding row.
                if (quantities.Count <= index)
                    break;

                index = i;
            }

            return quantities[index];
        }

        private List<int> selectOptionsFromEnumAsInts(int quantity, List<int> guaranteed, List<int> likely, List<int> blocked, List<int> all)
        {
            var selected = new List<int>();

            if (guaranteed.Count > 0)
                selected.AddRange(guaranteed);

            // Early out - Only enough room for the guaranteed options.
            if (selected.Count >= quantity)
                return selected;

            selected = selectOptionsFromLimitedList(selected, likely, quantity);

            // Early out - Only enough room for guaranteed and likely options.
            if (selected.Count >= quantity)
                return selected;

            all = all.Except(selected).Except(guaranteed).Except(likely).Except(blocked).ToList();
            selected = selectOptionsFromLimitedList(selected, all, quantity);

            return selected;
        }
        private List<GameObject> selectOptionsFromPrefabs(int quantity, List<GameObject> guaranteed, List<GameObject> likely, List<GameObject> blocked, List<GameObject> all)
        {
            var selected = new List<GameObject>();

            if (guaranteed.Count > 0)
                selected.AddRange(guaranteed);

            // Early out - Only enough room for the guaranteed options.
            if (selected.Count >= quantity)
                return selected;

            selected = selectOptionsFromLimitedList(selected, likely, quantity);

            // Early out - Only enough room for guaranteed and likely options.
            if (selected.Count >= quantity)
                return selected;

            all = all.Except(selected).Except(guaranteed).Except(likely).Except(blocked).ToList();
            selected = selectOptionsFromLimitedList(selected, all, quantity);

            return selected;
        }

        private List<int> selectOptionsFromLimitedList(List<int> selected, List<int> list, int quantity)
        {
            if (list.Count > 0)
            {
                while (selected.Count < quantity && list.Count > 0)
                {
                    int index = UnityEngine.Random.Range(0, list.Count);
                    selected.Add(list[index]);
                    list.RemoveAt(index);
                }
            }

            return selected;
        }
        private List<GameObject> selectOptionsFromLimitedList(List<GameObject> selected, List<GameObject> list, int quantity)
        {
            if (list.Count > 0)
            {
                while (selected.Count < quantity && list.Count > 0)
                {
                    int index = UnityEngine.Random.Range(0, list.Count);
                    selected.Add(list[index]);
                    list.RemoveAt(index);
                }
            }

            return selected;
        }

        private void Start()
        {
            MissionContentSelectorStateHandler.Singleton.Initializing.AddListener(initialize);
            MissionContentSelectorStateHandler.Singleton.SelectingFactions.AddListener(selectFactions);
            MissionContentSelectorStateHandler.Singleton.SelectingPrimaryModifiers.AddListener(selectPrimaryModifiers);
            MissionContentSelectorStateHandler.Singleton.SelectingSecondaryModifiers.AddListener(selectSecondaryModifiers);
            MissionContentSelectorStateHandler.Singleton.SelectingSetpieces.AddListener(selectSetpieces);
            MissionContentSelectorStateHandler.Singleton.SelectingHazards.AddListener(selectHazards);
            MissionContentSelectorStateHandler.Singleton.SelectingEnemies.AddListener(selectEnemies);
            MissionContentSelectorStateHandler.Singleton.SelectingObjectives.AddListener(selectObjectives);
        }
    }
}