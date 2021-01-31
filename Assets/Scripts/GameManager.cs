using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

using Palomas.Items;
using Palomas.Requests;
using Palomas.Pigeon;

namespace Palomas
{
    public class GameManager : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;
        private SceneLoadManager SceneLoadManager => SceneLoadManager.Instance;
        private RequestsList RequestsList => RequestsList.Instance;
        private ItemsList ItemsList => ItemsList.Instance;

        [SerializeField]
        private PigeonSpawner PigeonSpawner;

        [SerializeField]
        private ItemSpawner ItemSpawner;

        public Image BlackFade;
        public CinemachineVirtualCamera vCam;

        private int Lifes;
        private bool InPauseMenu = false;
        private int Points;

        private void Start()
        {
            Time.timeScale = 1;

            GameEvents.ToPauseMenu += (sender, args) => { Time.timeScale = 0; InPauseMenu = true; };
            GameEvents.BackFromPauseMenu += (sender, args) => { Time.timeScale = 1; InPauseMenu = false; };
            GameEvents.RestartLevel += (sender, args) => RestartLevel();
            GameEvents.ToMainMenu += (sender, args) => GoToMainMenu();

            GameEvents.TimerEnd += (sender, args) => GameEvents.OnGameEnd(GameEndState.Won, Points, Lifes);

            GameEvents.LifeLost += (sender, args) => RespawnPigeon();
            GameEvents.RequestCompleted += (sender, args) => { CalculatePointsGained(args.RequestPoints); Invoke(nameof(SpawnRandomRequest), GameConstants.REQUEST_DELAY); };

            StartLevel();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(InPauseMenu)
                {
                    GameEvents.OnBackFromPauseMenu();
                }
                else
                {
                    GameEvents.OnToPauseMenu();
                }
            }
        }

        private void RestartLevel()
        {
            SceneLoadManager.RestartScene();
        }

        private void GoToMainMenu()
        {
            SceneLoadManager.LoadMainMenuScene();
        }

        private void CalculatePointsGained(int requestPoints)
        {
            Points += requestPoints;
            GameEvents.OnPointsChanged(Points);
        }

        private void RespawnPigeon()
        {
            Lifes--;
            if(Lifes.Equals(0))
            {
                GameEvents.OnGameEnd(GameEndState.Lost);
            }
            else
            {
                StartCoroutine(FadeIn());
                Invoke(nameof(DisablePigeon), 1.5f);
                Invoke(nameof(SpawnPigeon), 1.8f);
                Invoke(nameof(ChangeCamFollow), 2f);
            }
        }

        private void ChangeCamFollow()
        {
            vCam.Follow = GameObject.Find("Pigeon").transform;
        }

        private void DisablePigeon()
        {
            PigeonMovement pigeon = GameObject.Find("Pigeon").GetComponent<PigeonMovement>();
            pigeon.canMove = false;

            Destroy(GameObject.Find("Pigeon"));
        }

        private void SpawnPigeon()
        {
            PigeonSpawner.Spawn();
        }

        private void SpawnRandomRequest()
        {
            Request request = RequestsList.GetRandomUnused();
            Item item = ItemsList.GetRandomUnused();

            if(request != null)
            {
                int spawnLevel = ItemSpawner.Spawn(item);
                GameEvents.OnRequestChanged(request.GetId(), item.GetId(), spawnLevel);
            }
        }

        private void StartLevel()
        {
            Lifes = GameConstants.MAX_LIFES;
            Points = 0;

            StartCoroutine(DoStartLevel());
        }

        private IEnumerator DoStartLevel()
        {
            yield return new WaitForEndOfFrame();

            GameEvents.OnGamePrepared();
            //SpawnPigeon();

            yield return new WaitForSeconds(GameConstants.START_DELAY);
            
            GameEvents.OnGameStart();

            for (int requests = 0; requests <= GameConstants.MAX_ACTIVE_REQUESTS; requests++)
            {
                SpawnRandomRequest();

                yield return new WaitForSeconds(GameConstants.REQUEST_DELAY);
            }
        }

        protected IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(0.5f);

            BlackFade.gameObject.SetActive(true);

            BlackFade.color = new Color(BlackFade.color.r, BlackFade.color.g, BlackFade.color.b, 0);
            while (BlackFade.color.a < 1.0f)
            {
                BlackFade.color = new Color(BlackFade.color.r, BlackFade.color.g, BlackFade.color.b, BlackFade.color.a + (Time.deltaTime / 1f));
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            StartCoroutine(FadeOut());
        }

        protected IEnumerator FadeOut()
        {
            BlackFade.color = new Color(BlackFade.color.r, BlackFade.color.g, BlackFade.color.b, 1);
            while (BlackFade.color.a > 0.0f)
            {
                BlackFade.color = new Color(BlackFade.color.r, BlackFade.color.g, BlackFade.color.b, BlackFade.color.a - (Time.deltaTime / 1f));
                yield return null;
            }

            BlackFade.gameObject.SetActive(false);
        }
    }
}
