using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceMage
{
    public class GameStateHandler : MonoBehaviour
    {
        // Singleton allowing access to the game state handler through the static class.
        private static GameStateHandler _;
        public static GameStateHandler Singleton { get { return _; } }
        private void Awake() { _ = this; }

        // Singleton allowing access to the game state through the static class.
        private static GameState gameState = GameState.INITIALIZING;
        public static GameState GameState { get { return gameState; } }

        // Events that fire off when the state changes.
        public UnityEvent InitializeGame = new UnityEvent();
        public UnityEvent GameInitializationComplete = new UnityEvent();
        public UnityEvent RunGame = new UnityEvent();
        public UnityEvent TerminateProcesses = new UnityEvent();
        public UnityEvent ProcessTerminationComplete = new UnityEvent();
        public UnityEvent ShutDown = new UnityEvent();

        private bool isReadyToInitializeGame = false;
        private bool isReadyToCompleteGameInitialization = false;
        private bool isReadyToRunGame = false;
        private bool isReadyToTerminateProcesses = false;
        private bool isReadyToCompleteTerminationProcess = false;
        private bool isReadyToShutDown = false;

        /// <summary>
        /// Called by game manager to start the game state handler.
        /// </summary>
        public void RegisterGameStarting()
        {
            // Register all the simple actions below.
            InitializeGame.AddListener(initializeGame);
            GameInitializationComplete.AddListener(gameInitializationComplete);
            RunGame.AddListener(runGame);
            TerminateProcesses.AddListener(terminateProcesses);
            ProcessTerminationComplete.AddListener(processTerminationComplete);
            ShutDown.AddListener(shutDown);

            // Start the state machine.
            isReadyToInitializeGame = true;
        }

        // No two events should fire on the same frame.
        private void Update()
        {
            if (isReadyToInitializeGame)
            {
                isReadyToInitializeGame = false;
                InitializeGame.Invoke();
            }

            if (isReadyToCompleteGameInitialization)
            {
                isReadyToCompleteGameInitialization = false;
                GameInitializationComplete.Invoke();
            }

            if (isReadyToRunGame)
            {
                isReadyToRunGame = false;
                RunGame.Invoke();
            }

            if (isReadyToTerminateProcesses)
            {
                isReadyToTerminateProcesses = false;
                TerminateProcesses.Invoke();
            }

            if (isReadyToCompleteTerminationProcess)
            {
                isReadyToCompleteTerminationProcess = false;
                ProcessTerminationComplete.Invoke();
            }

            if (isReadyToShutDown)
            {
                isReadyToShutDown = false;
                ShutDown.Invoke();
            }
        }

        public void RegisterQuitGameRequest()
        {
            if (gameState == GameState.RUNNING)
                isReadyToTerminateProcesses = true;
        }

        // Simple actions to show the events firing in the console and iterate through them.
        private void initializeGame()
        {
            Debug.Log("Initializing...");
            gameState = GameState.INITIALIZING_COMPLETE;
            isReadyToCompleteGameInitialization = true;
        }

        private void gameInitializationComplete()
        {
            Debug.Log("Initialization complete.");
            gameState = GameState.RUNNING;
            isReadyToRunGame = true;
        }

        private void runGame()
        {
            Debug.Log("Game Running...");
        }

        private void terminateProcesses()
        {
            Debug.Log("Terminating processes...");
            gameState = GameState.TERMINATING_PROCESSES;
            isReadyToCompleteTerminationProcess = true;
        }

        private void processTerminationComplete()
        {
            Debug.Log("Process termination complete.");
            gameState = GameState.TERMINATING_PROCESSES_COMPLETE;
            isReadyToShutDown = true;
        }

        private void shutDown()
        {
            Debug.Log("Shutting down...");
            gameState = GameState.SHUTTING_DOWN;
            // TODO: Actually shut down.
        }
    }


    /// <summary>
    /// The top level state of the game.
    /// 
    /// Initializing: Loads necessary files.
    /// Initialized: A hook for anything that should happen immediately after loading files.
    /// Running: The game functions normally.
    /// Terminating_Processes: Performs any necessary pre-shutdown steps.
    /// Processes_Terminated: A hook for anything that should happen immediately after terminating processes.
    /// Shutting_Down: Closes game.
    /// </summary>
    public enum GameState
    {
        INITIALIZING,
        INITIALIZING_COMPLETE,
        RUNNING,
        TERMINATING_PROCESSES,
        TERMINATING_PROCESSES_COMPLETE,
        SHUTTING_DOWN
    }
}