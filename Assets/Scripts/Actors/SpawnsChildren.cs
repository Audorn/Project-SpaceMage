using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceMage.Extensions;

namespace SpaceMage.Entities
{
    public class SpawnsChildren : MonoBehaviour
    {
        [SerializeField] private Order order = Order.PRIMARY;
        [SerializeField] private List<int> numberOfHazards = new List<int>();
        [SerializeField] private List<Hazard> hazardPrefabs = new List<Hazard>();

        public Order Order { get { return order; } }
        public void SetOrder(Order order) { this.order = order; }
        public List<int> NumberOfHazards { get { return numberOfHazards; } }
        public List<Hazard> HazardPrefabs { get { return hazardPrefabs; } }

        public void SpawnChildren()
        {
            StartCoroutine(spawnHazards());
            // Spawn other stuff.
        }

        private IEnumerator spawnHazards()
        {
            // Early out - nothing to spawn.
            if (numberOfHazards.Count == 0)
                yield break;

            int numberOfSpawnSets = numberOfHazards.Count;
            for (int i = 0; i < numberOfSpawnSets; i++)
            {
                StartCoroutine(spawnHazards(numberOfHazards[i], hazardPrefabs[i]));
                yield return new WaitForFixedUpdate();
            }
        }
        private IEnumerator spawnHazards(int numberToSpawn, Hazard hazardPrefab)
        {
            for (int j = 0; j < numberToSpawn; j++)
            {
                Hazard hazard = HazardManager.InstantiateHazard(hazardPrefab, transform.position, transform.rotation, order.Next(false));
                yield return new WaitForFixedUpdate();
            }

            Debug.LogError("finished");
            GetComponent<WaitsInQueue>().RegisterFinishedSpawningChildren();
        }
    }
}