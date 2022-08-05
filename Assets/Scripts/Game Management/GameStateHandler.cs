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
        private void Start() { _ = this; }

        // Singleton allowing access to the game state through the static class.
        private static GameState gameState = GameState.Initializing;
        public static GameState GameState { get { return gameState; } }

        // Events that fire off when the state changes.
        public UnityEvent InitializeGame = new UnityEvent();
        public UnityEvent GameInitializationComplete = new UnityEvent();
        public UnityEvent RunGame = new UnityEvent();
        public UnityEvent TerminateProcesses = new UnityEvent();
        public UnityEvent ProcessTerminationComplete = new UnityEvent();
        public UnityEvent ShutDown = new UnityEvent();

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
            InitializeGame.Invoke();
        }

        public void RegisterQuitGameRequest()
        {
            if (gameState == GameState.Running)
                TerminateProcesses.Invoke();
        }

        // Simple actions to show the events firing in the console and iterate through them.
        private void initializeGame()
        {
            Debug.Log("Initializing...");
            gameState = GameState.Initialized;
            GameInitializationComplete.Invoke();
        }

        private void gameInitializationComplete()
        {
            Debug.Log("Initialization complete.");
            gameState = GameState.Running;
            RunGame.Invoke();
        }

        private void runGame()
        {
            Debug.Log("Game Running...");
        }

        private void terminateProcesses()
        {
            Debug.Log("Terminating processes...");
            gameState = GameState.Terminating_Processes;
            ProcessTerminationComplete.Invoke();
        }

        private void processTerminationComplete()
        {
            Debug.Log("Process termination complete.");
            gameState = GameState.Processes_Terminated;
            ShutDown.Invoke();
        }

        private void shutDown()
        {
            Debug.Log("Shutting down...");
            gameState = GameState.Shutting_Down;
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
        Initializing,
        Initialized,
        Running,
        Terminating_Processes,
        Processes_Terminated,
        Shutting_Down
    }
}