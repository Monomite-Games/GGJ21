using System.Collections;
using UnityEngine;

using Palomas.Items;
using Palomas.Requests;
using Palomas.Pigeon;

namespace Palomas
{
    public class GameManager : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;
        private RequestsList RequestsList => RequestsList.Instance;
        private ItemsList ItemsList => ItemsList.Instance;

        [SerializeField]
        private PigeonSpawner PigeonSpawner;

        [SerializeField]
        private ItemSpawner ItemSpawner;

        private int Lifes;

        private void Start()
        {
            Time.timeScale = 1;

            GameEvents.ToPauseMenu += (sender, args) => Time.timeScale = 0;
            GameEvents.BackFromPauseMenu += (sender, args) => Time.timeScale = 1;

            GameEvents.LifeLost += (sender, args) => RespawnPigeon(args);

            StartLevel();
        }

        private void RespawnPigeon(LifeEventArgs args)
        {
            if(args.CurrentLifes.Equals(0))
            {
                GameEvents.OnGameEnd(GameEndState.Lost);
            }
            else
            {
                SpawnPigeon();
            }
        }

        private void SpawnPigeon()
        {
            PigeonSpawner.Spawn();
        }

        private void SpawnRandomRequest()
        {
            Request request = RequestsList.GetRandomUnused();
            Item item = ItemsList.GetRandomUnused();

            GameEvents.OnRequestChanged(request.GetId(), item.GetId());
            ItemSpawner.Spawn(item);
        }

        private void StartLevel()
        {
            Lifes = GameConstants.MAX_LIFES;

            StartCoroutine(DoStartLevel());
        }

        private IEnumerator DoStartLevel()
        {
            yield return new WaitForEndOfFrame();

            GameEvents.OnGamePrepared();
            //SpawnPigeon();

            yield return new WaitForSeconds(GameConstants.START_DELAY);
            
            GameEvents.OnGameStart();
            SpawnRandomRequest();
        }
    }
}
