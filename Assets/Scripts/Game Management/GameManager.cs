using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SpaceMage
{
    /// <summary>
    /// Handle top level game management tasks.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public void Start()
        {
            //Screen.fullScreen = false;
            GameStateHandler.Singleton.RegisterGameStarting();
        }
    }
}