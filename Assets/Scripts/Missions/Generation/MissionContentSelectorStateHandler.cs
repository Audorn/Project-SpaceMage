using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceMage.Missions
{
    public class MissionContentSelectorStateHandler : MonoBehaviour
    {
        // Singleton allowing access to the level data generator state handler through the static class.
        private static MissionContentSelectorStateHandler _;
        public static MissionContentSelectorStateHandler Singleton { get { return _; } }
        private void Awake() { _ = this; }

        // Singleton allowing access to the level data generator state through the static class.
        private static MissionContentSelectorState state = MissionContentSelectorState.WAITING;
        public static MissionContentSelectorState State { get { return state; } }

        public UnityEvent Initializing = new UnityEvent();
        public UnityEvent InitializingComplete = new UnityEvent();
        public UnityEvent SelectingFactions = new UnityEvent();
        public UnityEvent SelectingFactionsComplete = new UnityEvent();
        public UnityEvent SelectingPrimaryModifiers = new UnityEvent();
        public UnityEvent SelectingPrimaryModifiersComplete = new UnityEvent();
        public UnityEvent SelectingSecondaryModifiers = new UnityEvent();
        public UnityEvent SelectingSecondaryModifiersComplete = new UnityEvent();
        public UnityEvent SelectingSetpieces = new UnityEvent();
        public UnityEvent SelectingSetpiecesComplete = new UnityEvent();
        public UnityEvent SelectingHazards = new UnityEvent();
        public UnityEvent SelectingHazardsComplete = new UnityEvent();
        public UnityEvent SelectingEnemies = new UnityEvent();
        public UnityEvent SelectingEnemiesComplete = new UnityEvent();
        public UnityEvent SelectingObjectives = new UnityEvent();
        public UnityEvent SelectingObjectivesComplete = new UnityEvent();
        public UnityEvent Finished = new UnityEvent();

        private bool doInitializing = false;
        private bool doInitializingComplete = false;
        private bool doSelectingFactions = false;
        private bool doSelectingFactionsComplete = false;
        private bool doSelectingPrimaryModifiers = false;
        private bool doSelectingPrimaryModifiersComplete = false;
        private bool doSelectingSecondaryModifiers = false;
        private bool doSelectingSecondaryModifiersComplete = false;
        private bool doSelectingSetpieces = false;
        private bool doSelectingSetpiecesComplete = false;
        private bool doSelectingHazards = false;
        private bool doSelectingHazardsComplete = false;
        private bool doSelectingEnemies = false;
        private bool doSelectingEnemiesComplete = false;
        private bool doSelectingObjectives = false;
        private bool doSelectingObjectivesComplete = false;
        private bool doFinished = false;

        public static void Initialize() { _.doInitializing = true; }

        private static HashSet<MissionContentSelector> activeContentSelectors = new HashSet<MissionContentSelector>();
        public static void RegisterActiveContentSelector(MissionContentSelector contentSelector) { activeContentSelectors.Add(contentSelector); }
        public static void DeRegisterActiveContentSelector(MissionContentSelector contentSelector) { activeContentSelectors.Remove(contentSelector); }
        /// <summary>
        /// Called by LevelManager to start the level data generator state handler.
        /// </summary>
        public void Start()
        {
            // Register all the simple actions below.
            Initializing.AddListener(initializing);
            InitializingComplete.AddListener(initializingComplete);
            SelectingFactions.AddListener(selectingFactions);
            SelectingFactionsComplete.AddListener(selectingFactionsComplete);
            SelectingPrimaryModifiers.AddListener(selectingPrimaryModifiers);
            SelectingPrimaryModifiersComplete.AddListener(selectingPrimaryModifiersComplete);
            SelectingSecondaryModifiers.AddListener(selectingSecondaryModifiers);
            SelectingSecondaryModifiersComplete.AddListener(selectingSecondaryModifiersComplete);
            SelectingSetpieces.AddListener(selectingSetpieces);
            SelectingSetpiecesComplete.AddListener(selectingSetpiecesComplete);
            SelectingHazards.AddListener(selectingHazards);
            SelectingHazardsComplete.AddListener(selectingHazardsComplete);
            SelectingEnemies.AddListener(selectingEnemies);
            SelectingEnemiesComplete.AddListener(selectingEnemiesComplete);
            SelectingObjectives.AddListener(selectingObjectives);
            SelectingObjectivesComplete.AddListener(selectingObjectivesComplete);
            Finished.AddListener(finished);
        }

        // No two events should fire on the same frame.
        private void Update()
        {
            if (doInitializing)                                                                     Initializing.Invoke();
            else if (doInitializingComplete && activeContentSelectors.Count == 0)                   InitializingComplete.Invoke();
            else if (doSelectingFactions && activeContentSelectors.Count == 0)                      SelectingFactions.Invoke();
            else if (doSelectingFactionsComplete && activeContentSelectors.Count == 0)              SelectingFactionsComplete.Invoke();
            else if (doSelectingPrimaryModifiers && activeContentSelectors.Count == 0)              SelectingPrimaryModifiers.Invoke();
            else if (doSelectingPrimaryModifiersComplete && activeContentSelectors.Count == 0)      SelectingPrimaryModifiersComplete.Invoke();
            else if (doSelectingSecondaryModifiers && activeContentSelectors.Count == 0)            SelectingSecondaryModifiers.Invoke();
            else if (doSelectingSecondaryModifiersComplete && activeContentSelectors.Count == 0)    SelectingSecondaryModifiersComplete.Invoke();
            else if (doSelectingSetpieces && activeContentSelectors.Count == 0)                     SelectingSetpieces.Invoke();
            else if (doSelectingSetpiecesComplete && activeContentSelectors.Count == 0)             SelectingSetpiecesComplete.Invoke();
            else if (doSelectingHazards && activeContentSelectors.Count == 0)                       SelectingHazards.Invoke();
            else if (doSelectingHazardsComplete && activeContentSelectors.Count == 0)               SelectingHazardsComplete.Invoke();
            else if (doSelectingEnemies && activeContentSelectors.Count == 0)                       SelectingEnemies.Invoke();
            else if (doSelectingEnemiesComplete && activeContentSelectors.Count == 0)               SelectingEnemiesComplete.Invoke();
            else if (doSelectingObjectives && activeContentSelectors.Count == 0)                    SelectingObjectives.Invoke();
            else if (doSelectingObjectivesComplete && activeContentSelectors.Count == 0)            SelectingObjectivesComplete.Invoke();
            else if (doFinished && activeContentSelectors.Count == 0)                               Finished.Invoke();
        }

        // Simple actions to show the events firing in the console and iterate through them.
        private void initializing()
        {
            Debug.Log("Initializing content selector...");
            state = MissionContentSelectorState.INITIALIZING;
            doInitializing = false;
            doInitializingComplete = true;
        }
        private void initializingComplete()
        {
            Debug.Log("Content selector initialization complete.");
            state = MissionContentSelectorState.INITIALIZING_COMPLETE;
            doInitializingComplete = false;
            doSelectingFactions = true;
        }

        private void selectingFactions()
        {
            Debug.Log("Picking factions...");
            state = MissionContentSelectorState.SELECTING_FACTIONS;
            doSelectingFactions = false;
            doSelectingFactionsComplete = true;
        }
        private void selectingFactionsComplete()
        {
            Debug.Log("Factions selected.");
            state = MissionContentSelectorState.SELECTING_FACTIONS_COMPLETE;
            doSelectingFactionsComplete = false;
            doSelectingPrimaryModifiers = true;
        }

        private void selectingPrimaryModifiers()
        {
            Debug.Log("Selecting primary modifiers...");
            state = MissionContentSelectorState.SELECTING_PRIMARY_MODIFIERS;
            doSelectingPrimaryModifiers = false;
            doSelectingPrimaryModifiersComplete = true;
        }

        private void selectingPrimaryModifiersComplete()
        {
            Debug.Log("Primary modifiers selected.");
            state = MissionContentSelectorState.SELECTING_PRIMARY_MODIFIERS_COMPLETE;
            doSelectingPrimaryModifiersComplete = false;
            doSelectingSecondaryModifiers = true;
        }

        private void selectingSecondaryModifiers()
        {
            Debug.Log("Selecting secondary modifiers...");
            state = MissionContentSelectorState.SELECTING_SECONDARY_MODIFIERS;
            doSelectingSecondaryModifiers = false;
            doSelectingSecondaryModifiersComplete = true;
        }

        private void selectingSecondaryModifiersComplete()
        {
            Debug.Log("Secondary modifiers selected.");
            state = MissionContentSelectorState.SELECTING_SECONDARY_MODIFIERS_COMPLETE;
            doSelectingSecondaryModifiersComplete = false;
            doSelectingSetpieces = true;
        }

        private void selectingSetpieces()
        {
            Debug.Log("Selecting setpieces...");
            state = MissionContentSelectorState.SELECTING_SETPIECES;
            doSelectingSetpieces = false;
            doSelectingSetpiecesComplete = true;
        }

        private void selectingSetpiecesComplete()
        {
            Debug.Log("Setpieces selected.");
            state = MissionContentSelectorState.SELECTING_SETPIECES_COMPLETE;
            doSelectingSetpiecesComplete = false;
            doSelectingHazards = true;
        }

        private void selectingHazards()
        {
            Debug.Log("Selecting hazards...");
            state = MissionContentSelectorState.SELECTING_HAZARDS;
            doSelectingHazards = false;
            doSelectingHazardsComplete = true;
        }

        private void selectingHazardsComplete()
        {
            Debug.Log("Hazards selected.");
            state = MissionContentSelectorState.SELECTING_HAZARDS_COMPLETE;
            doSelectingHazardsComplete = false;
            doSelectingEnemies = true;
        }

        private void selectingEnemies()
        {
            Debug.Log("Selecting enemies...");
            state = MissionContentSelectorState.SELECTING_ENEMIES;
            doSelectingEnemies = false;
            doSelectingEnemiesComplete = true;
        }
        private void selectingEnemiesComplete()
        {
            Debug.Log("Enemies selecting.");
            state = MissionContentSelectorState.SELECTING_ENEMIES_COMPLETE;
            doSelectingEnemiesComplete = false;
            doSelectingObjectives = true;
        }
        private void selectingObjectives()
        {
            Debug.Log("Selecting objectives...");
            state = MissionContentSelectorState.SELECTING_OBJECTIVES;
            doSelectingObjectives = false;
            doSelectingObjectivesComplete = true;
        }
        private void selectingObjectivesComplete()
        {
            Debug.Log("Objectives selected.");
            state = MissionContentSelectorState.SELECTING_OBJECTIVES_COMPLETE;
            doSelectingObjectivesComplete = false;
            doFinished = true;
        }
        private void finished()
        {
            Debug.Log("Content selector finished.");
            state = MissionContentSelectorState.FINISHED;
            doFinished = false;
            MissionGeneratorStateHandler.RegisterSelectingMissionContentComplete();
        }
    }

    /// <summary>
    /// The state of a given level.
    /// </summary>
    public enum MissionContentSelectorState
    {
        WAITING,
        INITIALIZING,
        INITIALIZING_COMPLETE,
        SELECTING_FACTIONS,
        SELECTING_FACTIONS_COMPLETE,
        SELECTING_PRIMARY_MODIFIERS,
        SELECTING_PRIMARY_MODIFIERS_COMPLETE,
        SELECTING_SECONDARY_MODIFIERS,
        SELECTING_SECONDARY_MODIFIERS_COMPLETE,
        SELECTING_SETPIECES,
        SELECTING_SETPIECES_COMPLETE,
        SELECTING_HAZARDS,
        SELECTING_HAZARDS_COMPLETE,
        SELECTING_ENEMIES,
        SELECTING_ENEMIES_COMPLETE,
        SELECTING_OBJECTIVES,
        SELECTING_OBJECTIVES_COMPLETE,
        FINISHED
    }
}