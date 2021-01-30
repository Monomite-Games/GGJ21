using UnityEngine;

namespace Palomas.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private string Id;

        [SerializeField]
        private GameObject Prefab;

        private bool InUse;

        public string GetId()
        {
            return Id;
        }

        public GameObject GetPrefab()
        {
            return Prefab;
        }

        public bool IsInUse()
        {
            return InUse;
        }
    }
}