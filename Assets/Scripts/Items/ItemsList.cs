using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Palomas.Items
{
    public class ItemsList : MonoBehaviour
    {
        #region Singleton
        public static ItemsList Instance
        {
            get;
            private set;
        }
        private void CreateSingleton()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        [SerializeField]
        private List<Item> Items;

        private IDictionary<string, Item> ItemsMap;

        private void Awake()
        {
            CreateSingleton();
            ItemsMap = new Dictionary<string, Item>();

            FillMap();
        }

        private void FillMap()
        {
            foreach(Item item in Items)
            {
                ItemsMap.Add(item.GetId(), item);
            }
        }

        public Item GetById(string id)
        {
            if(ItemsMap.TryGetValue(id, out Item item))
            {
                return item;
            }

            return null;
        }

        public Item GetRandomUnused()
        {
            ICollection<Item> unusedRequests = ItemsMap.Values.Where<Item>(item => !item.IsInUse()).ToList<Item>();
            return GameUtils.RandomElement<Item>(unusedRequests);
        }
    }
}
