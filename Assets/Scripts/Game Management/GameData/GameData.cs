using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Data
{
    public class GameData : MonoBehaviour
    {
        // Singleton
        private static GameData _;
        public static GameData Singleton { get { return _; } }
        private void Awake() { _ = this; }

        [SerializeField] private GameObject playerPrefab;
        public static GameObject PlayerPrefab { get { return _.playerPrefab; } }
    }
}