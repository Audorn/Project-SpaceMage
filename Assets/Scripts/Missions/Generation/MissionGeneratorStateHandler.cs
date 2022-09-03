using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SpaceMage.Missions;

namespace SpaceMage
{
    public class MissionGeneratorStateHandler : MonoBehaviour
    {
        // Singleton.
        private static MissionGeneratorStateHandler _;
        public static MissionGeneratorStateHandler Singleton { get { return _; } }
        private void Awake() { _ = this; }

        private static MissionGeneratorState state = MissionGeneratorState.WAITING;
        public static MissionGeneratorState State { get { return state; } }

        // Events that fire off when the state changes.
        public UnityEvent Initializing = new UnityEvent();
        public UnityEvent InitializingComplete = new UnityEvent();
        public UnityEvent SelectingMissionContent = new UnityEvent();
        public UnityEvent SelectingMissionContentComplete = new UnityEvent();
        public UnityEvent GeneratingMissionContent = new UnityEvent();
        public UnityEvent GeneratingMissionContentComplete = new UnityEvent();
        public UnityEvent CreatingPlayer = new UnityEvent();
        public UnityEvent CreatingPlayerComplete = new UnityEvent();
        public UnityEvent Playing = new UnityEvent();
        public UnityEvent PlayingComplete = new UnityEvent();
        public UnityEvent ExitingMission = new UnityEvent();

        private bool doInitializing = false;
        private bool doInitializingComplete = false;
        private bool doSelectingMissionContent = false;
        private bool doSelectingMissionContentComplete = false;
        private bool doGeneratingMissionContent = false;
        private bool doGeneratingMissionContentComplete = false;
        private bool doCreatingPlayer = false;
        private bool doCreatingPlayerComplete = false;
        private bool doPlaying = false;
        private bool doPlayingComplete = false;
        private bool doExitingMission = false;

        public static void Initialize() { _.doInitializing = true; }

        private static HashSet<MissionGenerator> activeMissionGenerators = new HashSet<MissionGenerator>();
        public static void RegisterActiveMissionGenerator(MissionGenerator generator) { activeMissionGenerators.Add(generator); }
        public static void DeRegisterActiveMissionGenerator(MissionGenerator generator) { activeMissionGenerators.Remove(generator); }
        public static void RegisterSelectingMissionContentComplete() { _.doSelectingMissionContentComplete = true; }
        public static void RegisterGeneratingLevelComplete() { _.doGeneratingMissionContentComplete = true; }

        /// <summary>
        /// Start the level state handler on scene load.
        /// </summary>
        public void Start()
        {
            // Register all the simple actions below.
            Initializing.AddListener(initializing);
            InitializingComplete.AddListener(initializingComplete);
            SelectingMissionContent.AddListener(selectingMissionContent);
            SelectingMissionContentComplete.AddListener(selectingMissionContentComplete);
            GeneratingMissionContent.AddListener(generatingMissionContent);
            GeneratingMissionContentComplete.AddListener(generatingMissionContentComplete);
            CreatingPlayer.AddListener(creatingPlayer);
            CreatingPlayerComplete.AddListener(creatingPlayerComplete);
            Playing.AddListener(playing);
            PlayingComplete.AddListener(playingComplete);
            ExitingMission.AddListener(exitingMission);
        }

        // No two events should fire on the same frame.
        private void Update()
        {
            if (doInitializing)                                                                         Initializing.Invoke();
            else if (doInitializingComplete && activeMissionGenerators.Count == 0)                      InitializingComplete.Invoke();
            else if (doSelectingMissionContent && activeMissionGenerators.Count == 0)                   SelectingMissionContent.Invoke();
            else if (doSelectingMissionContentComplete && activeMissionGenerators.Count == 0)           SelectingMissionContentComplete.Invoke();
            else if (doGeneratingMissionContent && activeMissionGenerators.Count == 0)                  GeneratingMissionContent.Invoke();
            else if (doGeneratingMissionContentComplete && activeMissionGenerators.Count == 0)          GeneratingMissionContentComplete.Invoke();
            else if (doCreatingPlayer && activeMissionGenerators.Count == 0)                            CreatingPlayer.Invoke();
            else if (doCreatingPlayerComplete && activeMissionGenerators.Count == 0)                    CreatingPlayerComplete.Invoke();
            else if (doPlaying && activeMissionGenerators.Count == 0)                                   Playing.Invoke();
            else if (doPlayingComplete && activeMissionGenerators.Count == 0)                           PlayingComplete.Invoke();
            else if (doExitingMission && activeMissionGenerators.Count == 0)                            ExitingMission.Invoke();
        }

        // Simple actions to show the events firing in the console and iterate through them.
        private void initializing()
        {
            Debug.Log("Initializing level...");
            state = MissionGeneratorState.INITIALIZING;
            doInitializing = false;
            doInitializingComplete = true; // TODO: Only trigger when done.
        }

        private void initializingComplete()
        {
            Debug.Log("Level initialization complete.");
            state = MissionGeneratorState.INITIALIZING_COMPLETE;
            doInitializingComplete = false;
            doSelectingMissionContent = true; // TODO: Only trigger when done.
        }

        private void selectingMissionContent()
        {
            state = MissionGeneratorState.SELECTING_MISSION_CONTENT;
            doSelectingMissionContent = false;
            // Initializing of MissionContentSelector handled in MissionGenerator.
            // Registered complete by MissionContentSelectorStateHandler.
        }

        private void selectingMissionContentComplete()
        {
            Debug.Log("Selecting level content complete.");
            state = MissionGeneratorState.SELECTING_MISSION_CONTENT_COMPLETE;
            doSelectingMissionContentComplete = false;
            doGeneratingMissionContent = true; // TODO: Only trigger when done.
        }

        private void generatingMissionContent()
        {
            state = MissionGeneratorState.GENERATING_MISSION_CONTENT;
            doGeneratingMissionContent = false;
            // Initializing of MissionContentGenerator handled in MissionGenerator.
            // Registered complete by MissionContentGeneratorStateHandler.
        }

        private void generatingMissionContentComplete()
        {
            Debug.Log("Generating level content complete.");
            state = MissionGeneratorState.GENERATING_MISSION_CONTENT_COMPLETE;
            doGeneratingMissionContentComplete = false;
            doCreatingPlayer = true; // TODO: Only trigger when done.
        }

        private void creatingPlayer()
        {
            Debug.Log("Creating player...");
            state = MissionGeneratorState.CREATING_PLAYER;
            doCreatingPlayer = false;
            doCreatingPlayerComplete = true; // TODO: Only trigger when done.
        }

        private void creatingPlayerComplete()
        {
            Debug.Log("Creating player complete...");
            state = MissionGeneratorState.CREATING_PLAYER_COMPLETE;
            doCreatingPlayerComplete = false;
            doPlaying = true; // TODO: Only trigger when done.
        }

        private void playing()
        {
            Debug.Log("Playing level...");
            state = MissionGeneratorState.PLAYING;
            doPlaying = false;
            //doPlayingComplete = true; // TODO: Only trigger when done.
        }

        private void playingComplete()
        {
            Debug.Log("Playing level complete...");
            state = MissionGeneratorState.PLAYING_COMPLETE;
            doPlayingComplete = false;
            doExitingMission = true; // TODO: Only trigger when done.
        }

        private void exitingMission()
        {
            Debug.Log("Exiting level...");
            state = MissionGeneratorState.EXITING_MISSION;
            doExitingMission = false;
            // TODO: Go back to between levels ship.
        }
    }

    /// <summary>
    /// The state of a given level.
    /// </summary>
    public enum MissionGeneratorState
    {
        WAITING,
        INITIALIZING,
        INITIALIZING_COMPLETE,
        SELECTING_MISSION_CONTENT,
        SELECTING_MISSION_CONTENT_COMPLETE,
        GENERATING_MISSION_CONTENT,
        GENERATING_MISSION_CONTENT_COMPLETE,
        CREATING_PLAYER,
        CREATING_PLAYER_COMPLETE,
        PLAYING,
        PLAYING_COMPLETE,
        EXITING_MISSION
    }
}