using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using SpaceMage.Catalogs;
using SpaceMage.Entities;
using SpaceMage.LevelGeneration;

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
        }

        [SerializeField] private GameObject playerPrefab;

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

            LevelController.ToggleSpawnZones(true);
            //SpawnSettings spawnSettings = new SpawnSettings(Faction.NEUTRAL, ThreatLevel.VERY_LOW, Rarity.COMMON, 5);
            //List<Hazard> hazards = HazardCatalog.GetHazards(spawnSettings);

            //foreach(Hazard hazard in hazards)
            //{
            //    Vector2 position = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            //    Quaternion rotation = Random.rotation;
            //    rotation.x = rotation.y = 0;

            //    Hazard h = Instantiate(hazard, position, rotation);
            //    Vector2 direction = new Vector2(Random.Range(-1000, 1000), Random.Range(-1000, 1000));
            //    float force = Random.Range(-1, 1);
            //    h.GetComponent<Rigidbody2D>().AddForce(direction * force);
            //}
        }
    }
}