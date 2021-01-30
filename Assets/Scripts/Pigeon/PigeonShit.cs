using System.Collections;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonShit : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private GameObject ShitPrefab;

        [SerializeField]
        private Transform ShitHole;

        private int ShitMeter;
        private WaitForSeconds ManaDelay;

        private void Start()
        {
            ManaDelay = new WaitForSeconds(GameConstants.MANA_DELAY);
            ShitMeter = GameConstants.MAX_MANA;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && ShitMeter >= GameConstants.MAX_MANA)
            {
                GameObject.Instantiate(ShitPrefab, ShitHole.position, ShitHole.rotation);
                GameEvents.OnShit();
                ResetMana();
            }
        }

        private void ResetMana()
        {
            SetShitMeter(0);
            StartCoroutine(DoGraduallyIncreaseMana());
        }

        private IEnumerator DoGraduallyIncreaseMana()
        {
            while(ShitMeter < GameConstants.MAX_MANA) {
                yield return ManaDelay;

                int newValue = ShitMeter + GameConstants.MANA_PER_DELAY;
                SetShitMeter(newValue);
            }
        }

        private void SetShitMeter(int newValue)
        {
            ShitMeter = newValue;
            if(ShitMeter > GameConstants.MAX_MANA)
            {
                ShitMeter = GameConstants.MAX_MANA;
            }
            GameEvents.OnShitMeterChanged(ShitMeter);
        }
    }
}