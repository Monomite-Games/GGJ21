using System;
using UnityEngine;

namespace Palomas
{
    public class MenuEvents : MonoBehaviour
    {
        #region Singleton
        public static MenuEvents Instance
        {
            get;
            private set;
        }
        private void CreateSingleton()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        private void Awake()
        {
            CreateSingleton();
        }

        public event EventHandler StartLevel;
        public event EventHandler Quit;

        public void OnToLevel()
        {
            StartLevel?.Invoke(this, EventArgs.Empty);
        }

        public void OnQuit()
        {
            Quit?.Invoke(this, EventArgs.Empty);
        }
    }
}