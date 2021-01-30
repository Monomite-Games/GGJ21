using UnityEngine;
using UnityEngine.UI;

namespace Palomas.Menu
{
    public class PauseMenuController : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private GameObject PausePanel;

        [SerializeField]
        private Button ContinueButton;

        [SerializeField]
        private Button RestartButton;

        [SerializeField]
        private Button ExitButton;

        private void Start()
        {
            ContinueButton.onClick.AddListener(ContinueClick);
            RestartButton.onClick.AddListener(RestartClick);
            ExitButton.onClick.AddListener(ExitClick);

            GameEvents.ToPauseMenu += (sender, args) => Show();
            GameEvents.BackFromPauseMenu += (sender, args) => Hide();
        }

        private void Show()
        {
            PausePanel.SetActive(true);
        }

        private void Hide()
        {
            PausePanel.SetActive(false);
        }

        private void ContinueClick()
        {
            GameEvents.OnBackFromPauseMenu();
        }

        private void RestartClick()
        {
            GameEvents.OnRestartLevel();
        }

        private void ExitClick()
        {
            GameEvents.OnToMainMenu();
        }
    }
}