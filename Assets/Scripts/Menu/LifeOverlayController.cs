using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Palomas.Menu
{
    public class LifeOverlayController : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;


        private GameObject[] LifeImages;

        private void Start()
        {
            GameEvents.LifeLost += (sned, args) => UpdateLifeUI();
        }

        private void UpdateLifeUI()
        {

        }
    }
}