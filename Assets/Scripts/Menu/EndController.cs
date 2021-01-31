using System;
using UnityEngine;
using UnityEngine.UI;

namespace Palomas.Menu
{
    public class EndController : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private GameObject WonPanel;

        [SerializeField]
        private GameObject LostPanel;

        [SerializeField]
        private GameObject ButtonPanel;

        [SerializeField]
        private Text PointsText;

        [SerializeField]
        private Button RestartButton;

        [SerializeField]
        private Button MenuButton;

        private void Start()
        {
            GameEvents.GameEnd += (sender, args) => ShowEnd(args);

            RestartButton.onClick.AddListener(RestartClick);
            MenuButton.onClick.AddListener(MenuClick);
        }

        private void RestartClick()
        {
            GameEvents.OnRestartLevel();
        }

        private void MenuClick()
        {
            GameEvents.OnToMainMenu();
        }

        private void ShowEnd(GameEndEventArgs args)
        {
            switch (args.EndState)
            {
                case GameEndState.Won:
                    ShowEndWon(args.Points);
                    break;
                case GameEndState.Lost:
                    ShowEndLost();
                    break;
            }
            ButtonPanel.SetActive(true);
        }

        private void ShowEndLost()
        {
            WonPanel.SetActive(false);
            LostPanel.SetActive(true);
        }

        private void ShowEndWon(int points)
        {
            WonPanel.SetActive(true);
            LostPanel.SetActive(false);

            PointsText.text = points.ToString();
        }
    }
}