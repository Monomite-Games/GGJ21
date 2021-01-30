using System.Collections.Generic;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> SpawnPoints;

        [SerializeField]
        private GameObject PigeonPrefab;

        public void Spawn()
        {
            Transform spawnPoint = GameUtils.RandomElement<Transform>(SpawnPoints);

            GameObject pigeon = GameObject.Instantiate(PigeonPrefab, spawnPoint);
            pigeon.name = "Pigeon";
        }
    }
}
