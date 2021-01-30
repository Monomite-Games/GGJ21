using System.Collections.Generic;
using UnityEngine;

namespace Palomas.Items
{
    public class ItemSpawner : MonoBehaviour
    {
        private ItemsList RequestsList => ItemsList.Instance;

        [SerializeField]
        private List<Transform> SpawnPoints;

        public void Spawn(Item item)
        {
            Transform spawnPoint = GameUtils.RandomElement<Transform>(SpawnPoints);

            GameObject.Instantiate(item.GetPrefab(), spawnPoint);
        }

        public void Spawn(string itemId)
        {
            Item item = RequestsList.GetById(itemId);
            Spawn(item);
        }
    }
}
