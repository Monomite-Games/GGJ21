using UnityEngine;
using UnityEngine.SceneManagement;

namespace Palomas
{
    public class SceneLoadManager : MonoBehaviour
    {
        #region Singleton
        public static SceneLoadManager Instance
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

            LoadMainMenuScene();
        }

        private const int MainMenuBuildIndex = 1;
        private const int LevelBuildIndex = 2;

        private int CurrentBuildIndex;

        public void LoadMainMenuScene()
        {
            LoadScene(MainMenuBuildIndex);
        }

        public void LoadLevelScene()
        {
            LoadScene(LevelBuildIndex);
        }

        public void RestartScene()
        {
            LoadScene(CurrentBuildIndex);
        }

        private void LoadScene(int sceneBuildIndex)
        {
            CurrentBuildIndex = sceneBuildIndex;
            AsyncOperation asyncSceneLoading = SceneManager.LoadSceneAsync(sceneBuildIndex);
        }
    }
}