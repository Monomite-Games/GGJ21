using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Palomas.Menu
{
    public class TimerController : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private Text MinutesText;

        [SerializeField]
        private Text SecondsText;

        private WaitForSeconds SecondDelay;

        private void Start()
        {
            SecondDelay = new WaitForSeconds(1.0f);

            GameEvents.GameStart += (sender, args) => StartCoroutine(DoTimerStart());
            GameEvents.GameEnd += (sender, args) => { if (args.EndState == GameEndState.Lost) { StopCoroutine(DoTimerStart()); } };
        }

        private IEnumerator DoTimerStart()
        {
            int seconds = GameConstants.TIMER_SECONDS;

            while(seconds >= 0)
            {
                int timerMinutes = seconds / 60;
                int timerSeconds = seconds % 60;

                MinutesText.text = timerMinutes.ToString();
                SecondsText.text = timerSeconds.ToString("D2");

                seconds--;

                yield return SecondDelay;
            }

            GameEvents.OnTimerEnd();
        }
    }
}