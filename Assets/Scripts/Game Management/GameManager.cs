using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using SpaceMage.Catalogs;
using SpaceMage.Entities;

namespace SpaceMage
{
    /// <summary>
    /// Handle top level game management tasks.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;

        public void Start()
        {
            //Screen.fullScreen = false;
            GameStateHandler.Singleton.RegisterGameStarting();
        }

        public static void StartNewGame()
        {
            Debug.Log("Starting new game...");

            SortingData sortingData = new SortingData(Faction.NEUTRAL, ThreatLevel.VERY_LOW, Rarity.COMMON, 5, false);
            List<Hazard> hazards = HazardCatalog.GetHazard(sortingData);

            foreach(Hazard hazard in hazards)
            {
                Vector2 position = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
                Quaternion rotation = Random.rotation;
                rotation.x = rotation.y = 0;

                Hazard h = Instantiate(hazard, position, rotation);
                Vector2 direction = new Vector2(Random.Range(-1000, 1000), Random.Range(-1000, 1000));
                float force = Random.Range(-1, 1);
                h.GetComponent<Rigidbody2D>().AddForce(direction * force);
            }
        }
    }
}