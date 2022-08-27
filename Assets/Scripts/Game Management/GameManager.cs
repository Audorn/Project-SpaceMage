using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceMage.Catalogs;
using SpaceMage.Data;
using SpaceMage.Missions;

namespace SpaceMage
{
    /// <summary>
    /// Handle top level game management tasks.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // Singleton allowing access to the game state handler through the static class.
        private static GameManager _;
        public static GameManager Singleton { get { return _; } }
        private void Awake()
        { 
            _ = this;
            catalogs = GetComponentInChildren<SetpieceCatalog>().gameObject;

            DontDestroyOnLoad(gameObject);
        }

        [SerializeField] private GameObject catalogs;
        public static GameObject Catalogs { get { return _.catalogs; } }

        public void Start()
        {
            //Screen.fullScreen = false;
            GameStateHandler.Singleton.RegisterGameStarting();
        }

        public static void StartNewGame()
        {
            Debug.Log("Starting new game...");

            MissionPossibleContent missionPossibleContent = MissionManager.GetSelectedMissionPossibleContent();
            if (missionPossibleContent.MissionType == MissionType.SURVIVAL)
            {
                MissionManager.PrepareToGenerateMission();
                SceneManager.LoadScene("Survival");
            }
        }
    }
}