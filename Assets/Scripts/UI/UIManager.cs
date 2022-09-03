using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TitleUI title;
        public void OpenTitleMenu() { title.enabled = true; }
        public void CloseTitleMenu() { title.enabled = false; }

        private void Start()
        {
            GameStateHandler.Singleton.GameInitializationComplete.AddListener(OpenTitleMenu);
        }
    }
}