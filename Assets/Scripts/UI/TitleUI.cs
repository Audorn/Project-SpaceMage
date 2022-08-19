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
            if (GUILayout.Button("New Game")) newGame();
            if (GUILayout.Button("Settings")) viewSettings();
            if (GUILayout.Button("Site")) visitSite();
            if (GUILayout.Button("Quit")) quitGame();
        }

        private void newGame() { GameManager.StartNewGame(); }

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