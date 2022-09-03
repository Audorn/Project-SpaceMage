using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            MissionSelectorInitializer initializer = new MissionSelectorInitializer(transform, MissionManager.GetSelectedMissionPossibleContent());
            initializer.Execute();
            missionContent = initializer.MissionContent;

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
            FactionSelector factionSelector = new FactionSelector();
            factionSelector.Execute();
            missionContent.SetFactions(factionSelector.SelectedFactions);

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
            PrimaryModifiersSelector modifiersSelector = new PrimaryModifiersSelector();
            modifiersSelector.Execute();
            missionContent.SetPrimaryModifiers(modifiersSelector.SelectedPrimaryModifiers);

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
            SecondaryModifiersSelector modifiersSelector = new SecondaryModifiersSelector();
            modifiersSelector.Execute();
            missionContent.SetSecondaryModifiers(modifiersSelector.SelectedSecondaryModifiers);

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
            SetpiecesSelector setpiecesSelector = new SetpiecesSelector();
            setpiecesSelector.Execute();
            missionContent.SetSetpieces(setpiecesSelector.SelectedSetpieceFactions);

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
            HazardsSelector hazardsSelecor = new HazardsSelector();
            hazardsSelecor.Execute();
            missionContent.SetHazardFactions(hazardsSelecor.SelectedHazardFactions);

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
            ObjectivesSelector objectivesSelector = new ObjectivesSelector();
            objectivesSelector.Execute();
            missionContent.SetObjectives(objectivesSelector.SelectedObjectives);

            MissionManager.SetCurrentMissionContent(missionContent); // TODO: Should add a final state for setting finishing.

            yield return new WaitForFixedUpdate();
            MissionContentSelectorStateHandler.DeRegisterActiveContentSelector(this);
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