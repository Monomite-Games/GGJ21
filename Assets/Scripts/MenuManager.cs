using UnityEngine;

namespace Palomas
{
    class MenuManager : MonoBehaviour
    {
        private MenuEvents MenuEvents => MenuEvents.Instance;
        private SceneLoadManager SceneLoadManager => SceneLoadManager.Instance;

        private void Start()
        {
            Time.timeScale = 1;

            MenuEvents.StartLevel += (sender, args) => LoadLevel();
            MenuEvents.Quit += (sender, args) => Quit();
        }

        private void LoadLevel()
        {
            SceneLoadManager.LoadLevelScene();
        }

        private void Quit()
        {
            Application.Quit();

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
            #endif
        }
    }
}