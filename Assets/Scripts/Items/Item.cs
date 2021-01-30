using UnityEngine;

namespace Palomas.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private string Id;

        [SerializeField]
        private GameObject ItemPrefab;

        public string GetId()
        {
            return Id;
        }

        public GameObject GetItemPrefab()
        {
            return ItemPrefab;
        }
    }
}