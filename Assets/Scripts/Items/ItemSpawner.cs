﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Palomas.Items
{
    public class ItemSpawner : MonoBehaviour
    {
        private ItemsList RequestsList => ItemsList.Instance;

        [SerializeField]
        private List<SpawnPointState> SpawnPoints;

        public int Spawn(Item item)
        {
            ICollection<SpawnPointState> unusedSpawnPoints = SpawnPoints.Where<SpawnPointState>(state => !state.IsInUse()).ToList<SpawnPointState>();
            SpawnPointState spawnPoint = GameUtils.RandomElement<SpawnPointState>(unusedSpawnPoints);

            spawnPoint.SetItemId(item.GetId());
            GameObject itemObject = GameObject.Instantiate(item.GetPrefab(), spawnPoint.transform);
            itemObject.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);

            return spawnPoint.GetLevel();
        }

        public void Spawn(ItemTypes itemId)
        {
            Item item = RequestsList.GetById(itemId);
            Spawn(item);
        }
    }
}
