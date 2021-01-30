using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Palomas.Menu
{
    public class LifeOverlayController : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        public Image[] LifeImages;
        private int innerCounter = GameConstants.MAX_LIFES - 1;

        private void Start()
        {
            GameEvents.LifeLost += (send, args) => UpdateLifeUI();
        }

        private void UpdateLifeUI()
        {
            LifeImages[innerCounter].enabled = false;

            innerCounter--;
        }
    }
}