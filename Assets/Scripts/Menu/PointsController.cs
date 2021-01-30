using UnityEngine;
using UnityEngine.UI;

namespace Palomas.Menu
{
    public class PointsController : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private Text PointsText;

        [SerializeField]
        private string Suffix = "$";

        private void Start()
        {
            UpdatePoints(0);

            GameEvents.PointsChanged += (sender, args) => UpdatePoints(args);
        }

        private void UpdatePoints(int currentPoints)
        {
            PointsText.text = currentPoints.ToString() + " " + Suffix;
        }
    }
}