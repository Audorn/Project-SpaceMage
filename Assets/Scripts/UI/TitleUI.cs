using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SpaceMage.UI
{
    public class TitleUI : MonoBehaviour
    {
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
                MainMenu();
            GUILayout.EndArea();
        }

        private void MainMenu()
        {
            // TODO: If a game can be loaded, offer continue instead.
            if (GUILayout.Button("New Solo Game")) newGame();
            if (GUILayout.Button("Host Game")) hostGame();
            if (GUILayout.Button("Join Game")) joinGame();
            if (GUILayout.Button("Settings")) viewSettings();
            if (GUILayout.Button("Site")) visitSite();
            if (GUILayout.Button("Quit")) quitGame();
        }

        private void newGame()
        {
            Debug.Log("Starting new solo game...");
        }

        private void hostGame()
        {
            // TODO: Create ui to load a game or start a new one.
            // Character will either be in memory or a new one will need to be picked / created.
            Debug.Log("Starting game as host...");
        }

        private void joinGame()
        {
            // TODO: Create ui to select game to join.
            // Character will either be in memory or a new one will need to be picked / created.
            Debug.Log("Joining game...");
        }

        private void viewSettings()
        {
            Debug.Log("Opening settings...");
        }

        private void visitSite()
        {
            Debug.Log("Accessing external site...");
        }

        private void quitGame()
        {
            Debug.Log("Quitting...");
            GameStateHandler.Singleton.RegisterQuitGameRequest();
        }
    }
}