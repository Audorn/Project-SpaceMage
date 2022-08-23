using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceMage.LevelGeneration
{
    public class LevelContentPickerStateHandler : MonoBehaviour
    {
        // Singleton allowing access to the level data generator state handler through the static class.
        private static LevelContentPickerStateHandler _;
        public static LevelContentPickerStateHandler Singleton { get { return _; } }
        private void Awake() { _ = this; }

        // Singleton allowing access to the level data generator state through the static class.
        private static LevelContentPickerState state = LevelContentPickerState.WAITING;
        public static LevelContentPickerState State { get { return state; } }

        public UnityEvent Initializing = new UnityEvent();
        public UnityEvent InitializingComplete = new UnityEvent();
        public UnityEvent PickingPrimaryModifiers = new UnityEvent();
        public UnityEvent PickingPrimaryModifiersComplete = new UnityEvent();
        public UnityEvent PickingSecondaryModifiers = new UnityEvent();
        public UnityEvent PickingSecondaryModifiersComplete = new UnityEvent();
        public UnityEvent PickingSetpieces = new UnityEvent();
        public UnityEvent PickingSetpiecesComplete = new UnityEvent();
        public UnityEvent PickingHazards = new UnityEvent();
        public UnityEvent PickingHazardsComplete = new UnityEvent();
        public UnityEvent PickingEnemies = new UnityEvent();
        public UnityEvent PickingEnemiesComplete = new UnityEvent();
        public UnityEvent PickingObjectives = new UnityEvent();
        public UnityEvent PickingObjectivesComplete = new UnityEvent();
        public UnityEvent Finished = new UnityEvent();

        private bool doInitializing = false;
        private bool doInitializingComplete = false;
        private bool doPickingPrimaryModifiers = false;
        private bool doPickingPrimaryModifiersComplete = false;
        private bool doPickingSecondaryModifiers = false;
        private bool doPickingSecondaryModifiersComplete = false;
        private bool doPickingSetpieces = false;
        private bool doPickingSetpiecesComplete = false;
        private bool doPickingHazards = false;
        private bool doPickingHazardsComplete = false;
        private bool doPickingEnemies = false;
        private bool doPickingEnemiesComplete = false;
        private bool doPickingObjectives = false;
        private bool doPickingObjectivesComplete = false;
        private bool doFinished = false;

        public static void Initialize() { _.doInitializing = true; }

        private static HashSet<LevelContentPicker> activeContentPickers = new HashSet<LevelContentPicker>();
        public static void RegisterActiveContentPicker(LevelContentPicker contentPicker) { activeContentPickers.Add(contentPicker); }
        public static void DeRegisterActiveContentPicker(LevelContentPicker contentPicker) { activeContentPickers.Remove(contentPicker); }
        /// <summary>
        /// Called by LevelManager to start the level data generator state handler.
        /// </summary>
        public void Start()
        {
            // Register all the simple actions below.
            Initializing.AddListener(initializing);
            InitializingComplete.AddListener(initializingComplete);
            PickingPrimaryModifiers.AddListener(pickingPrimaryModifiers);
            PickingPrimaryModifiersComplete.AddListener(pickingPrimaryModifiersComplete);
            PickingSecondaryModifiers.AddListener(pickingSecondaryModifiers);
            PickingSecondaryModifiersComplete.AddListener(pickingSecondaryModifiersComplete);
            PickingSetpieces.AddListener(pickingSetpieces);
            PickingSetpiecesComplete.AddListener(pickingSetpiecesComplete);
            PickingHazards.AddListener(pickingHazards);
            PickingHazardsComplete.AddListener(pickingHazardsComplete);
            PickingEnemies.AddListener(pickingEnemies);
            PickingEnemiesComplete.AddListener(pickingEnemiesComplete);
            PickingObjectives.AddListener(pickingObjectives);
            PickingObjectivesComplete.AddListener(pickingObjectivesComplete);
            Finished.AddListener(finished);
        }

        // No two events should fire on the same frame.
        private void Update()
        {
            if (doInitializing)                                                                Initializing.Invoke();
            else if (doInitializingComplete && activeContentPickers.Count == 0)                InitializingComplete.Invoke();
            else if (doPickingPrimaryModifiers && activeContentPickers.Count == 0)             PickingPrimaryModifiers.Invoke();
            else if (doPickingPrimaryModifiersComplete && activeContentPickers.Count == 0)     PickingPrimaryModifiersComplete.Invoke();
            else if (doPickingSecondaryModifiers && activeContentPickers.Count == 0)           PickingSecondaryModifiers.Invoke();
            else if (doPickingSecondaryModifiersComplete && activeContentPickers.Count == 0)   PickingSecondaryModifiersComplete.Invoke();
            else if (doPickingSetpieces && activeContentPickers.Count == 0)                    PickingSetpieces.Invoke();
            else if (doPickingSetpiecesComplete && activeContentPickers.Count == 0)            PickingSetpiecesComplete.Invoke();
            else if (doPickingHazards && activeContentPickers.Count == 0)                      PickingHazards.Invoke();
            else if (doPickingHazardsComplete && activeContentPickers.Count == 0)              PickingHazardsComplete.Invoke();
            else if (doPickingEnemies && activeContentPickers.Count == 0)                      PickingEnemies.Invoke();
            else if (doPickingEnemiesComplete && activeContentPickers.Count == 0)              PickingEnemiesComplete.Invoke();
            else if (doPickingObjectives && activeContentPickers.Count == 0)                   PickingObjectives.Invoke();
            else if (doPickingObjectivesComplete && activeContentPickers.Count == 0)           PickingObjectivesComplete.Invoke();
            else if (doFinished && activeContentPickers.Count == 0)                            Finished.Invoke();
        }

        // Simple actions to show the events firing in the console and iterate through them.
        private void initializing()
        {
            Debug.Log("Initializing content picker...");
            state = LevelContentPickerState.INITIALIZING;
            doInitializing = false;
            doInitializingComplete = true;
        }

        private void initializingComplete()
        {
            Debug.Log("Content picker initialization complete.");
            state = LevelContentPickerState.INITIALIZING_COMPLETE;
            doInitializingComplete = false;
            doPickingPrimaryModifiers = true;
        }

        private void pickingPrimaryModifiers()
        {
            Debug.Log("Picking primary modifiers...");
            state = LevelContentPickerState.PICKING_PRIMARY_MODIFIERS;
            doPickingPrimaryModifiers = false;
            doPickingPrimaryModifiersComplete = true;
        }

        private void pickingPrimaryModifiersComplete()
        {
            Debug.Log("Primary modifiers picked.");
            state = LevelContentPickerState.PICKING_PRIMARY_MODIFIERS_COMPLETE;
            doPickingPrimaryModifiersComplete = false;
            doPickingSecondaryModifiers = true;
        }

        private void pickingSecondaryModifiers()
        {
            Debug.Log("Picking secondary modifiers...");
            state = LevelContentPickerState.PICKING_SECONDARY_MODIFIERS;
            doPickingSecondaryModifiers = false;
            doPickingSecondaryModifiersComplete = true;
        }

        private void pickingSecondaryModifiersComplete()
        {
            Debug.Log("Secondary modifiers picked.");
            state = LevelContentPickerState.PICKING_SECONDARY_MODIFIERS_COMPLETE;
            doPickingSecondaryModifiersComplete = false;
            doPickingSetpieces = true;
        }

        private void pickingSetpieces()
        {
            Debug.Log("Picking setpieces...");
            state = LevelContentPickerState.PICKING_SETPIECES;
            doPickingSetpieces = false;
            doPickingSetpiecesComplete = true;
        }

        private void pickingSetpiecesComplete()
        {
            Debug.Log("Setpieces picked.");
            state = LevelContentPickerState.PICKING_SETPIECES_COMPLETE;
            doPickingSetpiecesComplete = false;
            doPickingHazards = true;
        }

        private void pickingHazards()
        {
            Debug.Log("Picking hazards...");
            state = LevelContentPickerState.PICKING_HAZARDS;
            doPickingHazards = false;
            doPickingHazardsComplete = true;
        }

        private void pickingHazardsComplete()
        {
            Debug.Log("Hazards picked.");
            state = LevelContentPickerState.PICKING_HAZARDS_COMPLETE;
            doPickingHazardsComplete = false;
            doPickingEnemies = true;
        }

        private void pickingEnemies()
        {
            Debug.Log("Picking enemies...");
            state = LevelContentPickerState.PICKING_ENEMIES;
            doPickingEnemies = false;
            doPickingEnemiesComplete = true;
        }
        private void pickingEnemiesComplete()
        {
            Debug.Log("Enemies picked.");
            state = LevelContentPickerState.PICKING_ENEMIES_COMPLETE;
            doPickingEnemiesComplete = false;
            doPickingObjectives = true;
        }
        private void pickingObjectives()
        {
            Debug.Log("Picking objectives...");
            state = LevelContentPickerState.PICKING_OBJECTIVES;
            doPickingObjectives = false;
            doPickingObjectivesComplete = true;
        }
        private void pickingObjectivesComplete()
        {
            Debug.Log("Objectives picked.");
            state = LevelContentPickerState.PICKING_OBJECTIVES_COMPLETE;
            doPickingObjectivesComplete = false;
            doFinished = true;
        }
        private void finished()
        {
            Debug.Log("Content picker finished.");
            state = LevelContentPickerState.FINISHED;
            doFinished = false;
            LevelStateHandler.RegisterPickingLevelContentComplete();
        }

        // Only one level content picker state handler can exist at a time. Clean up after yourself.
        private void OnDestroy() { _ = null; }
    }

    /// <summary>
    /// The state of a given level.
    /// </summary>
    public enum LevelContentPickerState
    {
        WAITING,
        INITIALIZING,
        INITIALIZING_COMPLETE,
        PICKING_PRIMARY_MODIFIERS,
        PICKING_PRIMARY_MODIFIERS_COMPLETE,
        PICKING_SECONDARY_MODIFIERS,
        PICKING_SECONDARY_MODIFIERS_COMPLETE,
        PICKING_SETPIECES,
        PICKING_SETPIECES_COMPLETE,
        PICKING_HAZARDS,
        PICKING_HAZARDS_COMPLETE,
        PICKING_ENEMIES,
        PICKING_ENEMIES_COMPLETE,
        PICKING_OBJECTIVES,
        PICKING_OBJECTIVES_COMPLETE,
        FINISHED
    }
}