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

            GameObject.Instantiate(PigeonPrefab, spawnPoint);
        }
    }
}
