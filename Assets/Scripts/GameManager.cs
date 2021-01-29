using System.Collections;
using UnityEngine;

namespace Palomas
{
    public class GameManager : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        private void Start()
        {
            Time.timeScale = 1;

            GameEvents.ToPauseMenu += (sender, args) => Time.timeScale = 0;
            GameEvents.BackFromPauseMenu += (sender, args) => Time.timeScale = 1;

            StartLevel();
        }

        private void StartLevel()
        {
            StartCoroutine(DoStartLevel());
        }

        private IEnumerator DoStartLevel()
        {
            yield return new WaitForEndOfFrame();

            GameEvents.OnGamePrepared();

            yield return new WaitForSeconds(GameConstants.START_DELAY);

            GameEvents.OnGameStart();
        }
    }
}
