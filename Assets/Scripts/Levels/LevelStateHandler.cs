using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SpaceMage.LevelGeneration;

namespace SpaceMage
{
    public class LevelStateHandler : MonoBehaviour
    {
        // Singleton allowing access to the level state handler through the static class.
        private static LevelStateHandler _;
        public static LevelStateHandler Singleton { get { return _; } }
        private void Awake() { _ = this; }

        // Singleton allowing access to the level state through the static class.
        private static LevelState state = LevelState.INITIALIZING;
        public static LevelState State { get { return state; } }

        // Events that fire off when the state changes.
        public UnityEvent Initializing = new UnityEvent();
        public UnityEvent InitializingComplete = new UnityEvent();
        public UnityEvent PickingLevelContent = new UnityEvent();
        public UnityEvent PickingLevelContentComplete = new UnityEvent();
        public UnityEvent GeneratingLevel = new UnityEvent();
        public UnityEvent GeneratingLevelComplete = new UnityEvent();
        public UnityEvent CreatingPlayer = new UnityEvent();
        public UnityEvent CreatingPlayerComplete = new UnityEvent();
        public UnityEvent Playing = new UnityEvent();
        public UnityEvent PlayingComplete = new UnityEvent();
        public UnityEvent ExitingLevel = new UnityEvent();

        private bool doInitializing = false;
        private bool doInitializingComplete = false;
        private bool doPickingLevelContent = false;
        private bool doPickingLevelContentComplete = false;
        private bool doGeneratingLevel = false;
        private bool doGeneratingLevelComplete = false;
        private bool doCreatingPlayer = false;
        private bool doCreatingPlayerComplete = false;
        private bool doPlaying = false;
        private bool doPlayingComplete = false;
        private bool doExitingLevel = false;

        public static void RegisterPickingLevelContentComplete() { _.doPickingLevelContentComplete = true; }
        public static void RegisterGeneratingLevelComplete() { _.doGeneratingLevelComplete = true; }

        /// <summary>
        /// Start the level state handler on scene load.
        /// </summary>
        public void Start()
        {
            // Register all the simple actions below.
            Initializing.AddListener(initializing);
            InitializingComplete.AddListener(initializingComplete);
            PickingLevelContent.AddListener(pickingLevelContent);
            PickingLevelContentComplete.AddListener(pickingLevelContentComplete);
            GeneratingLevel.AddListener(generatingLevel);
            GeneratingLevelComplete.AddListener(generatingLevelComplete);
            CreatingPlayer.AddListener(creatingPlayer);
            CreatingPlayerComplete.AddListener(creatingPlayerComplete);
            Playing.AddListener(playing);
            PlayingComplete.AddListener(playingComplete);
            ExitingLevel.AddListener(exitingLevel);

            // Start the state machine.
            doInitializing = true;
        }

        // No two events should fire on the same frame.
        private void Update()
        {
            if (doInitializing)
            {
                doInitializing = false;
                Initializing.Invoke();
            }

            if (doInitializingComplete)
            {
                doInitializingComplete = false;
                InitializingComplete.Invoke();
            }

            if (doPickingLevelContent)
            {
                doPickingLevelContent = false;
                PickingLevelContent.Invoke();
            }

            if (doPickingLevelContentComplete)
            {
                doPickingLevelContentComplete = false;
                PickingLevelContentComplete.Invoke();
            }

            if (doGeneratingLevel)
            {
                doGeneratingLevel = false;
                GeneratingLevel.Invoke();
            }

            if (doGeneratingLevelComplete)
            {
                doGeneratingLevelComplete = false;
                GeneratingLevelComplete.Invoke();
            }

            if (doCreatingPlayer)
            {
                doCreatingPlayer = false;
                CreatingPlayer.Invoke();
            }

            if (doCreatingPlayerComplete)
            {
                doCreatingPlayerComplete = false;
                CreatingPlayerComplete.Invoke();
            }

            if (doPlaying)
            {
                doPlaying = false;
                Playing.Invoke();
            }

            if (doPlayingComplete)
            {
                doPlayingComplete = false;
                PlayingComplete.Invoke();
            }

            if (doExitingLevel)
            {
                doExitingLevel = false;
                ExitingLevel.Invoke();
            }
        }

        // Simple actions to show the events firing in the console and iterate through them.
        private void initializing()
        {
            Debug.Log("Initializing level...");
            state = LevelState.INITIALIZING;
            doInitializingComplete = true; // TODO: Only trigger when done.
        }

        private void initializingComplete()
        {
            Debug.Log("Level initialization complete.");
            state = LevelState.INITIALIZING_COMPLETE;
            doPickingLevelContent = true; // TODO: Only trigger when done.
        }

        private void pickingLevelContent()
        {
            Debug.Log("Picking level content...");
            state = LevelState.PICKING_LEVEL_CONTENT;
            LevelContentPickerStateHandler.Initialize();
            // Registered complete by LevelContentPickerStateHandler.
        }

        private void pickingLevelContentComplete()
        {
            Debug.Log("Picking level content complete.");
            state = LevelState.PICKING_LEVEL_CONTENT_COMPLETE;
            doGeneratingLevel = true; // TODO: Only trigger when done.
        }

        private void generatingLevel()
        {
            Debug.Log("Generating level...");
            state = LevelState.GENERATING_LEVEL;
            LevelGeneratorStateHandler.Initialize();
            // Registered complete by LevelGeneratorStateHandler.
        }

        private void generatingLevelComplete()
        {
            Debug.Log("Generating level complete.");
            state = LevelState.GENERATING_LEVEL_COMPLETE;
            doCreatingPlayer = true; // TODO: Only trigger when done.
        }

        private void creatingPlayer()
        {
            Debug.Log("Creating player...");
            state = LevelState.CREATING_PLAYER;
            doCreatingPlayerComplete = true; // TODO: Only trigger when done.
        }

        private void creatingPlayerComplete()
        {
            Debug.Log("Creating player complete...");
            state = LevelState.CREATING_PLAYER_COMPLETE;
            doPlaying = true; // TODO: Only trigger when done.
        }

        private void playing()
        {
            Debug.Log("Playing level...");
            state = LevelState.PLAYING;
            //doPlayingComplete = true; // TODO: Only trigger when done.
        }

        private void playingComplete()
        {
            Debug.Log("Playing level complete...");
            state = LevelState.PLAYING_COMPLETE;
            doExitingLevel = true; // TODO: Only trigger when done.
        }

        private void exitingLevel()
        {
            Debug.Log("Exiting level...");
            state = LevelState.EXITING_LEVEL;
            // TODO: Go back to between levels ship.
        }

        // Only one level state handler can exist at a time. Clean up after yourself.
        private void OnDestroy() { _ = null; }
    }

    /// <summary>
    /// The state of a given level.
    /// </summary>
    public enum LevelState
    {
        INITIALIZING,
        INITIALIZING_COMPLETE,
        PICKING_LEVEL_CONTENT,
        PICKING_LEVEL_CONTENT_COMPLETE,
        GENERATING_LEVEL,
        GENERATING_LEVEL_COMPLETE,
        CREATING_PLAYER,
        CREATING_PLAYER_COMPLETE,
        PLAYING,
        PLAYING_COMPLETE,
        EXITING_LEVEL
    }
}