using UnityEngine;
using UnityEngine.UI;

namespace Palomas.Menu
{
    public class MenuController : MonoBehaviour
    {
        private MenuEvents MenuEvents => MenuEvents.Instance;

        [SerializeField]
        private Button StartButton;

        [SerializeField]
        private Button QuitButton;

        private void Start()
        {
            StartButton.onClick.AddListener(MenuEvents.OnToLevel);
            QuitButton.onClick.AddListener(MenuEvents.OnQuit);
        }
    }
}
